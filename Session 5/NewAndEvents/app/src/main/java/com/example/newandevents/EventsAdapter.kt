package com.example.newandevents

import android.Manifest
import android.annotation.SuppressLint
import android.content.ContentValues
import android.content.Context
import android.content.pm.PackageManager
import android.net.Uri
import android.provider.CalendarContract
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.core.app.ActivityCompat
import androidx.core.content.ContextCompat
import androidx.recyclerview.widget.RecyclerView
import java.text.SimpleDateFormat
import java.util.Calendar
import java.util.Locale

class EventsAdapter(private val context: Context, private var list: List<Event>) :
    RecyclerView.Adapter<EventsAdapter.EventsViewHolder>() {

    class EventsViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        val textTitle: TextView = itemView.findViewById(R.id.textEventName)
        val textBody: TextView = itemView.findViewById(R.id.textBody)
        val textAuthor: TextView = itemView.findViewById(R.id.textAuthor)
        val textDate: TextView = itemView.findViewById(R.id.textDate)
        val starButton: ImageView = itemView.findViewById(R.id.imageView)
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): EventsViewHolder {
        val view = LayoutInflater.from(context).inflate(R.layout.event_card, parent, false)
        return EventsViewHolder(view)
    }

    override fun getItemCount(): Int {
        return list.size
    }

    override fun onBindViewHolder(holder: EventsViewHolder, position: Int) {
        val event = list[position]

        Log.d("EventsAdapter", "Binding event at position $position: ${event.getTitle()}")

        holder.textTitle.text = event.getTitle()
        holder.textBody.text = event.getBody()
        holder.textAuthor.text = event.getAuthor()
        holder.textDate.text = event.getDate()

        holder.starButton.setOnClickListener {
            checkCalendarPermissions(event)
        }
    }

    fun updateList(it: List<Event>) {
        list = it
        notifyDataSetChanged()
    }

    private val REQUEST_CODE = 100

    private fun checkCalendarPermissions(event: Event) {
        if (ContextCompat.checkSelfPermission(
                context,
                Manifest.permission.READ_CALENDAR
            ) != PackageManager.PERMISSION_GRANTED ||
            ContextCompat.checkSelfPermission(
                context,
                Manifest.permission.WRITE_CALENDAR
            ) != PackageManager.PERMISSION_GRANTED
        ) {

            ActivityCompat.requestPermissions(
                (context as AppCompatActivity),
                arrayOf(Manifest.permission.READ_CALENDAR, Manifest.permission.WRITE_CALENDAR),
                REQUEST_CODE
            )
        } else {
            addEventToCalendar(event)
        }
    }

    @SuppressLint("Range")
    private fun getCalendarId(): Long? {
        val projection = arrayOf(
            CalendarContract.Calendars._ID,
            CalendarContract.Calendars.CALENDAR_DISPLAY_NAME
        )
        val cursor = context.contentResolver.query(
            CalendarContract.Calendars.CONTENT_URI,
            projection,
            null,
            null,
            null
        )

        cursor?.use {
            if (it.moveToFirst()) {
                return it.getLong(it.getColumnIndex(CalendarContract.Calendars._ID))
            }
        }
        return null
    }

    fun addEventToCalendar(event: Event) {
        val calendarId = getCalendarId()
        if (calendarId == null) {
            Toast.makeText(context, "Нет доступных календарей.", Toast.LENGTH_SHORT).show()
            return
        }
        val values = ContentValues().apply {
            put(CalendarContract.Events.TITLE, event.getTitle())
            put(CalendarContract.Events.DESCRIPTION, event.getBody())
            put(CalendarContract.Events.DTSTART, parseDate(event.getDate()))
            put(CalendarContract.Events.DTEND, parseDate(event.getDate()))
            put(CalendarContract.Events.CALENDAR_ID, calendarId)
            put(CalendarContract.Events.EVENT_TIMEZONE, "UTC+5")
        }

        val uri: Uri? =
            context.contentResolver.insert(CalendarContract.Events.CONTENT_URI, values)


        if (uri != null) {
            Toast.makeText(context, "Событие добавлено в календарь!", Toast.LENGTH_SHORT).show()
        } else {
            Toast.makeText(context, "Ошибка при добавлении события.", Toast.LENGTH_SHORT).show()
        }
    }

        private fun parseDate(dateString: String): Long {
            val format = SimpleDateFormat("dd.MM.yyyy", Locale.getDefault())
            return format.parse(dateString)?.time ?: 0L
        }

}

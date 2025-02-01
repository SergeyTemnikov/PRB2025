package com.example.newandevents

import com.google.gson.annotations.SerializedName
import com.google.type.DateTime
import java.text.SimpleDateFormat
import java.util.Date
import java.util.Locale

class Event(
    @SerializedName("Title")
    private val title: String,
    @SerializedName("Body")
    private val body: String,
    @SerializedName("Author")
    private val author: String,
    @SerializedName("Date")
    private val date: String
) {
    fun getTitle(): String {
        return title
    }
    fun getBody(): String {
        return body
    }
    fun getAuthor(): String {
        return author
    }
    fun getDate(): String {
        if(date == null)
        {
            return "Unknown date"
        }
        return date
    }

    fun getParseDate() : Date
    {
        return  parseDate(getDate())
    }

    fun parseDate(dateString: String): Date {
        val format = SimpleDateFormat("dd.MM.yyyy", Locale.getDefault())
        return format.parse(dateString) ?: throw IllegalArgumentException("Invalid date format")
    }
}
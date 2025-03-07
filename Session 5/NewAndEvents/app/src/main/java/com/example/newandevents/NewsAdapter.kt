package com.example.newandevents

import android.app.Dialog
import android.content.Context
import android.graphics.Color
import android.graphics.drawable.ColorDrawable
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.FrameLayout
import android.widget.ImageView
import android.widget.TextView
import android.widget.Toast
import androidx.recyclerview.widget.RecyclerView
import com.example.newandevents.databinding.ActivitySetReactionBinding
import com.squareup.picasso.Picasso
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class NewsAdapter(private val context: Context, private var list: List<News>) : RecyclerView.Adapter<NewsAdapter.NewsViewHolder>() {

    private var newList: MutableList<News> = mutableListOf()

    class NewsViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        val textTitle: TextView = itemView.findViewById(R.id.textNewsName)
        val textBody: TextView = itemView.findViewById(R.id.textBody)
        val textPositiveReactions: TextView = itemView.findViewById(R.id.textPositiveReactions)
        val textNegativeReactions: TextView = itemView.findViewById(R.id.textNegativeReactions)
        val textDate: TextView = itemView.findViewById(R.id.textDate)
        val imageView: ImageView = itemView.findViewById(R.id.imageView)
        val frameLayout: FrameLayout = itemView.findViewById(R.id.Frame)
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): NewsViewHolder {
        val view = LayoutInflater.from(context).inflate(R.layout.news_card, parent, false)
        return NewsViewHolder(view)
    }

    override fun getItemCount(): Int {
        return list.size
    }

    override fun onBindViewHolder(holder: NewsViewHolder, position: Int) {
        val news = list[position]

        holder.textTitle.text = news.getTitle()
        holder.textBody.text = news.getBody()
        holder.textPositiveReactions.text = news.getPosReactions()
        holder.textNegativeReactions.text = news.getNegReactions()
        holder.textDate.text = news.getDate()

        holder.frameLayout.setOnLongClickListener {
            showDialog(context, position)
            return@setOnLongClickListener true
        }
        Log.d("NewsAdapter", "Picture: ${news.getPicture()}")

        Picasso.get()
            .load(news.getPicture())
            .fit()
            .centerInside()
            .placeholder(R.drawable.photo)
            .into(holder.imageView)
    }

    fun updateList(it: List<News>) {
        list = it
        notifyDataSetChanged()
    }

    private fun showDialog(context: Context, position: Int) {
        val dialogBinding = ActivitySetReactionBinding.inflate(LayoutInflater.from(context))
        val myDialog = Dialog(context)
        myDialog.setContentView(dialogBinding.root)
        myDialog.setCancelable(true)
        myDialog.window?.setBackgroundDrawable(ColorDrawable(Color.TRANSPARENT))
        myDialog.show()

        dialogBinding.buttonPlus.setOnClickListener {
            RetrofitClient.instance.updatePositiveReactions(position)
                .enqueue( object: Callback<News> {
                    override fun onResponse(call: Call<News>, response: Response<News>) {
                        if (response.isSuccessful) {
                            val updatedNews  = response.body()
                            newList = list.toSet().toMutableList()
                            if (updatedNews != null) {
                                newList[position] = updatedNews
                            }
                            list = newList.toList()
                            notifyItemChanged(position)
                        } else {
                            // В консоль или в лог ошибку
                        }
                    }
                    override fun onFailure(call: Call<News>, t: Throwable) {
                        // В консоль или в лог ошибку
                    }
                })
            myDialog.dismiss()
        }

        dialogBinding.buttonMinus.setOnClickListener {
            RetrofitClient.instance.updateNegativeReactions(position)
                .enqueue( object: Callback<News> {
                    override fun onResponse(call: Call<News>, response: Response<News>) {
                        if (response.isSuccessful) {
                            val updatedNews  = response.body()
                            newList = list.toSet().toSet().toMutableList()
                            if (updatedNews != null) {
                                newList[position] = updatedNews
                            }
                            list = newList.toList()
                            notifyItemChanged(position)
                        } else {
                            // В консоль или в лог ошибку
                        }
                    }
                    override fun onFailure(call: Call<News>, t: Throwable) {
                        // В консоль или в лог ошибку
                    }
                })
            myDialog.dismiss()
        }
    }
}
package com.example.newandevents

import com.google.gson.annotations.SerializedName

class News(
    @SerializedName("Id")
    private val id: Int,
    @SerializedName("Title")
    private val title: String,
    @SerializedName("Body")
    private val body: String,
    @SerializedName("Date")
    private val date: String,
    @SerializedName("Picture")
    private val picture: String,
    @SerializedName("PositiveReactions")
    private val positiveReactions: Int,
    @SerializedName("NegativeReactions")
    private val negativeReactions: Int
) {
    fun getId(): Int{
        return id
    }

    fun getTitle(): String {
        return title
    }
    fun getBody(): String {
        return body
    }
    fun getPicture(): String {
        return picture
    }
    fun getDate(): String {
        return date
    }
    fun getPosReactions(): String {
        return "+" + positiveReactions.toString()
    }
    fun getNegReactions(): String {
        return "-" + negativeReactions.toString()
    }
}
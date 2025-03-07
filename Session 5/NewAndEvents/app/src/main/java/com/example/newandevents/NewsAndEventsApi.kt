package com.example.newandevents

import retrofit2.Call
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.PUT
import retrofit2.http.Query

interface NewsAndEventsApi{
    @GET("/api/Events/GetEvents")
    fun getEvents(): Call<List<Event>>

    @GET("/api/News/GetNews")
    fun getNews(): Call<List<News>>

    @PUT("/api/News/PostPositiveReactions")
    fun updatePositiveReactions(@Query("id") id: Int): Call<News>

    @PUT("/api/News/PostNegativeReactions")
    fun updateNegativeReactions(@Query("id") id: Int): Call<News>
}
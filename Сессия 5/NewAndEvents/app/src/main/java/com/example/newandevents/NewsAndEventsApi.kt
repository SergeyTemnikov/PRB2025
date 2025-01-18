package com.example.newandevents

import retrofit2.Call
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.PUT
import retrofit2.http.Query

interface NewsAndEventsApi{
    @GET("/WebApiNews/GetEvents")
    fun getEvents(): Call<List<Event>>

    @GET("/WebApiNews/GetNews")
    fun getNews(): Call<List<News>>

    @PUT("/WebApiNews/PostPositiveReactions")
    fun updatePositiveReactions(@Query("id") id: Int): Call<News>

    @PUT("/WebApiNews/PostNegativeReactions")
    fun updateNegativeReactions(@Query("id") id: Int): Call<News>
}
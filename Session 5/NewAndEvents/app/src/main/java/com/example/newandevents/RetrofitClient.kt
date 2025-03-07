package com.example.newandevents

import okhttp3.OkHttpClient
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import java.security.SecureRandom
import javax.net.ssl.SSLContext
import javax.net.ssl.SSLSocketFactory

object RetrofitClient {
    private const val BASE_URL = "https://10.0.2.2:7208"// Замените на ваш базовый URL
    val instance: NewsAndEventsApi by lazy {
        val client = OkHttpClient.Builder()
            .sslSocketFactory(createSSLSocketFactory(), CustomTrustManager())
            .hostnameVerifier { hostname, session -> true } // Принимаем все имена хостов (не рекомендуется для продакшена)
            .build()

        val retrofit = Retrofit.Builder()
            .baseUrl(BASE_URL)
            .client(client)
            .addConverterFactory(GsonConverterFactory.create())
            .build()

//        val retrofit = Retrofit.Builder()
//            .baseUrl(BASE_URL)
//            .addConverterFactory(GsonConverterFactory.create())
//            .build()
        retrofit.create(NewsAndEventsApi::class.java)

    }

    private fun createSSLSocketFactory(): SSLSocketFactory {
        val sslContext = SSLContext.getInstance("TLS")
        sslContext.init(null, arrayOf(CustomTrustManager()), SecureRandom())
        return sslContext.socketFactory
    }

}
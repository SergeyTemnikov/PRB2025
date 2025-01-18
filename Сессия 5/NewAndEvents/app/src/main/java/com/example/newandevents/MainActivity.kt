package com.example.newandevents


import android.os.Bundle
import android.widget.Button
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat
import androidx.navigation.fragment.NavHostFragment


class MainActivity : AppCompatActivity() {


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        setContentView(R.layout.activity_main)

        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main)) { v, insets ->
            val systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars())
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom)
            insets
        }


        val navHostFragment = supportFragmentManager.findFragmentById(R.id.nav_host_fragment) as NavHostFragment
        val navController = navHostFragment.navController

        val buttonNews = findViewById<Button>(R.id.buttonNews)
        buttonNews.isEnabled = true
        val buttonEvents = findViewById<Button>(R.id.buttonEvents)
        buttonEvents.isEnabled = false

        buttonNews.setOnClickListener(){
            buttonNews.isEnabled = false
            buttonEvents.isEnabled = true
            navController.navigate(R.id.action_eventsListFragment_to_newsListFragment)
        }
        buttonEvents.setOnClickListener(){
            buttonNews.isEnabled = true
            buttonEvents.isEnabled = false
            navController.navigate(R.id.action_newsListFragment_to_eventsListFragment)
        }
    }



}


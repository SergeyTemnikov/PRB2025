package com.example.newandevents

import android.os.Bundle
import android.util.Log
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.lifecycle.MutableLiveData
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class EventsListFragment : Fragment() {

    private var _events: MutableLiveData<List<Event>>? = MutableLiveData<List<Event>>()


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val view =  inflater.inflate(R.layout.fragment_events_list, container, false)

        val list = view.findViewById<RecyclerView>(R.id.list_events)

        getEvents()
        Log.d("MainActivity", "Users: ${_events?.value}")
        val adapter = EventsAdapter(requireContext(), _events?.value?: listOf())
        list.adapter = adapter
        val manager = LinearLayoutManager(requireContext())
        list.layoutManager = manager

        _events?.observe(viewLifecycleOwner) { events ->
            events?.let {
                val sortedEvents = it.sortedByDescending { event -> event.getParseDate() }
                adapter.updateList(sortedEvents)
            }
        }
        return view
    }

    fun getEvents()
    {
        RetrofitClient.instance.getEvents()
            .enqueue( object: Callback<List<Event>> {
                override fun onResponse(call: Call<List<Event>>, response: Response<List<Event>>) {
                    if (response.isSuccessful) {
                        _events?.value = response.body()
                        Log.d("MainActivity-2", "Users: ${_events?.value?.get(0)?.getTitle()}")
                    } else {
                        Log.e("MainActivity", "Error: ${response.code()}")
                    }
                }
                override fun onFailure(call: Call<List<Event>>, t: Throwable) {
                    Log.e("MainActivity", "Failure: ${t.message}")
                }
            })
    }



}
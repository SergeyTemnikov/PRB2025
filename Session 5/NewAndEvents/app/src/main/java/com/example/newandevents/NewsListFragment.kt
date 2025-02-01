package com.example.newandevents

import android.content.res.Resources
import android.os.Bundle
import android.util.Log
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.lifecycle.MutableLiveData
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import androidx.viewpager2.widget.CompositePageTransformer
import androidx.viewpager2.widget.MarginPageTransformer
import androidx.viewpager2.widget.ViewPager2
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class NewsListFragment : Fragment() {

    private var _news: MutableLiveData<List<News>>? = MutableLiveData<List<News>>()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val view = inflater.inflate(R.layout.fragment_news_list, container, false)

        val list = view.findViewById<ViewPager2>(R.id.list_news)

        list.apply {
            clipChildren = false
            clipToPadding = false
            offscreenPageLimit = 3
            (getChildAt(0) as RecyclerView).overScrollMode =
                RecyclerView.OVER_SCROLL_NEVER
        }

        getNews()
        Log.d("MainActivity", "Users: ${_news?.value}")
        val adapter = NewsAdapter(requireContext(), _news?.value?: listOf())
        list.adapter = adapter

        val compositePageTransformer = CompositePageTransformer()
        compositePageTransformer.addTransformer(MarginPageTransformer((40 * Resources.getSystem().displayMetrics.density).toInt()))
        list.setPageTransformer(compositePageTransformer)

        _news?.observe(viewLifecycleOwner){ it ->
            it?.let {
                adapter.updateList(it)
            }
        }

        return view
    }


    fun getNews()
    {
        RetrofitClient.instance.getNews()
            .enqueue( object: Callback<List<News>> {
                override fun onResponse(call: Call<List<News>>, response: Response<List<News>>) {
                    if (response.isSuccessful) {
                        _news?.value = response.body()
                        Log.d("MainActivity-2", "Users: ${_news?.value?.get(0)?.getTitle()}")
                    } else {
                        Log.e("MainActivity", "Error: ${response.code()}")
                    }
                }

                override fun onFailure(call: Call<List<News>>, t: Throwable) {
                    Log.e("MainActivity", "Failure: ${t.message}")
                }

            })
    }
}
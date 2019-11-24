package com.uaic.gaitauthentication.helpers;

import android.os.AsyncTask;

import androidx.lifecycle.MutableLiveData;

import java.io.IOException;

import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.Response;

public class AsyncTaskHttpCall extends AsyncTask<Void, Void, Result> {

    private final Request request;
    private final MutableLiveData<Result> liveData;

    public AsyncTaskHttpCall(Request request, MutableLiveData<Result> liveData) {
        this.request = request;
        this.liveData = liveData;
    }

    @Override
    protected Result doInBackground(Void... voids) {
        try {
            OkHttpClient client = new OkHttpClient();
            Response response = client.newCall(request).execute();

            if (response.isSuccessful()) {
                return new Result.Success(response.body().string());
            }

            return new Result.Error(new Exception(response.body().string()));

        } catch (IOException e) {
            e.printStackTrace();
        }

        return new Result.Error(new Exception("Could not connect to server!"));
    }

    @Override
    protected void onPostExecute(Result result) {
        super.onPostExecute(result);
        liveData.setValue(result);
    }
}


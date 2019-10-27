package com.uaic.gaitauthentication.helpers;

import android.app.Activity;
import android.content.Context;
import android.os.AsyncTask;
import android.widget.Toast;

import java.io.IOException;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.Future;

import okhttp3.Response;

public abstract class AsyncTaskHttpCall extends AsyncTask<Future<Response>, Void, Result> {

    protected Activity activity;

    public AsyncTaskHttpCall(Activity activity) {
        this.activity = activity;
    }

    @Override
    protected Result doInBackground(Future<Response>... futures) {
        try {
            Response response = futures[0].get();
            if (response.isSuccessful()) {
                return new Result.Success(response.body().string());
            }

            return new Result.Error(new Exception(response.body().string()));

        } catch (ExecutionException e) {
            e.printStackTrace();
        } catch (InterruptedException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }

        return new Result.Error(new Exception("Could not connect to server!"));
    }
}


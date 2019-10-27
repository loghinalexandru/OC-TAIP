package com.uaic.gaitauthentication.helpers;

import android.os.Build;

import androidx.annotation.RequiresApi;

import java.io.IOException;
import java.util.concurrent.CompletableFuture;

import okhttp3.Call;
import okhttp3.Callback;
import okhttp3.Response;

@RequiresApi(api = Build.VERSION_CODES.N)
public class OkHttpResponseFuture implements Callback {
    public final CompletableFuture<Response> future = new CompletableFuture<>();

    public OkHttpResponseFuture() {
    }

    @RequiresApi(api = Build.VERSION_CODES.N)
    @Override public void onFailure(Call call, IOException e) {
        future.completeExceptionally(e);
    }

    @RequiresApi(api = Build.VERSION_CODES.N)
    @Override public void onResponse(Call call, Response response) throws IOException {
        future.complete(response);
    }
}
package com.uaic.gaitauthentication.data;

import android.os.Build;

import androidx.annotation.RequiresApi;

import com.google.gson.Gson;
import com.uaic.gaitauthentication.helpers.Constants;
import com.uaic.gaitauthentication.helpers.OkHttpResponseFuture;
import com.uaic.gaitauthentication.data.model.RegisterModel;

import java.util.concurrent.Future;

import okhttp3.Call;
import okhttp3.MediaType;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.RequestBody;
import okhttp3.Response;

public class RegisterDataSource {

    private final Gson jsonParser;

    public RegisterDataSource(){
        jsonParser = new Gson();
    }

    public Future<Response> register(RegisterModel model) {
        OkHttpClient client = new OkHttpClient();
        Request registerRequest = createRequest(model, Constants.registerEndpoint);

        return makeRequest(client, registerRequest);
    }

    private Request createRequest(RegisterModel model, String endpoint){
        MediaType JSON = MediaType.parse("application/json; charset=utf-8");
        RequestBody requestBody = RequestBody.create(JSON, jsonParser.toJson(model));

        return new Request
                .Builder()
                .post(requestBody)
                .url(endpoint)
                .build();
    }

    @RequiresApi(api = Build.VERSION_CODES.N)
    private Future<Response> makeRequest(OkHttpClient client, Request request) {
        Call call = client.newCall(request);

        OkHttpResponseFuture result = new OkHttpResponseFuture();

        call.enqueue(result);

        return result.future;
    }
}

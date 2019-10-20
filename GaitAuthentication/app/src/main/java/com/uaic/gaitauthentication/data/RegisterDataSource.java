package com.uaic.gaitauthentication.data;

import android.util.Log;

import com.google.gson.Gson;
import com.uaic.gaitauthentication.data.model.RegisterModel;
import java.io.IOException;

import okhttp3.Call;
import okhttp3.Callback;
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

    public Result register(RegisterModel model) {

        try {
            OkHttpClient client = new OkHttpClient();
            Request registerRequest = createRequest(model, Constants.registerEndpoint);

            client.newCall(registerRequest).enqueue(new Callback() {
                @Override
                public void onFailure(Call call, IOException e) {
                    Log.d("FAIL", e.toString());
                }

                @Override
                public void onResponse(Call call, Response response) throws IOException {
                    Log.d("SUCCES", response.body().string());
                }
            });

        } catch (Exception e) {
            return new Result.Error(new IOException("Error when signing up", e));
        }

        return new Result.Error();
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
}

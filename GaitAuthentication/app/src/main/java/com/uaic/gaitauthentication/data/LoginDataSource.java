package com.uaic.gaitauthentication.data;

import android.os.Build;
import android.util.Log;

import androidx.annotation.RequiresApi;

import com.google.gson.Gson;
import com.uaic.gaitauthentication.data.model.LoggedInUser;
import com.uaic.gaitauthentication.data.model.LoginModel;
import com.uaic.gaitauthentication.helpers.Constants;
import com.uaic.gaitauthentication.helpers.OkHttpResponseFuture;
import com.uaic.gaitauthentication.helpers.Result;

import java.io.IOException;
import java.util.concurrent.Future;

import okhttp3.Call;
import okhttp3.Callback;
import okhttp3.MediaType;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.RequestBody;
import okhttp3.Response;

/**
 * Class that handles authentication w/ login credentials and retrieves user information.
 */
public class LoginDataSource {

    private final Gson jsonParser;

    public LoginDataSource() {
        jsonParser = new Gson();
    }

    @RequiresApi(api = Build.VERSION_CODES.N)
    public Future<Response> login(LoginModel model) {
        OkHttpClient client = new OkHttpClient();
        Request loginRequest = createRequest(model, Constants.loginEndpoint);

        return makeRequest(client, loginRequest);

    }

    public void logout() {
        // TODO: revoke authentication
    }

    private Request createRequest(LoginModel model, String endpoint) {
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

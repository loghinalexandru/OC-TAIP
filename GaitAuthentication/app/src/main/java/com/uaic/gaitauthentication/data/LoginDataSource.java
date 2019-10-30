package com.uaic.gaitauthentication.data;

import androidx.lifecycle.MutableLiveData;

import com.google.gson.Gson;
import com.uaic.gaitauthentication.data.model.LoginModel;
import com.uaic.gaitauthentication.helpers.AsyncTaskHttpCall;
import com.uaic.gaitauthentication.helpers.Constants;
import com.uaic.gaitauthentication.helpers.Result;

import okhttp3.MediaType;
import okhttp3.Request;
import okhttp3.RequestBody;

/**
 * Class that handles authentication w/ login credentials and retrieves user information.
 */
public class LoginDataSource {

    private final Gson jsonParser;
    private final MutableLiveData<Result> result;

    public LoginDataSource(MutableLiveData<Result> result) {
        jsonParser = new Gson();
        this.result = result;
    }

    public void login(LoginModel model) {
        Request registerRequest = createRequest(model, Constants.loginEndpoint);
        new AsyncTaskHttpCall(registerRequest, result).execute();
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
}

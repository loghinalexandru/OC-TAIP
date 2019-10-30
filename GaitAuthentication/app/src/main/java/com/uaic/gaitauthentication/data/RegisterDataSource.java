package com.uaic.gaitauthentication.data;

import androidx.lifecycle.MutableLiveData;

import com.google.gson.Gson;
import com.uaic.gaitauthentication.helpers.AsyncTaskHttpCall;
import com.uaic.gaitauthentication.helpers.Constants;
import com.uaic.gaitauthentication.data.model.RegisterModel;
import com.uaic.gaitauthentication.helpers.Result;

import okhttp3.MediaType;
import okhttp3.Request;
import okhttp3.RequestBody;

public class RegisterDataSource {

    private final Gson jsonParser;
    private final MutableLiveData<Result> result;

    public RegisterDataSource(MutableLiveData<Result> liveData) {
        jsonParser = new Gson();
        result = liveData;
    }

    public void register(RegisterModel model) {
        Request registerRequest = createRequest(model, Constants.registerEndpoint);
        new AsyncTaskHttpCall(registerRequest, result).execute();
    }

    private Request createRequest(RegisterModel model, String endpoint) {
        MediaType JSON = MediaType.parse("application/json; charset=utf-8");
        RequestBody requestBody = RequestBody.create(JSON, jsonParser.toJson(model));

        return new Request
                .Builder()
                .post(requestBody)
                .url(endpoint)
                .build();
    }
}

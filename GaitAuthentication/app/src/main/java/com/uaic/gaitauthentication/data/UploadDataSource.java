package com.uaic.gaitauthentication.data;


import androidx.lifecycle.MutableLiveData;

import com.uaic.gaitauthentication.helpers.AsyncTaskHttpCall;
import com.uaic.gaitauthentication.helpers.Constants;
import com.uaic.gaitauthentication.helpers.Result;

import java.io.File;

import okhttp3.MediaType;
import okhttp3.MultipartBody;
import okhttp3.Request;
import okhttp3.RequestBody;

public class UploadDataSource {

    private final MutableLiveData<Result> result;
    private final String token;

    public UploadDataSource(MutableLiveData<Result> liveData, String token) {
        this.result = liveData;
        this.token = token;
    }

    public void upload(File fileToUpload) {
        Request uploadRequest = createRequest(fileToUpload, Constants.uploadEndpoint);
        new AsyncTaskHttpCall(uploadRequest, result).execute();
    }

    private Request createRequest(File file, String endpoint) {
        MediaType textCsv = MediaType.parse("text/csv");
        RequestBody requestBody = new MultipartBody.Builder().setType(MultipartBody.FORM)
                .addFormDataPart("CsvFile", file.getName(), RequestBody.create(textCsv, file))
                .build();

        return new Request
                .Builder()
                .post(requestBody)
                .addHeader("Authorization", "Bearer " + token)
                .url(endpoint)
                .build();
    }
}

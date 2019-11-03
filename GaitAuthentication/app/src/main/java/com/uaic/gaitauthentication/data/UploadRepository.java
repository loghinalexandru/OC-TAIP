package com.uaic.gaitauthentication.data;

import androidx.lifecycle.LiveData;
import androidx.lifecycle.MutableLiveData;

import com.uaic.gaitauthentication.helpers.Result;

import java.io.File;

public class UploadRepository {

    private static volatile UploadRepository instance;

    private final UploadDataSource dataSource;
    private final MutableLiveData<Result> result;

    private UploadRepository(String token) {
        this.result = new MutableLiveData<>();
        this.dataSource = new UploadDataSource(result, token);
    }

    public static UploadRepository getInstance(String token) {
        if (instance == null) {
            instance = new UploadRepository(token);
        }

        return instance;
    }

    public void upload(File fileToUpload) {
        dataSource.upload(fileToUpload);
    }

    public LiveData<Result> getResult(){
        return result;
    }

}

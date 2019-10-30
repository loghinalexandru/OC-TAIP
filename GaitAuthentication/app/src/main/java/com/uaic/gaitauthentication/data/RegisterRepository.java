package com.uaic.gaitauthentication.data;

import androidx.lifecycle.LiveData;
import androidx.lifecycle.MutableLiveData;

import com.uaic.gaitauthentication.data.model.RegisterModel;
import com.uaic.gaitauthentication.helpers.Result;

public class RegisterRepository {

    private static volatile RegisterRepository instance;

    private final RegisterDataSource dataSource;
    private final MutableLiveData<Result> result;

    private RegisterRepository() {
        this.result = new MutableLiveData<>();
        this.dataSource = new RegisterDataSource(result);
    }

    public static RegisterRepository getInstance() {
        if (instance == null) {
            instance = new RegisterRepository();
        }

        return instance;
    }

    public void register(RegisterModel model) {
        dataSource.register(model);
    }

    public LiveData<Result> getResult(){
        return result;
    }
}

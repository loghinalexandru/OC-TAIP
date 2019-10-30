package com.uaic.gaitauthentication.data;

import androidx.lifecycle.MutableLiveData;

import com.uaic.gaitauthentication.data.model.RegisterModel;
import com.uaic.gaitauthentication.helpers.Result;

public class RegisterRepository {

    private static volatile RegisterRepository instance;

    private final RegisterDataSource dataSource;

    private RegisterRepository(RegisterDataSource dataSource) {
        this.dataSource = dataSource;
    }

    public static RegisterRepository getInstance(RegisterDataSource dataSource) {
        if (instance == null) {
            instance = new RegisterRepository(dataSource);
        }

        return instance;
    }

    public void register(RegisterModel model) {
        dataSource.register(model);
    }
}

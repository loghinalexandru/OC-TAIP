package com.uaic.gaitauthentication.data;

import com.uaic.gaitauthentication.data.model.RegisterModel;
import java.util.concurrent.Future;

import okhttp3.Response;

public class RegisterRepository {

    private static volatile RegisterRepository instance;

    private RegisterDataSource dataSource;

    private RegisterRepository(RegisterDataSource dataSource){
        this.dataSource = dataSource;
    }

    public static RegisterRepository getInstance(RegisterDataSource dataSource) {
        if (instance == null) {
            instance = new RegisterRepository(dataSource);
        }

        return instance;
    }

    public Future<Response> register(RegisterModel model){
       return dataSource.register(model);
    }
}

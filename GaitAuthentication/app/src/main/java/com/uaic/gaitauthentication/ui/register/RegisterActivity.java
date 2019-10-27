package com.uaic.gaitauthentication.ui.register;

import androidx.lifecycle.ViewModelProviders;

import android.os.Bundle;

import androidx.appcompat.app.AppCompatActivity;

import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ProgressBar;

import com.uaic.gaitauthentication.R;
import com.uaic.gaitauthentication.helpers.AsyncTaskHttpCall;
import com.uaic.gaitauthentication.helpers.AsyncTaskRegister;

import java.util.concurrent.Future;

import okhttp3.Response;

public class RegisterActivity extends AppCompatActivity {

    private RegisterViewModel registerViewModel;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_register);
        registerViewModel = ViewModelProviders.of(this, new RegisterViewModelFactory())
                .get(RegisterViewModel.class);

        final EditText usernameEditText = findViewById(R.id.username);
        final EditText passwordEditText = findViewById(R.id.password);
        final EditText emailEditText = findViewById(R.id.email);
        final Button register = findViewById(R.id.register);
        final ProgressBar loadingProgressBar = findViewById(R.id.loading);

        register.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                loadingProgressBar.setVisibility(View.VISIBLE);
                Future<Response> future = registerViewModel.register(usernameEditText.getText().toString(), passwordEditText.getText().toString(), emailEditText.getText().toString());
                new AsyncTaskRegister(RegisterActivity.this).execute(future);
            }
        });

    }

}

package com.uaic.gaitauthentication.ui.login;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Build;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ProgressBar;
import android.widget.Toast;

import androidx.annotation.Nullable;
import androidx.annotation.RequiresApi;
import androidx.appcompat.app.AppCompatActivity;
import androidx.lifecycle.Observer;
import androidx.lifecycle.ViewModelProviders;

import com.auth0.android.jwt.JWT;
import com.uaic.gaitauthentication.R;
import com.uaic.gaitauthentication.helpers.Result;
import com.uaic.gaitauthentication.ui.MainActivity;
import com.uaic.gaitauthentication.ui.register.RegisterActivity;

import static android.preference.PreferenceManager.getDefaultSharedPreferences;

public class LoginActivity extends AppCompatActivity {

    private LoginViewModel loginViewModel;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        checkAuthentication();

        loginViewModel = ViewModelProviders.of(this, new LoginViewModelFactory())
                .get(LoginViewModel.class);

        final EditText usernameEditText = findViewById(R.id.username);
        final EditText passwordEditText = findViewById(R.id.password);
        final Button loginButton = findViewById(R.id.register);
        final Button signupButton = findViewById(R.id.signup);
        final ProgressBar loadingProgressBar = findViewById(R.id.loading);

        loginViewModel.getLoginFormState().observe(this, new Observer<LoginFormState>() {
            @Override
            public void onChanged(@Nullable LoginFormState loginFormState) {
                if (loginFormState == null) {
                    return;
                }
                loginButton.setEnabled(loginFormState.isDataValid());
                if (loginFormState.getUsernameError() != null) {
                    usernameEditText.setError(getString(loginFormState.getUsernameError()));
                }
                if (loginFormState.getPasswordError() != null) {
                    passwordEditText.setError(getString(loginFormState.getPasswordError()));
                }
            }
        });

        TextWatcher afterTextChangedListener = new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {
            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
            }

            @Override
            public void afterTextChanged(Editable s) {
                loginViewModel.loginDataChanged(usernameEditText.getText().toString(),
                        passwordEditText.getText().toString());
            }
        };

        usernameEditText.addTextChangedListener(afterTextChangedListener);
        passwordEditText.addTextChangedListener(afterTextChangedListener);

        loginButton.setOnClickListener(new View.OnClickListener() {
            @RequiresApi(api = Build.VERSION_CODES.N)
            @Override
            public void onClick(View v) {
                loadingProgressBar.setVisibility(View.VISIBLE);
                loginButton.setEnabled(false);
                loginViewModel.login(usernameEditText.getText().toString(), passwordEditText.getText().toString());
            }
        });

        signupButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent registerIntent = new Intent(getApplicationContext(), RegisterActivity.class);
                startActivity(registerIntent);
            }
        });

        loginViewModel.getResult().observe(this, new Observer<Result>() {
            @Override
            public void onChanged(Result result) {
                if (result instanceof Result.Success) {
                    Intent mainActivity = new Intent(getApplication(), MainActivity.class);

                    storeToken(((Result.Success) result).getData().toString());

                    startActivity(mainActivity);
                    finish();
                } else {
                    loginButton.setEnabled(true);
                    findViewById(R.id.loading).setVisibility(View.INVISIBLE);
                    Toast.makeText(getApplication(), result.toString(), Toast.LENGTH_LONG).show();
                }
            }
        });
    }

    private void storeToken(String token) {
        SharedPreferences.Editor preferences = getDefaultSharedPreferences(getApplicationContext()).edit();
        preferences.putString("token", token);
        preferences.commit();
    }

    private void checkAuthentication() {
        SharedPreferences preferences = getDefaultSharedPreferences(getApplicationContext());
        String token = preferences.getString("token", null);

        if (token != null && !isExpired(token)) {
            Intent mainActivity = new Intent(getApplication(), MainActivity.class);
            startActivity(mainActivity);
            finish();
        }
    }

    public boolean isExpired(String token) {
        JWT jwt = new JWT(token);

        return jwt.isExpired(0);
    }
}

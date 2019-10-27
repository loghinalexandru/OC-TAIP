package com.uaic.gaitauthentication.helpers;

import android.app.Activity;
import android.content.Intent;
import android.view.View;
import android.widget.Toast;

import com.uaic.gaitauthentication.MainActivity;
import com.uaic.gaitauthentication.R;

public class AsyncTaskLogin extends AsyncTaskHttpCall {
    public AsyncTaskLogin(Activity activity) {
        super(activity);
    }

    @Override
    protected void onPostExecute(Result response) {
        super.onPostExecute(response);
        if(response instanceof Result.Success){
            Intent mainActivity = new Intent(activity, MainActivity.class);
            Toast.makeText(activity, response.toString(), Toast.LENGTH_LONG).show();
            activity.startActivity(mainActivity);
            activity.finish();
        }
        else{
            activity.findViewById(R.id.loading).setVisibility(View.INVISIBLE);
            Toast.makeText(activity, response.toString(), Toast.LENGTH_LONG).show();
        }
    }
}

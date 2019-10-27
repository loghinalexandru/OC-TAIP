package com.uaic.gaitauthentication.helpers;

import android.app.Activity;
import android.view.View;
import android.widget.Toast;

import com.uaic.gaitauthentication.R;

public class AsyncTaskRegister extends AsyncTaskHttpCall {

    public AsyncTaskRegister(Activity activity) {
        super(activity);
    }

    @Override
    protected void onPostExecute(Result response) {
        super.onPostExecute(response);
        if(response instanceof Result.Success){
            activity.finish();
        }
        else{
            activity.findViewById(R.id.loading).setVisibility(View.INVISIBLE);
            Toast.makeText(activity, response.toString(), Toast.LENGTH_LONG).show();
        }
    }
}

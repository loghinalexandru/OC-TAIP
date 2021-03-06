package com.uaic.gaitauthentication.helpers;

public class Constants {
    public static final String authorityBaseUrl = "http://192.168.43.152:45456";
    public static final String registerEndpoint = authorityBaseUrl + "/Authority/register";
    public static final String loginEndpoint = authorityBaseUrl + "/Authority/token";

    public static  String storageServerBaseUrl = "http://3b8954c5.ngrok.io";
    public static  String uploadEndpoint = storageServerBaseUrl + "/api/storage/upload";

    public static final String START_SERVICE = "START";
    public static final String STOP_SERVICE = "STOP";

    public static void setStorageServerBaseUrl(String baseUrl){
        storageServerBaseUrl = baseUrl;
        uploadEndpoint = storageServerBaseUrl + "/api/storage/upload";
    }
}

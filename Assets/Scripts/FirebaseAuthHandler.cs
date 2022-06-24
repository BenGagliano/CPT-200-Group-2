using System.Collections;
using System.Collections.Generic;
using Proyecto26;
using UnityEngine;

public static class FirebaseAuthHandler
{
    private const string ApiKey = "AIzaSyCGVQH7iEO5rVZN0Pa9NxkeAL2oW-mmDnM";

    public static void SignInWithToken(string idToken, string providerId)
    {
        var payLoad = $"{{\"postBody\":\"id_token={idToken}&providerId={providerId}\",\"requestUri\":\"http://localhost\",\"returnIdpCredential\":true,\"returnSecureToken\":true}}";
        RestClient.Post($"https://identitytoolkit.googleapis.com/v1/accounts:signInWithIdp?key={ApiKey}", payLoad).Then(response =>
        {
            Debug.Log(response.Text);
        });
    }
}

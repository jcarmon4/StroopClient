using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using StroopTest.Models;

public class UserService : MonoBehaviour {
    private string url = "https://stroopapi.azurewebsites.net/api/User";

    public Text Message;

    public void CreateUser(User user){
        StartCoroutine(PostRequest(user));
    }

    IEnumerator PostRequest(User user)
    {
        string jsonData = JsonUtility.ToJson(user);
        Debug.Log("jsonData: " + jsonData);
        using (UnityWebRequest request = UnityWebRequest.Put(url, jsonData))
        {
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Message.text = "ERROR "+ request.downloadHandler.text;
                Debug.Log(request.error);
            }
            else
            {
                Message.text = "Usuario registrado";
                Debug.Log("Usuario registrado");
                Debug.Log(request.downloadHandler.text);
            }
        }
    }
}
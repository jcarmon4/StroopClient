using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ButtonBlueCall : MonoBehaviour
{
    public Text message;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetRequest());
    }

    public void OnClick()
    {
        StartCoroutine(PostRequest());
    }

    IEnumerator GetRequest()
    {
        string url = "https://stroopapi.azurewebsites.net/api/RecordActivity";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = url.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                message.text = webRequest.downloadHandler.text;
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            }
        }
    }

    IEnumerator PostRequest()
    {
        string url = "https://stroopapi.azurewebsites.net/api/RecordActivity";
        
        RecordActivity recordActivity = new RecordActivity();
        recordActivity.IdUser = 1;
        recordActivity.Stage = "1";
        recordActivity.Status = "Ok";
        recordActivity.Text = "Rojo";
        recordActivity.Ink = "Rojo";
        string jsonData = JsonUtility.ToJson(recordActivity);
        //jsonData = "{\"idUser\": 1,\"stage\": \"1\",\"status\": \"Fail\",\"text\": \"Azul\",\"ink\": \"Verde\"}";
        Debug.Log("jsonData: " + jsonData);

        using (UnityWebRequest request = UnityWebRequest.Put(url, jsonData))
        {
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                message.text = request.downloadHandler.text;
                Debug.Log(request.error);
            }
            else
            {
                message.text = request.downloadHandler.text;
                Debug.Log("OK POST");
            }
        }
    }

    public IEnumerator PostMethod(){
        RecordActivity recordActivity = new RecordActivity();
        recordActivity.Status = "ok";
        recordActivity.Text = "rojo";
        recordActivity.Ink = "rojo";
        recordActivity.Time = DateTime.Now.ToString("MM/dd/yyyy H:mm:ss");
        
        string value = "value0";
        string jsonData = JsonUtility.ToJson(value);
        
        WWWForm formData = new WWWForm();
        formData.AddField("value", "WWWform");

        string url = "https://strooptest4.azurewebsites.net/api/values";

        using (UnityWebRequest request = UnityWebRequest.Post(url, formData))
        {
            // request.method = UnityWebRequest.kHttpVerbPOST;
            // request.SetRequestHeader("Content-Type", "application/json");
            // request.SetRequestHeader("Accept", "application/json");
            yield return request.SendWebRequest();
            if (!request.isNetworkError) //&& request.responseCode == 200)
            {
                //message.text = www.text;
                Debug.Log("Form upload complete!");
            }
            else
            {
                Debug.Log("request.error");
            }
        }
    }

    // Update is called once per frame
    IEnumerator Request()
    {
        string url = "https://strooptest4.azurewebsites.net/api/values";

        WWWForm formData = new WWWForm();
        formData.AddField("status", "ok");
        formData.AddField("text", "rojo");
        formData.AddField("ink", "rojo");
        formData.AddField("time", DateTime.Now.ToString("MM/dd/yyyy H:mm:ss"));

        using (UnityWebRequest www = UnityWebRequest.Post(url, formData))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //message.text = www.text;
                Debug.Log("Form upload complete!");
            }
        }
    }
}

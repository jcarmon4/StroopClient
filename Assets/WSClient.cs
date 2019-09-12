using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class WSClient : MonoBehaviour
{
    public Text message;

    private string url = "https://stroopapi.azurewebsites.net/api/RecordActivity";

    // Start is called before the first frame update
    // void Start()
    // {
    //     StartCoroutine(Upload());
    // }

    // // Update is called once per frame
    // IEnumerator Upload()
    // {
    //     string url = "https://strooptest4.azurewebsites.net/api/values";

    //     WWWForm formData = new WWWForm();
    //     formData.AddField("", "");
    //     formData.AddField("", "");

    //     using (UnityWebRequest www = UnityWebRequest.Post(url, formData))
    //     {
    //         yield return www.SendWebRequest();

    //         if (www.isNetworkError || www.isHttpError)
    //         {
    //             Debug.Log(www.error);
    //         }
    //         else
    //         {
    //             //message.text = www.text;
    //             Debug.Log("Form upload complete!");
    //         }
    //     }
    // }

    public void CallPostResquest(RecordActivity recordActivity){
        StartCoroutine(PostRequest(recordActivity));
    }

    IEnumerator PostRequest(RecordActivity recordActivity)
    {
        // RecordActivity recordActivity = new RecordActivity();
        // recordActivity.IdUser = 1;
        // recordActivity.Stage = "1";
        // recordActivity.Status = "Ok";
        // recordActivity.Text = "Rojo";
        // recordActivity.Ink = "Rojo";
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
}

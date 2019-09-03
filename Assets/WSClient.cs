using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class WSClient : MonoBehaviour
{
    public Text message;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Upload());
    }

    // Update is called once per frame
    IEnumerator Upload()
    {
        string url = "https://strooptest4.azurewebsites.net/api/values";

        WWWForm formData = new WWWForm();
        formData.AddField("", "");
        formData.AddField("", "");

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

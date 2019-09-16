using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using StroopTest.Models;

public class RegisterButton : MonoBehaviour
{
    private string url = "https://stroopapi.azurewebsites.net/api/User";
    public string NextScene;
    public Text FullName;
    public Text Email;
    public Dropdown Gender;
    public Text Age;
    public Text Message;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnClick(){
        User user = new User();
        user.fullname = FullName.text;
        user.email = Email.text;
        user.gender = Gender.options[Gender.value].text;
        user.age = Age.text;
        Debug.Log(user.fullname + "" + user.email + "" + user.gender + "" + user.age);
        CreateUser(user);
    }

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
                SceneManager.LoadScene(NextScene);
            }
        }
    }
}

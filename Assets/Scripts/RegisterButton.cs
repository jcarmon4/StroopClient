﻿using System.Collections;
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
    public GameObject Loading;

    // Start is called before the first frame update
    void Start()
    {
        Loading.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnClick(){
        Loading.SetActive(true);
        GetComponent<Button>().interactable = false;
        User user = new User();
        user.fullName = FullName.text;
        user.email = Email.text;
        user.gender = Gender.options[Gender.value].text;
        user.age = Age.text;
        Debug.Log(user.fullName + "" + user.email + "" + user.gender + "" + user.age);
        CreateUser(user);
    }

    public void OnBackClick(){
        SceneManager.LoadScene("MenuScene");
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
                Debug.Log(request.error);
                Message.text = "ERROR "+ request.error;
            }
            else
            {
                Message.text = "Usuario registrado";
                Debug.Log("Usuario registrado");

                PlayerPrefs.SetInt("PlusPoint1", 0);
                PlayerPrefs.SetInt("LosePoint1", 0);
                PlayerPrefs.SetInt("PlusPoint2", 0);
                PlayerPrefs.SetInt("LosePoint2", 0);
                
                string response = request.downloadHandler.text;
                Debug.Log(response);
                User User = JsonUtility.FromJson<User>(response);
                Debug.Log(User.id + " "+ User.fullName + " "+ User.email + " "+ User.age);
                PlayerPrefs.SetString("IdUser", "" + User.id);
                PlayerPrefs.SetString("fullName", User.fullName);
                SceneManager.LoadScene(NextScene);
            }
            Loading.SetActive(false);
            GetComponent<Button>().interactable = true;
        }
    }
}

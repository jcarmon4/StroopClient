﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class StopcountTimer : MonoBehaviour
{
    public Text CountdownText;
    public string NextScene;
    public Text ColorText;
    public Text PlusPointText;
    public Text LosePointText;
    public GameObject LabelContainer;
    public Text WinFailText;
    public Slider Slider;
    private float TimeRemaining;
    private const float TimerMax = 3f;

    public Text Message;

    private int PlusPoint;
    private int LosePoint;

    private (Color, string)[] Colors = {(Color.green, "verde"), (Color.red, "rojo"), (Color.blue, "azul"),
    (Color.black, "negro"), (Color.magenta, "fucsia"), (Color.yellow, "amarillo")};

    float CurrentTime = 0f;
    float StartingTime = 300f;

    int stageScene = 1;

    private (Color, string) RandomColor;
    private (Color, string) RandomText;

    private string url = "https://stroopapi.azurewebsites.net/api/RecordActivity";

    // Start is called before the first frame update
    void Start()
    {
        CurrentTime = StartingTime;
                
        Scene CurrentScene = SceneManager.GetActiveScene();
        string sceneName = CurrentScene.name;
        // Verifica la escena actual para modificar a un comportamiento coherente o incoherente.
        if (sceneName == "IncoherentScene"){
            stageScene = 2;
        }
        LabelContainer.SetActive(false);

        PlusPointText.text = "" + PlusPoint;
        LosePointText.text = "" + LosePoint;

        TimeRemaining = TimerMax;

        UpdateColorText();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentTime -= 1 * Time.deltaTime;
        TimeSpan t = TimeSpan.FromSeconds(CurrentTime);
        // Debug.Log("CurrentTime: " + t.ToString(@"mm\:ss"));
        CountdownText.text = t.ToString(@"mm\:ss");

        if (CurrentTime <= 0){ 
            CurrentTime = 0;
            if (stageScene == 1){
                PlayerPrefs.SetInt("PlusPoint1", PlusPoint);
                PlayerPrefs.SetInt("LosePoint1", LosePoint);
            } else {
                PlayerPrefs.SetInt("PlusPoint2", PlusPoint);
                PlayerPrefs.SetInt("LosePoint2", LosePoint);
            }
            SceneManager.LoadScene(NextScene);
        }
        Slider.value = CalculateSliderValue();

        if (TimeRemaining > 0){
            TimeRemaining -= Time.deltaTime;
            // Verifica que si pasaron medio segundo, debe desaparecer el mensaje
            if (TimeRemaining < (TimerMax - 0.5f)){
                LabelContainer.SetActive(false);
            }
        } else {
            CheckPoint("None");
            UpdateColorText();
        }
    }

    public void CheckPoint(string ColorSelected){
        // Reset el tiempo por actividad
        TimeRemaining = TimerMax;

        RecordActivity recordActivity = new RecordActivity();
        string IdUserString = PlayerPrefs.GetString("IdUser"); 
        recordActivity.IdUser = long.Parse(IdUserString);
        recordActivity.Stage = "" + stageScene;

        if (ColorSelected == RandomColor.Item2){
            WinFailText.text = "+1";
            WinFailText.color = Color.green;
            PlusPoint++;
            PlusPointText.text = "" + PlusPoint;
            recordActivity.Status = "Ok";
        } else {
            WinFailText.text = "-1";
            WinFailText.color = Color.red;
            LosePoint++;
            LosePointText.text = "" + LosePoint;
            recordActivity.Status = "Fail";
        }
        LabelContainer.SetActive(true);
        recordActivity.Text = RandomText.Item2;
        recordActivity.Ink = RandomColor.Item2;
        recordActivity.Selected = ColorSelected;

        CallPostResquest(recordActivity);
    }

    public void UpdateColorText(){
        RandomColor = ShuffleColor();
        RandomText = RandomColor;
        ColorText.color = RandomColor.Item1;
        ColorText.text = RandomColor.Item2;
        if (stageScene == 2){
            RandomText = ShuffleColor();  
            ColorText.text = RandomText.Item2;
        } 
    }

    private (Color, string) ShuffleColor(){
        return Colors[UnityEngine.Random.Range(0, Colors.Length)];
    } 

    private float CalculateSliderValue(){
        return TimeRemaining / TimerMax;
    }

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
                Message.text = request.downloadHandler.text;
                Debug.Log(request.error);
            }
            else
            {
                Message.text = request.downloadHandler.text;
                Debug.Log("OK POST");
            }
        }
    }
}

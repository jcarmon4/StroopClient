using System;
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

    public Text Message;

    private int PlusPoint;
    private int LosePoint;

    private (Color, string)[] Colors = {(Color.green, "verde"), (Color.red, "rojo"), (Color.blue, "azul")};

    float CurrentTime = 0f;
    float StartingTime = 120f;

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

        PlusPointText.text = "" + PlusPoint;
        LosePointText.text = "" + LosePoint;

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
    }

    public void CheckPoint(string ColorSelected){
        RecordActivity recordActivity = new RecordActivity();
        recordActivity.IdUser = 1;
        recordActivity.Stage = "" + stageScene;

        if (ColorSelected == RandomColor.Item2){
            PlusPoint++;
            PlusPointText.text = "" + PlusPoint;
            recordActivity.Status = "Ok";
        } else {
            LosePoint++;
            LosePointText.text = "" + LosePoint;
            recordActivity.Status = "Fail";
        }
        recordActivity.Text = RandomText.Item2;
        recordActivity.Ink = RandomColor.Item2;

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

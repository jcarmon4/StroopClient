using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrainingController : MonoBehaviour
{
    public int stageScene;
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
    float StartingTime = 9f;

    private (Color, string) RandomColor;
    private (Color, string) RandomText;

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
                PlayerPrefs.SetInt("PlusPointTraining1", PlusPoint);
                PlayerPrefs.SetInt("LosePointTraining1", LosePoint);
            } else {
                PlayerPrefs.SetInt("PlusPointTraining2", PlusPoint);
                PlayerPrefs.SetInt("LosePointTraining2", LosePoint);
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
}

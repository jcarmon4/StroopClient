using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StopcountTimer : MonoBehaviour
{
    public Text CountdownText;
    public string NextScene;
    public Text ColorText;

    private (Color, string)[] Colors = {(Color.green, "verde"), (Color.red, "rojo"), (Color.blue, "azul")};

    float CurrentTime = 0f;
    float StartingTime = 120f;

    int stageScene = 1;

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

        UpdateColorText();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentTime -= 1 * Time.deltaTime;
        TimeSpan t = TimeSpan.FromSeconds(CurrentTime);
        Debug.Log("CurrentTime: " + t.ToString(@"mm\:ss"));
        CountdownText.text = t.ToString(@"mm\:ss");

        if (CurrentTime <= 0){ 
            CurrentTime = 0;
            SceneManager.LoadScene(NextScene);
        }
    }

    public void UpdateColorText(){
        (Color, string) ColorSelected = RandomColor();
        ColorText.color = ColorSelected.Item1;
        ColorText.text = ColorSelected.Item2;
        if (stageScene == 2){
            ColorText.text = RandomColor().Item2;  
        } 
    }

    private (Color, string) RandomColor(){
        return Colors[UnityEngine.Random.Range(0, Colors.Length)];
    } 
}

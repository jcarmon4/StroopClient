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

    // Start is called before the first frame update
    void Start()
    {
        CurrentTime = StartingTime;
        (Color, string) ColorSelected = RandomColor();
        ColorText.color = ColorSelected.Item1;

        Scene CurrentScene = SceneManager.GetActiveScene();
        string sceneName = CurrentScene.name;
        // Verifica la escena actual para modificar a un comportamiento coherente o incoherente.
        if (sceneName == "SampleScene"){
            ColorText.text = ColorSelected.Item2;
        } else if (sceneName == "IncoherentScene"){
            ColorText.text = RandomColor().Item2;
        }
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

    private (Color, string) RandomColor(){
        return Colors[UnityEngine.Random.Range(0, Colors.Length)];
    } 
}

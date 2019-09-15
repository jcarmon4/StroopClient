using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultCoherentScene : MonoBehaviour
{
    public Text CountdownText;
    public string NextScene;
    public Text PlusPointText;
    public Text LosePointText;
    float CurrentTime = 0f;
    public float StartingTime;

    // Start is called before the first frame update
    void Start()
    {
        CurrentTime = StartingTime;
        int PlusPoint1 = PlayerPrefs.GetInt("PlusPoint1");
        int LosePoint1 = PlayerPrefs.GetInt("LosePoint1");
        PlusPointText.text = "" + PlusPoint1;
        LosePointText.text = "" + LosePoint1;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentTime -= 1 * Time.deltaTime;
        TimeSpan t = TimeSpan.FromSeconds(CurrentTime);
        CountdownText.text = t.ToString(@"mm\:ss");

        if (CurrentTime <= 0){ 
            CurrentTime = 0;
            SceneManager.LoadScene(NextScene);
        }
    }
}

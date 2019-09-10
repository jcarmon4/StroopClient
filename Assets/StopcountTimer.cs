﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StopcountTimer : MonoBehaviour
{
    public Text CountdownText;
    public string NextScene;

    float CurrentTime = 0f;
    float StartingTime = 120f;

    // Start is called before the first frame update
    void Start()
    {
        CurrentTime = StartingTime;
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartView : MonoBehaviour
{
    public string NextScene;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(){
        Debug.Log("OnClick");
        if (NextScene == "SampleScene") {
            PlayerPrefs.SetInt("PlusPoint1", 0);
            PlayerPrefs.SetInt("LosePoint1", 0);
        } else if (NextScene == "IncoherentScene") {
            PlayerPrefs.SetInt("PlusPoint2", 0);
            PlayerPrefs.SetInt("LosePoint2", 0);
        } else if (NextScene == "CongruentTrainingScene") {
            PlayerPrefs.SetInt("PlusPointTraining1", 0);
            PlayerPrefs.SetInt("LosePointTraining1", 0);
        } else if (NextScene == "IncongruentTrainingScene") {
            PlayerPrefs.SetInt("PlusPointTraining2", 0);
            PlayerPrefs.SetInt("LosePointTraining2", 0);
        }
        SceneManager.LoadScene(NextScene);
    }

    public void OnBackClick(){
        SceneManager.LoadScene("MenuScene");
    }
}

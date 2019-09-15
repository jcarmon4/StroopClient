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
        PlayerPrefs.SetInt("PlusPoint1", 0);
        PlayerPrefs.SetInt("LosePoint1", 0);
        PlayerPrefs.SetInt("PlusPoint2", 0);
        PlayerPrefs.SetInt("LosePoint2", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(){
        SceneManager.LoadScene(NextScene);
    }
}

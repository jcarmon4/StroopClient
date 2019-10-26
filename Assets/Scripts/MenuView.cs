using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuView : MonoBehaviour
{
    public string NextScene;
    public Text FullNameText;

    // Start is called before the first frame update
    void Start()
    {
        string fullName = PlayerPrefs.GetString("fullName");
        if (fullName != ""){
            FullNameText.text = fullName;
        } else {
            FullNameText.text = "Bienvenido, registrate!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickRegister(){
        SceneManager.LoadScene(NextScene);
    }

    public void OnClickCoherent(){
        SceneManager.LoadScene(NextScene);
    }

    public void OnClickIncoherent(){
        SceneManager.LoadScene(NextScene);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultTrainingView : MonoBehaviour
{
    public Text PlusPointText1;
    public Text LosePointText1;
    public Text PlusPointText2;
    public Text LosePointText2;

    // Start is called before the first frame update
    void Start()
    {
        int PlusPoint1 = PlayerPrefs.GetInt("PlusPointTraining1");
        int LosePoint1 = PlayerPrefs.GetInt("LosePointTraining1");
        int PlusPoint2 = PlayerPrefs.GetInt("PlusPointTraining2");
        int LosePoint2 = PlayerPrefs.GetInt("LosePointTraining2");
        PlusPointText1.text = "" + PlusPoint1;
        LosePointText1.text = "" + LosePoint1;
        PlusPointText2.text = "" + PlusPoint2;
        LosePointText2.text = "" + LosePoint2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

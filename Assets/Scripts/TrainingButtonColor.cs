using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingButtonColor : MonoBehaviour
{
    public TrainingController Controller;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void YellowOnClick(){
        Controller.CheckPoint("amarillo");
        Controller.UpdateColorText();
    }

    public void BlueOnClick(){
        Controller.CheckPoint("azul");
        Controller.UpdateColorText();
    }

    public void RedOnClick(){
        Controller.CheckPoint("rojo");
        Controller.UpdateColorText();
    }

    public void GreenOnClick(){
        Controller.CheckPoint("verde");
        Controller.UpdateColorText();
    }

    public void FucsiaOnClick(){
        Controller.CheckPoint("fucsia");
        Controller.UpdateColorText();
    }

    public void BlackOnClick(){
        Controller.CheckPoint("negro");
        Controller.UpdateColorText();
    }
}

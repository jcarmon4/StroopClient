using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackButton : MonoBehaviour
{
    public StopcountTimer Controller;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(){
        Controller.CheckPoint("negro");
        Controller.UpdateColorText();
    }
}

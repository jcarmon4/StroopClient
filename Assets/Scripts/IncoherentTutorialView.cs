using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncoherentTutorialView : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        var player = GetComponent<UnityEngine.Video.VideoPlayer>();
        player.url = System.IO.Path.Combine(Application.streamingAssetsPath,"CongruentTutorial.mp4");
        player.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

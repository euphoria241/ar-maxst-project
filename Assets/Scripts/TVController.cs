using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TVController : MonoBehaviour
{
    public GameObject playStopButton;
    public AudioClip clickSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayStopVideo()
    {
        if (playStopButton.GetComponentInChildren<Text>().text == "Play")
        {
            GetComponent<AudioSource>().PlayOneShot(clickSound);
            playStopButton.GetComponentInChildren<Text>().text = "Stop";
            ((VideoPlayer)FindObjectOfType(typeof(VideoPlayer))).Play();
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(clickSound);
            playStopButton.GetComponentInChildren<Text>().text = "Play";
            ((VideoPlayer)FindObjectOfType(typeof(VideoPlayer))).Stop();
        }
    }
}

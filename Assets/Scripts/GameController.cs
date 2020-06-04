using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;

public class GameController : NetworkBehaviour 
{
    public static GameController instance;
    public GameObject highLight;

    Vector3[] keys = { new Vector3(0.0785f, 0.0f, 0.014f), new Vector3(-0.102f, 0.0f, -0.0047f), new Vector3(-0.0806f, 0.0f, -0.0047f),
        new Vector3(-0.086f, 0.0f, 0.0152f), new Vector3(0.0581f, 0.0f, 0.014f), new Vector3(-0.0438f, 0.0f, 0.0152f),
            new Vector3(-0.0604f, 0.0f, -0.0051f) };
	string keysNames = "PASWORD";
    Animator animator;

    [SyncVar] int status = 0;

    [HideInInspector] public char activeChar;
    float waitMe = 0;

    private void Awake()
    { 
        instance = this;    
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (isServer) GameObject.Find("CameraBackground").SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
      /*  if (isServer && Input.GetKey(""+activeChar) && waitMe <=0){
            status++;
            waitMe = 1f;
        }*/
        if (waitMe > 0) waitMe -= Time.deltaTime;
        animator.SetInteger("letterNumber",status);
    }

    public void LetterShow(char c)
    {
        //      highLight.SetActive(true);
        int i = keysNames.IndexOf(c);
        highLight.transform.localPosition = keys[i];
    }

    public void LetterHide(char s)
    {
        //       highLight.SetActive(false);
    }

    void OnGUI()
    {

        Event e = Event.current;

        //Check the type of the current event, making sure to take in only the KeyDown of the keystroke.
        //char.IsLetter to filter out all other KeyCodes besides alphabetical.
        if (e.type == EventType.KeyDown && e.keyCode.ToString().Length == 1)
            if (e.keyCode.ToString()[0]==activeChar && waitMe <=0)
        {
            status++;
            waitMe = 1f;
        }
    }

    public void VideoPlay() {
        if (!isServer) ((VideoPlayer)FindObjectOfType(typeof(VideoPlayer))).Play();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

using System;

public class SceneButtons : MonoBehaviour
{

    public static Tuple<string, string>[] scenes =
    {   new Tuple<string, string>("Изображение", "M yMaxSTKeyboard"),
        new Tuple<string, string>("Камера конфиг", "MyCameraConfig"),
        new Tuple<string, string>("Маркеры", "MyMarkerTracker"),
        new Tuple<string, string>("Поверхность", "MyInstantTracker"),
        new Tuple<string, string>("Объекты", "MyObjectTracker"),
        new Tuple<string, string>("QR-code", "QrTracker"),
    };

    private readonly Tuple<byte, byte, byte, byte> COLORGRAY = new Tuple<byte, byte, byte, byte>(198, 202, 204, 202),
                                                COLORBLUE = new Tuple<byte, byte, byte, byte>(45, 111, 253, 255);


    GameObject[] buttons;
    public GameObject buttonPrefab;
    GameObject panel, mainButton;

    void Awake()
    {
        panel = transform.Find("Panel").gameObject;
        mainButton = transform.Find("Button").gameObject;
        buttons = new GameObject[scenes.Length];
        for (int i = 0; i < scenes.Length; i++)
        {
            buttons[i] = Instantiate(buttonPrefab, panel.transform);
            var rectTransform = buttons[i].GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector3(0, 15 + i * 55, 0);
            buttons[i].name = scenes[i].Item2;
            buttons[i].GetComponentInChildren<Text>().text = scenes[i].Item1;
            String sceneName = SceneManager.GetActiveScene().name;
            SetColor(buttons[i].transform, buttons[i].name != sceneName ? COLORGRAY : COLORBLUE);
            String s = buttons[i].name;
            buttons[i].GetComponent<Button>().onClick.AddListener(delegate
            {
                StartGameName(s);
            }); 
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void ShowButtons()
    {
        panel.SetActive(!panel.activeSelf);
    }

    void SetColor(Transform button, Tuple<byte, byte, byte, byte> color)
    {
        button.GetComponent<Image>().color = new Color32(color.Item1, color.Item2, color.Item3, color.Item4);
    }


    public void StartGameName(string s)
    {
        SceneManager.LoadScene(s);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject optionCanvas;
    private void Start()
    {
        menuCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        optionCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        menuCanvas.SetActive(true);
        optionCanvas.SetActive(false);
    }

    public void OpenOptions()
    {
        menuCanvas.SetActive(false);
        optionCanvas.SetActive(true);
    }

    public void OpenMenu()
    {
        menuCanvas.SetActive(true);
        optionCanvas.SetActive(false);
    }
}

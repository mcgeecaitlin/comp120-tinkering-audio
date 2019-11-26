using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject optionCanvas;

    /// <summary>
    /// Sets up the canvas's so that the main menu is displayed when the project starts
    /// </summary>
    private void Start()
    {
        menuCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        optionCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        menuCanvas.SetActive(true);
        optionCanvas.SetActive(false);
    }

    /// <summary>
    /// Used to set the option menu canvas to active whilst disabling the the main menu canvas
    /// </summary>
    public void OpenOptions()
    {
        menuCanvas.SetActive(false);
        optionCanvas.SetActive(true);
    }

    /// <summary>
    /// Used to set the main menu canvas to active whilst disabling the options menu canvas
    /// </summary>
    public void OpenMenu()
    {
        menuCanvas.SetActive(true);
        optionCanvas.SetActive(false);
    }
}

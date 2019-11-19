using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenOptions : MonoBehaviour
{
    public void OptionsScene()
    {
        SceneManager.LoadScene("Options");
    }

    public void MenuScene()
    {
        SceneManager.LoadScene("MainMeny");
    }
}

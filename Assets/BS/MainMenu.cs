using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Canvas optionsCanvas = null;
    public Canvas mainMenuCanvas = null;
    public Toggle mainToggle;

    bool optionsBool = false;
    public void Start()
    {
        mainToggle.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Options()
    {
        if(optionsBool)
        {
            optionsBool = false;
            optionsCanvas.gameObject.SetActive(false);
            mainMenuCanvas.gameObject.SetActive(true);
        }
        else
        {
            optionsBool = true;
            optionsCanvas.gameObject.SetActive(true);
            mainMenuCanvas.gameObject.SetActive(false);
        }
    }

    public void ValueChangeCheck()
    {
        Debug.Log(mainToggle.isOn);

        if(mainToggle.isOn)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionCanvas : MenuCanvas
{
    // Start is called before the first frame update
    public AudioMixer audioMixer;
    
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }

    public void BackToMain()
    {
        if (!PauseCanvas.instances.isPaused)
        {
            MenuMngr.Instance.ShowCanvas(MenuType.MainMenu);
        }
        else
        {
            MenuMngr.Instance.ShowCanvas(MenuType.InGame);
            PauseCanvas.instances.OnGamePause();
        }
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}

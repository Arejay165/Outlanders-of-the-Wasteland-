using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCanvas : MenuCanvas
{
    // Start is called before the first frame update
    public bool isPaused = false;
    public static PauseCanvas instances;

    protected override void Start()
    {
        base.Start();
        if (instances == null)
            instances = this;
    }
    // Update is called once per fram

    

    public void OnGamePause()
    {
        isPaused = !isPaused;
       
        if(isPaused)
        {
            Time.timeScale = 0;
            GameMngr.Instance.player.canFlip = false;
        }
        else
        {
            Time.timeScale = 1;
            MenuMngr.Instance.ShowCanvas(MenuType.InGame);
            GameMngr.Instance.player.canFlip = true;
        }
    }

  public void Quit()
    {
        Application.Quit();
    }
   

    private void OnApplicationPause()
    {
        isPaused = true;
        Time.timeScale = 0;
    }

    public void ShowOption()
    {
        MenuMngr.Instance.ShowCanvas(MenuType.Option);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuCanvas : MenuCanvas
{
    // Start is called before the first frame update

    private bool isOnMenu = true;
    public GameObject credits;
    public GameObject menu;
    public Button startButton;
    protected override void Start()
    {
        base.Start();

    }
    public void StartGame()
    {
        // GameMngr.Instance

        GameLoader();
    }

    public void GameLoader()
    {
        StartCoroutine(GameMngr.Instance.AsyncLoadScene(GameMngr.Instance.currentLevel, GameMngr.Instance.StartGame));
        startButton.interactable = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        isOnMenu = !isOnMenu;

        if (isOnMenu)
        {
            credits.SetActive(false);
            menu.SetActive(true);
        }
        else
        {
            credits.SetActive(true);
            menu.SetActive(false);
        }
    }

    public void ShowOption()
    {
        MenuMngr.Instance.ShowCanvas(MenuType.Option);
    }

    
}

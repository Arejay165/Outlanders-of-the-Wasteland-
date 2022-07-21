using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
public class GameMngr : MonoBehaviour
{
    [SerializeField]  public Player player { get; set; }
    [SerializeField]  public Boss boss { get; set; }

    public bool isKeyOn;
    public Text keyText;
    public Text instrucText;
    public string currentLevel;
    public static GameMngr Instance;
    public bool introLevel, defeatedCutscene, bossDefeatedCutscene;
    void Start()
    {
        defeatedCutscene = false;

        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            return;
        }
    
        StartCoroutine(AsyncLoadScene("Menu", onMenuLoaded));
      //  StartCoroutine(AsyncLoadScene(currentLevel, StartGame));
    }

    public void onMenuLoaded()
    {
        MenuMngr.Instance.ShowCanvas(MenuType.MainMenu);

    }

    public void StartGame() //try again (I want to rename this)
    {
        MenuMngr.Instance.ShowCanvas(MenuType.InGame);
     
      //    StartCoroutine(AsyncLoadScene("ingamescene", onGameStarted));
    }

    public void ShowCutscene()
    {
        MenuMngr.Instance.ShowCanvas(MenuType.Cutscene);

    }
    public void TitleScreen()
    {
        MenuMngr.Instance.ShowCanvas(MenuType.MainMenu);

        //  StartCoroutine(AsyncLoadScene("ingamescene", onGameStarted));

    }

    public void PauseScreen()
    {
        MenuMngr.Instance.ShowCanvas(MenuType.Pause);
        PauseCanvas.instances.OnGamePause();
    }



    public void RestartScene(string levelName)
    {
        StartCoroutine(UnLoadScene(levelName));
        StartCoroutine(AsyncLoadScene(levelName));

    }

    public void LoadLevel(string previousLevel, string nextLevel)
    {
        StartCoroutine(UnLoadScene(previousLevel));
        StartCoroutine(AsyncLoadScene(nextLevel));
        currentLevel = nextLevel;
    }

    public IEnumerator AsyncLoadScene(string name, Action onCallBack = null)
    {
        AsyncOperation asyncLoadScene = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);

        while (!asyncLoadScene.isDone)
        {
            // loading bar =  asyncLoadScene.progress

            yield return null;
        }
        if (onCallBack != null)
            onCallBack.Invoke();
    }

    public IEnumerator UnLoadScene(string name, Action onCallBack = null)
    {
        AsyncOperation unasych = SceneManager.UnloadSceneAsync(name);

        while (!unasych.isDone)
        {

            yield return null;
        }
        if (onCallBack != null)
            onCallBack.Invoke();
    }
}

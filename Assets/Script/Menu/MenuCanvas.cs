using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum MenuType
{
    MainMenu, 
    Shop,
    InGame,
    GameOver,
    PerkRewards,
    Pause,
    Cutscene,
    Fade,
    Option
}


public class MenuCanvas : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]  MenuType menuType;
    public MenuType MenuType
    {
        get { return menuType; }
    }
    

    protected virtual void Start()
    {
        MenuMngr.Instance.RegisterMenu(this);
    }


    public void Show()
    {
        if (!this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(true);
        }
    }

    public void Hide()
    {
        if (this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
        }
    }
   
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMngr : Singleton<MenuMngr>
{
    [SerializeField] List<MenuCanvas> menuCanvasList = new List<MenuCanvas>();


    public void RegisterMenu(MenuCanvas menuCanvas)
    {
        menuCanvasList.Add(menuCanvas);
        menuCanvas.Hide();
        
    }

    public void HideAll()
    {
        foreach (MenuCanvas menuCanvas in menuCanvasList)
        {
            menuCanvas.Hide();
        }
    }


    public void ShowCanvas(MenuType menuType)
    {
        HideAll();
        
        foreach (MenuCanvas menuCanvas in menuCanvasList)
        {
            if (menuCanvas.MenuType == menuType)
            {
                menuCanvas.Show();
                break;
            }
        }
    }

    public MenuCanvas GetCanvas(MenuType menuType)
    {
        foreach (MenuCanvas menuCanvas in menuCanvasList)
        {
            if (menuCanvas.MenuType == menuType)
            {
                return menuCanvas;
            }
        }
        return null;
    }
}
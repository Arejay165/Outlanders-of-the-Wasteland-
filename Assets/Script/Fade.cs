using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MenuCanvas
{
    // Start is called before the first frame update
    public static Fade instance;
    public Image fadeImage;
    float tempt = 0;
    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            return;
        }
        base.Start();
    }
   public void fadeIn()
    {
    
        fadeImage.CrossFadeAlpha(1, 2.0f, false);
        MenuMngr.Instance.ShowCanvas(MenuType.InGame);

    }

    public void fadeOut()
    {

        fadeImage.CrossFadeAlpha(0, 2.0f, false);


    }

    public void ActivateFade()
    {
        MenuMngr.Instance.ShowCanvas(MenuType.Fade);
        StartCoroutine(FadeSequence(1f));
    }


    public IEnumerator FadeSequence(float fadeDuration)
    {
        while (tempt < fadeDuration)
        {
            tempt++;
            Debug.Log(tempt);
            fadeOut();
            yield return new WaitForSeconds(0.5f);
        }
     
        fadeIn();
       
    }
}

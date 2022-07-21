using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IngameCanvas : MenuCanvas
{
    // Start is called before the first frame update

    public Text keyText;
    public Text tutorialText;
    public Text cutsceneText;
    public GameObject textBox;
    public Image[] image;
    public static IngameCanvas instances;
    public Slider healthSlider;
    public Image[] dialogueBox;
    public Sprite[] dialogueImages;
   // public List<Image> inventoryPerk = new List<Image>();
   // public List<Image> usedPerk = new List<Image>();
    public Text[] coolDownText;
    private void Awake()
    {
        GameMngr.Instance.keyText = keyText;
        GameMngr.Instance.instrucText = tutorialText;
        textBox.SetActive(false);
    }

  
    protected override void Start()
    {
        base.Start();
        if(instances == null)
        {
            instances = this;
        }
       
      
    }
    public void CurrentSelectPerk()
    {
      
        for (int i = 0; i <= GameMngr.Instance.player.perks.Count; i++)
        {
            image[i].color = Color.gray;

        }

        LastImageNull();
        image[GameMngr.Instance.player.perkIndex].color = new Color(255f, 255f, 255f, 255f);
       // Debug.Log("Current Select inventory");
       
    }

    public void LastImageNull()
    {
         // image[GameMngr.Instance.player.perks.Count].sprite = null;
        for(int i = 0; i < 3; i++)
        {
            if(image[i].sprite == null)
            {
                Color alphaCode = image[i].color;
                alphaCode.a = 0;
                image[i].color = alphaCode;
            }
        }
      
    }
 
    public void UsedPerk()
    {
        image[GameMngr.Instance.player.perkIndex].sprite = null;
      //  image[GameMngr.Instance.player.perkIndex].gameObject.SetActive(false);
      

        GameMngr.Instance.player.perkIndex = 0;
       
        ResetImagePerks();

    }

    public void AddPerk()
    {
        for(int i = 0; i <= GameMngr.Instance.player.perks.Count; i++)
        {
            if(image[i].gameObject.activeInHierarchy == false)
            {
                image[i].gameObject.SetActive(true);
                break;
            }
        
        }
    }

    public void ResetInventory()
    {
        foreach (Image imageRef in image)
        {
            imageRef.color = Color.gray;
        }
        LastImageNull();
        image[GameMngr.Instance.player.perkIndex + 1].color = new Color(255f, 255f, 255f, 255f);
    }

    public void ResetPerks()
    {
        foreach (Image imageRef in image)
        {
            
            imageRef.gameObject.SetActive(false);
        }


    }

    public void ResetImagePerks()
    {
        for(int i = 0; i < GameMngr.Instance.player.perks.Count; i++)
        {
            image[i].sprite = GameMngr.Instance.player.perks[i].icon;
        }
        image[GameMngr.Instance.player.perks.Count].sprite = null;
        Color alphaCode = image[GameMngr.Instance.player.perks.Count].color;
        alphaCode.a = 0;

        image[GameMngr.Instance.player.perks.Count].color = alphaCode;
        
    }

    public void SpriteDirectional(int index)
    {
    

        foreach(Image image in dialogueBox)
        {

           image.sprite = null;
            Color alphaCode = image.color;
            alphaCode.a = 0;
            image.color = alphaCode;
        }
        dialogueBox[index].color = new Color(255f, 255f, 255f, 255f);
        switch (index)
        {
            case 1:
                dialogueBox[index].GetComponent<RectTransform>().rotation = new Quaternion(0, 180, 0, 0);
                break;

        }

    }

    public void ResetDirectionalSprites()
    {
        foreach (Image image in dialogueBox)
        {

            image.sprite = null;
            Color alphaCode = image.color;
            alphaCode.a = 0;
            image.color = alphaCode;
        }
    }
}



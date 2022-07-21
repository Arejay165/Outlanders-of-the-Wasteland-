using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarSystem : Singleton<HealthBarSystem>
{
    // Start is called before the first frame update
    public Sprite[] healthbarList;
    public Image healthbar;
    void Start()
    {
       //  healthbar.sprite = healthbarList[12];
    
    }

    // Update is called once per frame
    void Update()
    {
        DisplayHealth();
    }

    public void DisplayHealth()
    {
        if (GameMngr.Instance.player.currentHP >= 0)
            healthbar.sprite = healthbarList[(int)GameMngr.Instance.player.currentHP];
        else
            healthbar.sprite = healthbarList[0];
    }

    public void DisplayBossHealth()
    {

    }
}

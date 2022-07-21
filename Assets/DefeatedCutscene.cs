using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatedCutscene : CutsceneTrigger
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    protected override void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameMngr.Instance.defeatedCutscene == true)
        {
            Time.timeScale = 0f;

            GameMngr.Instance.player.canMove = false;
            GameMngr.Instance.boss.canFight = false;
            hasEnter = true;

            StartDialogue(cutscene);
            IngameCanvas.instances.tutorialText.text = "Press [F] to continue";
        }
    }
}

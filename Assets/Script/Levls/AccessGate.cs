using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessGate : Utilities
{
    // Start is called before the first frame update
    public Sprite[] sprites;
    public GameObject disableLock;
    public bool hasLock = true;
    public GameObject unlockPerks;
    private bool hasEnter;
    public CinemachineSwitcher cinemachineSwitcher;
    public override void OnHit()
    {
        base.OnHit();
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];

        NextLevel.instance.Unlock();
        GameMngr.Instance.isKeyOn = true;

        if (hasLock)
        {
            StartCoroutine(DisableLock());
            if(unlockPerks != null)
            unlockPerks.SetActive(true);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.V) && hasEnter)
        {
            if(hasLock)
            OnHit();
        }
    }

    IEnumerator DisableLock()
    {
        cinemachineSwitcher.SwitchPriority();
        hasLock = false;
       
     GameMngr.Instance.player.canMove = false;
        GameMngr.Instance.player.canFlip = false;
       yield return new WaitForSeconds(2f);
        disableLock.SetActive(false);
        sound.PlayAudio(gameObject.GetComponent<AudioSource>(), 0);
        yield return new WaitForSeconds(0.5f);
       cinemachineSwitcher.SwitchPriority();
        yield return new WaitForSeconds(2f);
        GameMngr.Instance.player.canMove = true;
        GameMngr.Instance.player.canFlip = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hasEnter = true;
            GameMngr.Instance.instrucText.text = "Press [v] to pull lever";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hasEnter = false;
            GameMngr.Instance.instrucText.text = "";
        }
    }

}

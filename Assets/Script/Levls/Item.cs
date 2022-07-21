using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update

    public Perks perks;
    public bool isInEffect;
    public float statsMulplier;
   
    void Start()
    {
        statsMulplier = perks.statsMultplier;
        
     
    }


  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(GameMngr.Instance.player.perks.Count < 3 && GameMngr.Instance.player.canUsePerk)
            {
                IngameCanvas.instances.AddPerk();
               
                perks.PlayAudio(GameMngr.Instance.player.gameObject.GetComponent<AudioSource>(), perks.obtainSfx);

                //IngameCanvas.instances.image[GameMngr.Instance.player.perks.Count].sprite = perks.icon;

                for (int i = 0; i <= GameMngr.Instance.player.perks.Count; i++)
                {
                    if (IngameCanvas.instances.image[i].sprite == null)
                    {
                        Color alphaCode = IngameCanvas.instances.image[i].color;
                        alphaCode.a = 255f;

                        IngameCanvas.instances.image[i].color = alphaCode;
                        IngameCanvas.instances.image[i].sprite = perks.icon;
                        break;
                    }
               
                }

                GameMngr.Instance.player.perks.Add(perks);
                IngameCanvas.instances.CurrentSelectPerk();
                //  Destroy(gameObject);
                GameMngr.Instance.player.items.Add(gameObject);
                gameObject.SetActive(false);
            }
        }
    }

   
    public IEnumerator ActivateJumpBuff()
    {
        while (isInEffect)
        {

            increaseJumpForce(statsMulplier);
            yield return new WaitForSeconds(5f);
            revertJumpForce();
            isInEffect = false;
        }

    }

    


    public void UpgradeState()
    {
        isInEffect = true;

        StartCoroutine(ActivateJumpBuff());
    }

   public void increaseJumpForce(float statMultipler)
    {
        GameMngr.Instance.player.jumpForce += statMultipler;
    }

    public void revertJumpForce()
    {
        GameMngr.Instance.player.jumpForce = GameMngr.Instance.player.data.jumpForce;

    }
}

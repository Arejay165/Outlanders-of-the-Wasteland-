using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Jump Increase")]
public class JumpIncrease : Perks
{
    // Start is called before the first frame update
    public float amount;
    public float buffTimeStarted;
    private float timeStarted;
    
    
    public override IEnumerator CooldownEffect(GameObject obj)
    {
         buffTimeStarted = 0;
        timeStarted = effectDuration;
        obj.gameObject.GetComponent<Player>().canUsePerk = false;
        obj.gameObject.GetComponent<Player>().effectsTransform[effectIndex].SetActive(true);
        Debug.Log("Use Perk");
        obj.gameObject.GetComponent<Player>().equipJump = true;
            while (buffTimeStarted < effectDuration)
            {
                timeStarted--;
                buffTimeStarted++;
                obj.gameObject.GetComponent<Player>().jumpForce += amount;
                yield return new WaitForSeconds(0.5f);
                obj.gameObject.GetComponent<Player>().jumpForce = obj.gameObject.GetComponent<Player>().data.jumpForce;
            IngameCanvas.instances.coolDownText[obj.gameObject.GetComponent<Player>().lastPerkIndex].text = timeStarted.ToString();
            }
        obj.gameObject.GetComponent<Player>().items[obj.gameObject.GetComponent<Player>().lastPerkIndex].SetActive(true); //Spawn after using
        obj.gameObject.GetComponent<Player>().items.RemoveAt(obj.gameObject.GetComponent<Player>().lastPerkIndex); //Remove to the inventory index at player
        obj.gameObject.GetComponent<Player>().canUsePerk = true;
        PlayAudio(obj.GetComponent<AudioSource>(), expireSfx);
       
        obj.gameObject.GetComponent<Player>().effectsTransform[effectIndex].SetActive(false);
        IngameCanvas.instances.coolDownText[obj.gameObject.GetComponent<Player>().lastPerkIndex].text = "";
        obj.gameObject.GetComponent<Player>().equipJump = false;
        IngameCanvas.instances.UsedPerk();
    }

    public override void PlayAudio(AudioSource obj, AudioClip sfx)
    {
     
        obj.clip = sfx;
        obj.PlayOneShot(sfx);
    }

}

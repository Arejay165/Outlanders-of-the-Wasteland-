using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Dash")]
public class Dash : Perks
{
    public float buffTimeStarted;
    public float dashForce;
    private float timeStarted;
    public override IEnumerator CooldownEffect(GameObject obj)
    {
        buffTimeStarted = 0;
        timeStarted = effectDuration;
        obj.gameObject.GetComponent<Player>().canUsePerk = false;
        obj.gameObject.GetComponent<Player>().equipDash = true;
        while (buffTimeStarted < effectDuration)
        {
       
            timeStarted--;
           
            buffTimeStarted++;
            Debug.Log("Start Dash");
            obj.GetComponent<Player>().canDash = true;
            obj.GetComponent<TrailRenderer>().enabled = true;
            yield return new WaitForSeconds(0.5f);
            IngameCanvas.instances.coolDownText[obj.gameObject.GetComponent<Player>().lastPerkIndex].text = timeStarted.ToString();

        }
        obj.GetComponent<Player>().canDash = false;
        obj.GetComponent<TrailRenderer>().enabled = false;
        obj.gameObject.GetComponent<Player>().items[obj.gameObject.GetComponent<Player>().lastPerkIndex].SetActive(true);
        obj.gameObject.GetComponent<Player>().items.RemoveAt(obj.gameObject.GetComponent<Player>().lastPerkIndex);
        PlayAudio(obj.GetComponent<AudioSource>(), expireSfx);
        obj.gameObject.GetComponent<Player>().equipDash = false;
        obj.gameObject.GetComponent<Player>().canUsePerk = true;
        IngameCanvas.instances.coolDownText[obj.gameObject.GetComponent<Player>().lastPerkIndex].text = "";
        IngameCanvas.instances.UsedPerk();

    }

    public override void PlayAudio(AudioSource obj, AudioClip sfx)
    {

        obj.clip = sfx;
        obj.PlayOneShot(sfx);
    }
}

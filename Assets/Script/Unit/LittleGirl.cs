using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleGirl : Unit
{
    Animator anim;
    public Transform player;
    public bool isScared;
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        InIt();
        anim = this.GetComponent<Animator>();

        if (GameMngr.Instance.currentLevel == "FinalLevel")
        {
            isScared = /*true*/ false;
        }
        else
        {
            isScared = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("IsScared", isScared);
        anim.SetFloat("Distance", Vector2.Distance(transform.position, player.transform.position));
    }

    public void LookAtPlayer()
    {
        if (isScared == false)
        {
            if (transform.position.x > player.position.x && isFacingRight)
            {
                Flip();
            }
            else if (transform.position.x < player.position.x && !isFacingRight)
            {
                Flip();
            }
        }
        else
        {
            if (transform.position.x < player.position.x && isFacingRight)
            {
                Flip();
            }
            else if (transform.position.x > player.position.x && !isFacingRight)
            {
                Flip();
            }

        }
    }
}

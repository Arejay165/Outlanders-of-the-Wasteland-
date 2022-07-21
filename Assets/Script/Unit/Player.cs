using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
public class Player : Unit
{
    // Start is called before the first frame update
    public Transform feet;

    public LayerMask groundLayer;
    public CinemachineVirtualCamera vcam;
    public Animator animator;
    public GameObject[] effectsTransform;
    public int perkIndex;
    public List<Perks> perks = new List<Perks>();
    public bool canUsePerk, isMoving;
    private AudioSource audioSrc;
    public bool canDash = false;
    public float dashSpeed;
    public float dashTime = 0.2f;
    public float startDashTime;
    public float direction = 1f;
    private float lastDash = -100f;
    private float dashCooldown = /*2.5f*/ 0.2f;
    public bool isDashing;
    public float dashTimeLeft;
    public bool canMove;
    public bool canFlip;
    public GameObject particle;
    public List<GameObject> items = new List<GameObject>();
    public Action OnDeath;
    private int numberOfFlashes = 10;
    private float flashDuration = 0.07f;
    public Color regularColor;
    public Color flashColor;
    public bool isInvincible = false;
    public int lastPerkIndex;
    public bool equipDash;
    public bool equipJump;

    public bool hasLittleGirl;

    void Start()
    {
        GameMngr.Instance.player = this;

        InIt();
        isFacingRight = true;
        canUsePerk = true;

        audioSrc = gameObject.GetComponent<AudioSource>();
        dashTime = startDashTime;
        canMove = true;
        canFlip = true;
        IngameCanvas.instances.ResetPerks();
        OnDeath += Death;

        if (GameMngr.Instance.currentLevel == "IntroCutscene")
        {
            hasLittleGirl = true;
        }

        if (GameMngr.Instance.currentLevel == "Level1")
        {
            currentHP = 4;
        }
    }




    void Update()
    {
        if (canMove)
        {
            moveX = Input.GetAxisRaw("Horizontal");
            animator.SetFloat("move", Mathf.Abs(moveX));
           // direction = Mathf.Abs(moveX);
        }

        animator.SetBool("HasLittleGirl", hasLittleGirl);
        animator.SetBool("CanJump", isGrounded());
        Vector2 movement = new Vector2(moveX * speed, rb2d.velocity.y);

        rb2d.velocity = movement;

        if (hasLittleGirl == false && Input.GetButtonDown("Jump") && isGrounded())
        {
            Jump();

        }

        if (hasLittleGirl == false && Input.GetKeyDown(KeyCode.LeftShift))
        {
            UsePerk();
        }

        if (canFlip)
        {
            if (isFacingRight && moveX < 0)
            {
                direction = -1;
                Flip();
             
            }
            else if (!isFacingRight && moveX > 0)
            {
                direction = 1;
                Flip();
          
            }
        }

        if (hasLittleGirl == false && Input.GetKeyDown(KeyCode.X))
        {

            SwitchPerkIndex();
        }

        if (hasLittleGirl == false && Input.GetKeyDown(KeyCode.C))
        {
            if (equipDash)
            {
                if (Time.time >= (lastDash + dashCooldown))
                    DashMovement();

            }
            if (equipJump && isGrounded())
            {
                JumpHigh();
            }
        }
         CheckDash();


    }

    public void DashMovement()
    {
        if(canDash == true)
        {

            data.sound.PlayAudio(gameObject.GetComponent<AudioSource>(), 6);
            isDashing = true;
            dashTimeLeft = dashTime;
            lastDash = Time.time;
            particle.SetActive(true);

        }
    }

    public void CheckDash()
    {
        if (isDashing)
        {
            if(dashTimeLeft > 0)
            {
                canMove = false;
                canFlip = false;
                rb2d.velocity = new Vector2(dashSpeed * direction, rb2d.velocity.y);
                dashTimeLeft -= Time.deltaTime;
              // canDash = false
              
            }

            if (dashTimeLeft <= 0)
            {
                isDashing = false;
                particle.SetActive(false);
                canMove = true;
                canFlip = true;
            }
        }
    }

    public void SwitchPerkIndex()
    {

        if (canUsePerk)
        {
            if (perkIndex < perks.Count - 1)
            {
                perkIndex++;
                if (IngameCanvas.instances.image[perkIndex].gameObject.activeSelf == false)
                {
                    Debug.Log("Perk Index " + perkIndex + " Perk add" + (perkIndex + 1));


                    IngameCanvas.instances.ResetInventory();
                }
                else
                {

                    IngameCanvas.instances.CurrentSelectPerk();
                }
                //  Debug.Log(perks[perkIndex].name);


            }
            else
            {
                perkIndex = 0;
                if (IngameCanvas.instances.image[perkIndex].gameObject.activeSelf == false)
                {
                    Debug.Log("Perk Index " + perkIndex + " Perk add" + (perkIndex + 1));
                    IngameCanvas.instances.ResetInventory();
                }
                else
                {

                    IngameCanvas.instances.CurrentSelectPerk();
                }


            }


        }


    }

  
    void Jump()
    {

      
            data.sound.PlayAudio(audioSrc, 2);

            rb2d.velocity = Vector2.up * data.jumpForce;

        


    }

   void JumpHigh()
    {

        data.sound.PlayAudio(audioSrc, 2);

        rb2d.velocity = Vector2.up * jumpForce;
    }

    public bool isGrounded()
    {
        Collider2D groundChecker = Physics2D.OverlapCircle(feet.position, 0.5f, groundLayer);

        if (groundChecker != null)
        {
         
            return true;
        }
       
        return false;
        
    }

   

    public override void Death()
    {
        base.Death();
        if(GameMngr.Instance.introLevel == false)
        {
            animator.SetBool("Death", true);
            data.sound.PlayAudio(audioSrc, 0);
            StartCoroutine(deathEffect());
        }
        else
        {
            Debug.Log("Stuff");
            GameMngr.Instance.defeatedCutscene = true; // Enable something in the inspector and trigger the cutscene
            // GameMngr.Instance.LoadLevel("IntroCutscene", "Level1"); // After cutscene move to level 1
        }
    
    }
    public void UsePerk()
    {
        if (canUsePerk && perks[perkIndex] != null)
        {
            lastPerkIndex = perkIndex;
            StartCoroutine(perks[perkIndex].CooldownEffect(gameObject));
            perks.RemoveAt(perkIndex);
     
       

         //   Debug.Log(perks[perkIndex].name);
   
           
        }
 
       
    }

    public override void TakeDamage(float damage)
    {
        if (!isInvincible)
        {
            base.TakeDamage(damage);
         
            StartCoroutine(IFrames());
            animator.SetTrigger("Hurt");

            data.sound.PlayAudio(audioSrc, 4);
            HealthBarSystem.Instance.DisplayHealth();
        }
       
    }

    public IEnumerator IFrames()
    {
        int tempt = 0;

        // gameObject.GetComponent<Collider2D>().enabled = false;
     
       
        isInvincible = true;

        
         // StartCoroutine(Knockback(0.02f, 100, gameObject.transform.position));
        //rb2d.AddForce(new Vector2(-0.02f * 100, rb2d.velocity.y), ForceMode2D.Impulse);
        while (tempt < numberOfFlashes)
        {
            gameObject.GetComponent<SpriteRenderer>().color = flashColor; // white color
            yield return new WaitForSeconds(flashDuration);
            gameObject.GetComponent<SpriteRenderer>().color = regularColor; // Regular color
            yield return new WaitForSeconds(flashDuration);
            tempt++;
        }
        // gameObject.GetComponent<Collider2D>().enabled = true;
        isInvincible = false;
        gameObject.layer = 8;
      
    }

    public IEnumerator Knockback(float knockDuration, float knockPowr, Vector3 knockBarDir)
    {
        float timer = 0;

        while(timer <= knockDuration)
        {
            timer += Time.deltaTime;
            rb2d.AddForce(new Vector3(-knockBarDir.x * 100, knockBarDir.y + knockPowr, -transform.position.x));

        }

       yield return 0;
    }

    public override void increaseHP(float statMultiplier)
    {
        animator.SetTrigger("Heal");

        base.increaseHP(statMultiplier);
    }

    IEnumerator deathEffect()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        this.enabled = false;
        yield return new WaitForSeconds(1f);
        maxHP = data.healthPoints;
        // HealthBarSystem.Instance.DisplayHealth();
        gameObject.SetActive(false);

        GameMngr.Instance.RestartScene(GameMngr.Instance.currentLevel);

        // HealthBarSystem.Instance.DisplayHealth();
       
    //    HealthBarSystem.Instance.healthbar.sprite = HealthBarSystem.Instance.healthbarList[12];
    }

    

}

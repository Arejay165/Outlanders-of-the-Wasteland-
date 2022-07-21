using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Unit
{
    Animator anim;

    public Transform player;

    public bool canFight;

    public SpriteRenderer spriteRenderer;

    public GameObject bulletPrefab;
    public GameObject jumpPerk;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;
    public bool isAlive = true;

    public float meleeDmg;
    public float rangedDmg;
    public float collisionDmg;

    public float flashDuration;
    public int numberOfFlashes;
    public Color regularColor;
    public Color flashColor;

    void Start()
    {
        GameMngr.Instance.boss = this;

        InIt();
        anim = this.GetComponent<Animator>();

        flashDuration = 0.07f;
        numberOfFlashes = 1;

        canFight = false;

        if (GameMngr.Instance.currentLevel == "IntroCutscene")
        {
            GameMngr.Instance.introLevel = true;
            meleeDmg = 4f;
            rangedDmg = 2f;
            collisionDmg = 2f;
        }
        else
        {
            meleeDmg = 2f;
            rangedDmg = 1f;
            collisionDmg = 1f;
        }
    }

    void Update()
    {
        anim.SetBool("BossFightStart", canFight);
        anim.SetFloat("Distance", Vector2.Distance(transform.position, player.transform.position));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isAlive)
        {

            collision.gameObject.GetComponent<Player>().TakeDamage(collisionDmg);
        }
    }

    public void LookAtPlayer()
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

    public void AttackMelee()
    {
        rb2d.velocity = Vector3.zero;

        LookAtPlayer();

        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        foreach (Collider2D player in hitPlayer)
        {
            if (player != null && player.GetComponent<Player>().currentHP >= 0)
            {
                player.GetComponent<Player>().TakeDamage(meleeDmg);
                // HealthBarSystem.Instance.DisplayHealth();
            }
        }
    }

    public void AttackRanged()
    {
        rb2d.velocity = Vector3.zero;

        LookAtPlayer();

        Instantiate(bulletPrefab, attackPoint.transform.position, attackPoint.transform.rotation);
    }

    public override void TakeDamage(float damage)
    {
        StartCoroutine(DamageIFrames());
        if (GameMngr.Instance.introLevel == false)
        {
            base.TakeDamage(damage);
        }
    }

    public override void Death()
    {
        GameMngr.Instance.bossDefeatedCutscene = true;
        rb2d.velocity = Vector3.zero;

        anim.SetBool("isDead", true);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = false;

        jumpPerk.SetActive(true);

        this.enabled = false;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private IEnumerator DamageIFrames()
    {
        int tempt = 0;

        while (tempt < numberOfFlashes)
        {
            gameObject.GetComponent<SpriteRenderer>().color = flashColor; // red color
            yield return new WaitForSeconds(flashDuration);
            gameObject.GetComponent<SpriteRenderer>().color = regularColor; // normal color
            yield return new WaitForSeconds(flashDuration);
            tempt++;
        }
    }
}

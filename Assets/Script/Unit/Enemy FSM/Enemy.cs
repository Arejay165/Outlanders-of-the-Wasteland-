using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    Animator anim;

    public GameObject player;
    public SpriteRenderer spriteRenderer;

    public LayerMask groundLayers;

    public Transform groundCheck;
    public Transform playerCheck;

    public RaycastHit2D groundHit2D;
    public RaycastHit2D playerHit2D;

    public bool canSeePlayer;
    public bool isHit;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;

    public float decayTime;

    public GameObject GetPlayer()
    {
        return player;
    }

    // Start is called before the first frame update
    void Start()
    {
        InIt();
        isHit = false;
        isFacingRight = true;
        canSeePlayer = false;
        anim = this.GetComponent<Animator>();

        decayTime = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Distance", Vector2.Distance(transform.position, player.transform.position));
        anim.SetBool("CanSeePlayer", canSeePlayer);

        groundHit2D = Physics2D.Raycast(groundCheck.position, -transform.up, 1f, groundLayers);
        playerHit2D = Physics2D.Raycast(playerCheck.position, transform.right, 50f, playerLayer);
        
        if (playerHit2D)
        {
            PlayerDetection();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && currentHP > 0)
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(1f);
        }
    }

    public override void TakeDamage(float damage)
    {

        anim.SetTrigger("isHit");

        base.TakeDamage(damage);
    }

    public override void Death()
    {
        rb2d.velocity = Vector3.zero;
        anim.SetBool("isDead", true);
    
    }

    public void EnemyMovement()
    {
        if (isFacingRight)
        {
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        }
        else
        {
            rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
        }
    }

    public void GroundDetection()
    {
        if (groundHit2D.collider != false)
        {
            EnemyMovement();
        }
        else
        {
            // Debug.Log("No ground detected");
            Flip();
        }
    }

    public void PlayerDetection()
    {
        Player player = playerHit2D.transform.GetComponent<Player>();
        if (player != null)
        {
            canSeePlayer = true;
        }
        else
        {
            canSeePlayer = false;
        }
    }

    public void Attack()
    {
        rb2d.velocity = Vector3.zero;

        LookAtPlayer();

        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        foreach (Collider2D player in hitPlayer)
        {
            if (player != null && player.GetComponent<Player>().currentHP >= 0 && currentHP > 0)
            {
                player.GetComponent<Player>().TakeDamage(2f);
                HealthBarSystem.Instance.DisplayHealth();
            }
            else
            {
                player.GetComponent<Player>().Death();
            }
        }
    }

    public void LookAtPlayer()
    {
        if (transform.position.x > player.transform.position.x && isFacingRight)
        {
            Flip();
        }
        else if (transform.position.x < player.transform.position.x && !isFacingRight)
        {
            Flip();
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void deathEffect()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        rb2d.bodyType = RigidbodyType2D.Static;
        StartCoroutine(DestroyEnemy());
    }

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(decayTime);
        Destroy(gameObject);
    }
}

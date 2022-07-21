using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelerMovement : Unit
{
    // Start is called before the first frame update

    [HideInInspector] public Transform attackPoint;
    [HideInInspector] public float attackRange = 0.5f;
    [HideInInspector] public LayerMask enemyLayers;
    [HideInInspector] public float attackSpeed = 2f;
    [HideInInspector] public float nextAttack = 0;
    [HideInInspector] private Animator anim;
    [HideInInspector] public bool travelerAlive = true;
    
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMngr.Instance.player.hasLittleGirl == false && Input.GetKeyDown(KeyCode.Z) /*&& rb2d.velocity.x == 0*/)
        {
            Attack();
            nextAttack = Time.time + 1f / attackSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameMngr.Instance.PauseScreen();
        }

    }

    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        anim.SetTrigger("Attack");
        data.sound.PlayAudio(gameObject.GetComponent<AudioSource>(), 3);
        foreach (Collider2D enemy in hitEnemies)
        {
            // CameraManager.Instance.ShakeCamera(2f, .1f);
            if (enemy.GetComponent<Enemy>() != null)
            {
                enemy.gameObject.GetComponent<Enemy>().TakeDamage(20f);
            }
            else if (enemy.GetComponent<Boss>() != null)
            {
                enemy.gameObject.GetComponent<Boss>().TakeDamage(20f);
            }
           else if(enemy.GetComponent<Breakables>() != null)
            {
                enemy.gameObject.GetComponent<Breakables>().OnHit();
            }

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            gameObject.GetComponent<AudioSource>().clip = data.sound.clips[5];
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

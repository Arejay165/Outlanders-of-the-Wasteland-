using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxHP;
    public float currentHP;
    public float speed;
    [SerializeField] public Rigidbody2D rb2d;
     public float moveX;
    [SerializeField] public CharacterData data;
     public float jumpForce;
    public bool isFacingRight = true;

    void Start()
    {
        InIt();
    }

    public void InIt()
    {
        this.maxHP = data.healthPoints;
        this.currentHP = maxHP;
        this.jumpForce = data.jumpForce;
        this.speed = data.moveSpeed;
    }

    public virtual void TakeDamage(float damage)
    {
        currentHP -= damage;
        if(currentHP <= 0)
        {
            Death();
        }

    }

    public virtual void Death()
    {

    }

    public virtual void increaseHP(float statMultiplier)
    {
        currentHP += statMultiplier;

        if (currentHP > maxHP)
            currentHP = maxHP;
    }

    public void revertHP(float statReverter)
    {
        maxHP -= statReverter;
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180f, 0);
    }
}

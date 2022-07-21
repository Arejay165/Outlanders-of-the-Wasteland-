using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    public int addHealth;
    public GameObject psPrefab;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameMngr.Instance.player.currentHP < GameMngr.Instance.player.maxHP)
            {
                GameMngr.Instance.player.increaseHP(addHealth);
                // Instantiate(psPrefab, transform.position, transform.rotation);
                HealthBarSystem.Instance.DisplayHealth();
                Instantiate(psPrefab, this.transform.position, this.transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}

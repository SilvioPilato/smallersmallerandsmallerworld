using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    private int health { get; set; }
    public int startingHealth;

    private void Start()
    {
        health = startingHealth;
    }

    private void Update()
    {
        if (health <= 0)
        {
            GameManager.instance.score++;
            AudioManager.manager.OnEnemyKilled();
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        
        health -= damage;
    }

}

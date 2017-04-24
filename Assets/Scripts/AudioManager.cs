using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioClip enemyKilled, enemySpawn, playerDeath, shoot, buttonPressed;
    public AudioSource source;
    public static AudioManager manager;

    private void Awake()
    {
        if (manager != null)
        {
            Destroy(this);
        }
        else
        {
            manager = this;
        }
    }


    public void OnEnemyKilled()
    {
        source.PlayOneShot(enemyKilled);
    }

    public void OnEnemySpawn()
    {
        source.PlayOneShot(enemySpawn);
    }

    public void OnPlayerDead()
    {
        source.PlayOneShot(playerDeath);
    }

    public void OnShoot()
    {
        source.PlayOneShot(shoot);
    }

    public void OnButtonPressed()
    {
        source.PlayOneShot(buttonPressed);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {


    public static GameManager instance;
    public Text scoreText;
    bool mapFinished=false;
    public bool playerDead = false;

    private float spawnTimer=0;
    public float timeFromSpawn = 3f;
    public float spawnTimeDiminishing = .2f;
    public float minSpawnTime=.4f;
    public GameObject enemyPrefab;

    public float timeFromShrink= 7f;
    public float shrinkTiming= 5f;
    private float shrinkTimer=0;
    
    [Range(0, 10)]
    public float unitsToShrink = 2;
    public float minMapDimensions = 2;
    public int score=0;

    public GameObject playAgainButton;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        UpgradeScoreText(score.ToString());     
    }



    public void Update()
    {

        shrinkTimer += Time.deltaTime;
        spawnTimer += Time.deltaTime;
        if (!mapFinished && !playerDead)
        {
            if (spawnTimer > timeFromSpawn)
            {
                spawnTimer = 0;
                SpawnEnemy();
            }

            if (shrinkTimer >= timeFromShrink)
            {
                shrinkTimer = 0;
                StopAllCoroutines();
                StartCoroutine(ShrinkMap());
            }
        }
        UpgradeScoreText(score.ToString());
        if(mapFinished || playerDead)
        {
            playAgainButton.SetActive(true);
        }
    }


    public void SpawnEnemy()
    {
        AudioManager.manager.OnEnemySpawn();
        if (timeFromSpawn - spawnTimeDiminishing > minSpawnTime)
        {
            timeFromSpawn -= spawnTimeDiminishing;
        }
        Vector3 randomSpawnOffset = new Vector3(Random.Range(-transform.localScale.x/2, transform.localScale.x/2), 2, Random.Range(-transform.localScale.z/2, transform.localScale.z/2));
        Vector3 spawnPos = transform.position + randomSpawnOffset;
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
       
    }

    public IEnumerator ShrinkMap()
    {
        Vector3 targetShrink = new Vector3(transform.localScale.x - unitsToShrink, transform.localScale.y, transform.localScale.z - unitsToShrink);
        if (targetShrink.x > minMapDimensions)
        {
            float t = 0;


            while (t <= 1)
            {
                t += Time.deltaTime / shrinkTiming;
                transform.localScale = Vector3.Lerp(transform.localScale, targetShrink, t);
                yield return null;
            }


            yield return null;
        }
        else
        {
            mapFinished = true;
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void UpgradeScoreText(string score)
    {
        scoreText.text = score;
    }
    

}

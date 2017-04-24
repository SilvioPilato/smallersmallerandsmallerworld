using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {
    Transform player;
    public float minPlayerDistance=1f;  
    public float walkSpeed, pushStrenght;
    public bool pushing;

    public float timeFromPush;
    private float pushTimer;

    private Rigidbody rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Update()
    {
        if (!GameManager.instance.playerDead)
        {
            transform.LookAt(player);


            if (Vector3.Distance(transform.position, player.transform.position) > minPlayerDistance)
            {
                pushing = false;
            }
            else if (timeFromPush < pushTimer)
            {
                pushing = true;
            }

            if (pushing)
            {
                rb.AddRelativeForce(Vector3.forward * pushStrenght, ForceMode.Impulse);
                pushTimer = 0;
                pushing = false;
            }
            else
            {
                pushTimer += Time.deltaTime;
                rb.AddRelativeForce(Vector3.forward * walkSpeed, ForceMode.Acceleration);

            }
        }
    }

}

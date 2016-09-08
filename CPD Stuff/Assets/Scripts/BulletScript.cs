﻿using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    //public member variables
    public float bulletSpeed;
    public float lifetime;
    public string bulletTag;
    public Vector3 direction;
    

    //private member variables


    // Use this for initialization
    void Start ()
    {
		//direction = GameObject.FindGameObjectWithTag ("Player").transform.position - transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += direction * bulletSpeed * Time.deltaTime;

        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
	}

    //Deals with collision of bullet with gameobjects, differentiated by "tags"
    void OnTriggerEnter2D(Collider2D hit)
    {
        Debug.Log("I work I promise");
        if (hit.gameObject.tag == "Enemy")
        {
            //deals with enemy hit by player
            if (bulletTag == "Player")
            {
                EnemyController enemy = hit.GetComponent<EnemyController>();
                enemy.health--;
                Destroy(gameObject);
                Debug.Log("Enemy hit by Player Bullet");
            }
        }
        else if (hit.gameObject.tag == "Player")
        {
            //deals with player hit by enemy
            if (bulletTag == "Enemy")
            {
                PlayerScript player = hit.GetComponent<PlayerScript>();
                if (player.delayTime == 0)
                {
                    player.currentHealth--;
                    player.delayTime = player.InvulnTime;
                }
                Destroy(gameObject);
                Debug.Log("Player hit by Enemy Bullet");
            }
        }
        else if (hit.gameObject.tag == "Cube")
        {
            if (bulletTag == "Enemy")
            {
                CubeScript hitCube = hit.GetComponent<CubeScript>();
                hitCube.health--;
                Destroy(gameObject);
            }
        }
    }
}

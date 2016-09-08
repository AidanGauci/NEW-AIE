using UnityEngine;
using System.Collections;

public class HealthPickupScript : MonoBehaviour {

    //public member variables
    public Vector3 direction = Vector3.down;
    public float speed = 1;
    public float lifetime = 5;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += direction * speed * Time.deltaTime;

        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
	}

    //testing collision for player
    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            PlayerScript player = hit.gameObject.GetComponent<PlayerScript>();
            player.currentHealth = player.maxHealth;
            Destroy(gameObject);
        }
    }
}

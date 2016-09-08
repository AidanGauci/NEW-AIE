using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    //public member variables
	public Transform bulletSpawnPos;
	public GameObject bullet;
	public GameObject spawnControl;
    public GameObject healthPickup;
	public int attackDamage = 1;
	public int health = 2;
	public int currentDirection;
	public int LayerID;
    public float fireRate = 2f;
	public float directionChangeDelay = 1f;
	public float timeSinceFlip = 0;
	public float verticalTimeDelay = 10f;
    
    //private member variables
    private float maxVerticalTimeDelay = 10f;
	private float fireDelay;
	private Spawning spawnLink;
    private GameObject player;
    private PlayerScript thePlayer;

    // Use this for initialization
    void Start () {
		fireDelay = Random.Range (1, 10);
		Invoke("FireBullet", fireDelay);
        player = GameObject.FindGameObjectWithTag("Player");
		spawnLink = GameObject.Find("SpawnContoller").GetComponent<Spawning>();
        thePlayer = player.GetComponent<PlayerScript>();
	}
	
	// Update is called once per frame
	void Update () {
		verticalTimeDelay -= Time.deltaTime;
		if (timeSinceFlip < 0)
		{
			timeSinceFlip = 0;
		}
		else if (timeSinceFlip > 0)
		{
			timeSinceFlip -= Time.deltaTime;
		}

        if ((spawnLink.healthLevel + 3) == spawnLink.currentLevel && !spawnLink.healthCanSpawn)
        {
            Debug.Log("i have reset (bool)");
            spawnLink.healthCanSpawn = true;
            spawnLink.healthLevel = spawnLink.currentLevel;
        }
	}

	void FireBullet()
	{
		//Debug.Log ("Fire");
		Instantiate (bullet, bulletSpawnPos.position, Quaternion.identity);
		Invoke("FireBullet", fireRate);
	}

	public bool CheckIfDead()
	{
		if (health == 0)
		{
            spawnLink.aliveEnemies--;
            if ((Random.value <= 1) && spawnLink.healthCanSpawn && (thePlayer.currentHealth != thePlayer.maxHealth))
            {
                Instantiate(healthPickup, transform.position, Quaternion.identity);
                spawnLink.healthCanSpawn = false;
            }
            return true;
		}
		return false;
	}

	IEnumerator MoveDown()
	{
		verticalTimeDelay = maxVerticalTimeDelay;
		Vector3 newPos = transform.position + new Vector3 (0, -0.5f, 0);
		float t = 0;
		float step = (1 / (transform.position - newPos).magnitude) * Time.fixedDeltaTime;
		while (t <= 1.0f)
		{
			t += step;
			transform.position += Vector3.down * Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}
		newPos.x = transform.position.x;
		transform.position = newPos;
	}
}

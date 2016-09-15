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
	public int enemyWorth;
    public float fireRate = 2f;
	public float directionChangeDelay = 1.5f;
	public float timeSinceFlip = 0;
	public float verticalTimeDelay = 10f;
    public float moveSpeed = 1;
    
    //private member variables
	private float maxVerticalTimeDelay = 10f;
	private float fireDelay;
	private Spawning spawnLink;
    private GameObject player;
    private PlayerScript thePlayer;

	void Awake ()
	{
		spawnLink = GameObject.Find("SpawnContoller").GetComponent<Spawning>();
	}

    // Use this for initialization
    void Start () {
		fireDelay = Random.Range (1, 5);
		Invoke("FireBullet", fireDelay);
        player = GameObject.FindGameObjectWithTag("Player");
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

        if ((spawnLink.healthLevel + 2) == spawnLink.currentLevel && !spawnLink.healthCanSpawn)
        {
            Debug.Log("i have reset (bool)");
            spawnLink.healthCanSpawn = true;
            spawnLink.healthLevel = spawnLink.currentLevel;
        }
	}

	void FireBullet()
	{
		GameObject newBullet = (GameObject)Instantiate (bullet, bulletSpawnPos.position, Quaternion.identity);
		BulletScript BS = newBullet.GetComponent<BulletScript> ();
        BS.damage = attackDamage;
		BS.SetAudioPitch(GetPitch ());
		Invoke("FireBullet", fireRate);
	}

	float GetPitch()
	{
		if (transform.name == "Pawn(Clone)")
		{
			return 1f;
		}
		else if (transform.name == "Hawk(Clone)")
		{
			return 2f;
		}
		else if (transform.name == "Gauntler(Clone)")
		{
			return 0.7f;
		}
		else if (transform.name == "Vida(Clone)")
		{
			return 1.5f;
		}
		else if (transform.name == "Ravager(Clone)")
		{
			return 0.5f;
		}
		return 1;
	}

	public bool CheckIfDead()
	{
		if (health == 0)
		{
            if ((Random.value <= 0.35f) && spawnLink.healthCanSpawn && (thePlayer.currentHealth != thePlayer.maxHealth))
            {
                Instantiate(healthPickup, transform.position, Quaternion.identity);
                spawnLink.healthCanSpawn = false;
            }
			thePlayer.score += enemyWorth;
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

	void OnTriggerEnter2D(Collider2D hit)
	{
		if (hit.gameObject.tag == "Player")
		{
			//deals with player hit by enemy
			PlayerScript player = hit.GetComponent<PlayerScript> ();
			if (player.delayTime == 0)
			{
				player.currentHealth = 0;
			}
			health = 0;
		}
		else if (hit.gameObject.tag == "Cube")
		{
			CubeScript hitCube = hit.GetComponent<CubeScript> ();
			hitCube.health = 0;
			health = 0;
		}
	}
}

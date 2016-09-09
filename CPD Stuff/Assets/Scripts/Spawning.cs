using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Spawning : MonoBehaviour {

    //public member variables
	public GameObject[] enemies;
	public GameObject player;
    public GameObject healthGlobe;
	public int aliveEnemies = 0;
	public int currentLevel = 0;
    [HideInInspector]
    public bool healthCanSpawn = true;
    public int healthLevel;

    //private member variables
    private int maxLevel = 5;
	private int currentMoveDir = -1;
    private int refPlayerHealth;
	private bool b_LoadingLevel = false;
	private GameObject myPlayer;
    private PlayerScript thisPlayer;
    private List<GameObject> HealthMeter = new List<GameObject>();

	// Use this for initialization
	void Start ()
    {
		LoadPlayer ();
		LoadNextLevel ();
        thisPlayer = myPlayer.GetComponent<PlayerScript>();
        healthLevel = currentLevel;
        float xPos = -1.361f;
        for (int count = 0; count < 10; count++)
        {
            GameObject newHealth = (GameObject)Instantiate(healthGlobe, new Vector3(xPos, 4.676f, 0), Quaternion.identity);
            HealthMeter.Add(newHealth);
            xPos += 0.343f;
        }
        refPlayerHealth = thisPlayer.maxHealth;
    }
	
	// Update is called once per frame
	void Update () 
{
		if (aliveEnemies == 0 && !b_LoadingLevel)
		{
			b_LoadingLevel = true;
			int nextLevelDelay = CheckForActiveBullets ();
			Invoke ("LoadNextLevel", nextLevelDelay);
		}
        if (thisPlayer.currentHealth != refPlayerHealth)
        {
            for (int count = refPlayerHealth - 1; count > thisPlayer.currentHealth - 1; count--)
            {
                HealthMeter[count].SetActive(false);
            }
        }
        if (thisPlayer.currentHealth == thisPlayer.maxHealth)
        {
            for (int count = 0; count < 10; count++)
            {
                HealthMeter[count].SetActive(true);
            }
        }
    }

    // 
    void ChangeMoveDir ()
	{
		currentMoveDir *= -1;
	}


	public void LoadPlayer()
	{
		myPlayer = Instantiate (player, new Vector3 (0, -3, 0), Quaternion.Euler (0, 0, 180)) as GameObject;
	}
	public void LoadNextLevel()
	{
		currentLevel++;
		if (currentLevel <= maxLevel)
		{
			Invoke ("LoadLevel" + currentLevel, 0);
		}
		else if (currentLevel > maxLevel)
		{
			GameController GC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
			GC.LoadEndMenu (myPlayer.GetComponent<PlayerScript>().score);
		}

		if (healthCanSpawn)
		{
			healthLevel = currentLevel;
		}

		myPlayer.GetComponent<PlayerScript>().canShoot = true;
	}

	void LoadLevel1()
	{
		float startX = -2f;
		ChangeMoveDir ();
		for (int i = 0; i < 4; i++)
		{
			GameObject enemy = (GameObject) Instantiate (enemies[0], new Vector3 (startX, 1.5f, 0), Quaternion.identity);
			enemy.GetComponent<EnemyController> ().currentDirection = currentMoveDir;
			enemy.GetComponent<EnemyController> ().LayerID = 0;
			aliveEnemies++;
			startX += 1.2f;
			Debug.Log("Spawn enemy " + aliveEnemies);
		}
		ChangeMoveDir ();
		startX = -1.4f;
		for (int i = 0; i < 3; i++)
		{
			GameObject enemy = (GameObject) Instantiate (enemies[1], new Vector3 (startX, 3, 0), Quaternion.identity);
			enemy.GetComponent<EnemyController> ().currentDirection = currentMoveDir;
			enemy.GetComponent<EnemyController> ().LayerID = 1;
			aliveEnemies++;
			startX += 1.2f;
			Debug.Log("Spawn enemy " + aliveEnemies);
		}
		b_LoadingLevel = false;
		GetComponent<SquadController> ().GetEnemies ();
	}

	void LoadLevel2()
	{
		float startX = -1.4f;
		ChangeMoveDir ();
		for (int i = 0; i < 3; i++)
		{
			GameObject enemy = (GameObject) Instantiate (enemies[1], new Vector3 (startX, 3, 0), Quaternion.identity);
			enemy.GetComponent<EnemyController> ().currentDirection = currentMoveDir;
			enemy.GetComponent<EnemyController> ().LayerID = 1;
			aliveEnemies++;
			startX += 2f;
			Debug.Log("Spawn enemy " + aliveEnemies);
		}
		ChangeMoveDir ();
		startX = -2f;
		for (int i = 0; i < 2; i++)
		{
			GameObject enemy = (GameObject) Instantiate (enemies[0], new Vector3 (startX, 1.5f, 0), Quaternion.identity);
			enemy.GetComponent<EnemyController> ().currentDirection = currentMoveDir;
			enemy.GetComponent<EnemyController> ().LayerID = 0;
			aliveEnemies++;
			startX += 4f;
			Debug.Log("Spawn enemy " + aliveEnemies);
		}
		GameObject _enemy = (GameObject) Instantiate (enemies[3], new Vector3 (0, 1.5f, 0), Quaternion.identity);
		aliveEnemies++;
        _enemy.GetComponent<EnemyController> ().currentDirection = currentMoveDir;
		_enemy.GetComponent<EnemyController> ().LayerID = 0;
		b_LoadingLevel = false;
		GetComponent<SquadController> ().GetEnemies ();
	}


	void LoadLevel3()
	{
		float startX = -2f;
		ChangeMoveDir ();
		for (int i = 0; i < 4; i++)
		{
			GameObject enemy = (GameObject) Instantiate (enemies[0], new Vector3 (startX, 1.5f, 0), Quaternion.identity);
			enemy.GetComponent<EnemyController> ().currentDirection = currentMoveDir;
			enemy.GetComponent<EnemyController> ().LayerID = 0;
			aliveEnemies++;
			startX += 1.2f;
			Debug.Log("Spawn enemy " + aliveEnemies);
		}
		startX = -1.5f;
		ChangeMoveDir ();
		for (int i = 0; i < 2; i++)
		{
			GameObject enemy = (GameObject) Instantiate (enemies[3], new Vector3 (startX,3f, 0), Quaternion.identity);
			enemy.GetComponent<EnemyController> ().currentDirection = currentMoveDir;
			enemy.GetComponent<EnemyController> ().LayerID = 1;
			aliveEnemies++;
			startX += 2.5f;
			Debug.Log("Spawn enemy " + aliveEnemies);
		}
		b_LoadingLevel = false;
		GetComponent<SquadController> ().GetEnemies ();
	}


	void LoadLevel4()
	{
		float startX = -1.5f;
		ChangeMoveDir ();
		for (int i = 0; i < 3; i++)
		{
			GameObject enemy = (GameObject) Instantiate (enemies[1], new Vector3 (startX, 3, 0), Quaternion.identity);
			enemy.GetComponent<EnemyController> ().currentDirection = currentMoveDir;
			enemy.GetComponent<EnemyController> ().LayerID = 0;
			aliveEnemies++;
			startX += 1.5f;
			Debug.Log("Spawn enemy " + aliveEnemies);
		}
		startX = -1f;
		ChangeMoveDir ();
		for (int i = 0; i < 2; i++)
		{
			GameObject enemy = (GameObject) Instantiate (enemies[3], new Vector3 (startX, 1.5f, 0), Quaternion.identity);
			enemy.GetComponent<EnemyController> ().currentDirection = currentMoveDir;
			enemy.GetComponent<EnemyController> ().LayerID = 1;
			aliveEnemies++;
			startX += 2f;
			Debug.Log("Spawn enemy " + aliveEnemies);
		}
		startX = -1.5f;
		ChangeMoveDir ();
		for (int i = 0; i < 3; i++)
		{
			GameObject enemy = (GameObject) Instantiate (enemies[2], new Vector3 (startX, 0, 0), Quaternion.identity);
			enemy.GetComponent<EnemyController> ().currentDirection = currentMoveDir;
			enemy.GetComponent<EnemyController> ().LayerID = 2;
			aliveEnemies++;
			startX += 1.5f;
			Debug.Log("Spawn enemy " + aliveEnemies);
		}
		b_LoadingLevel = false;
		GetComponent<SquadController> ().GetEnemies ();
	}


	void LoadLevel5()
	{
		float startX = -1.5f;
		ChangeMoveDir ();
		for (int i = 0; i < 2; i++)
		{
			GameObject enemy = (GameObject) Instantiate (enemies[3], new Vector3 (startX, 1.5f, 0), Quaternion.identity);
			enemy.GetComponent<EnemyController> ().currentDirection = currentMoveDir;
			enemy.GetComponent<EnemyController> ().LayerID = 0;
			aliveEnemies++;
			startX += 3f;
			Debug.Log("Spawn enemy " + aliveEnemies);
		}
		ChangeMoveDir ();
		GameObject _enemy = (GameObject) Instantiate (enemies[4], new Vector3 (0, 3.2f, 0), Quaternion.identity);
		aliveEnemies++;
		_enemy.GetComponent<EnemyController> ().currentDirection = currentMoveDir;
		_enemy.GetComponent<EnemyController> ().LayerID = 1;
		b_LoadingLevel = false;
		GetComponent<SquadController> ().GetEnemies ();
	}

	int CheckForActiveBullets()
	{
		GameObject[] bullets = GameObject.FindGameObjectsWithTag ("Bullet");
		float max = 0;
		for (int i = 0; i < bullets.Length; i++)
		{
			float temp = bullets [i].GetComponent<BulletScript> ().lifetime;
			if (temp > max)
			{
				max = temp;
			}
		}

		if (max != 0)
		{
			myPlayer.GetComponent<PlayerScript>().canShoot = false;
		}

		int finalDelay = (int)max + 1;

		return finalDelay;
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SquadController : MonoBehaviour {

	public Transform bulletSpawnPos;
	public GameObject bullet;
	public GameObject spawnControl;
	public int currentDirection;
	public int LayerID;

	private float screenHalfWidthInWorldUnits;
	private List<EnemyController> activeEnemies = new List<EnemyController>();

	// Use this for initialization
	void Start () {
		float halfSelfWidth = transform.localScale.x / 2;
		screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize - halfSelfWidth;
	}

	// Update is called once per frame
	void Update () {

		for (int i = 0; i < activeEnemies.Count; i++)
		{
			EnemyController currentEnemy = activeEnemies [i];
			if (currentEnemy.verticalTimeDelay <= 0)
			{
				currentEnemy.StartCoroutine("MoveDown");
			}
			if (currentEnemy.transform.position.x < -screenHalfWidthInWorldUnits ||
				currentEnemy.transform.position.x > screenHalfWidthInWorldUnits &&
				currentEnemy.timeSinceFlip <= 0)
			{
				currentEnemy.timeSinceFlip = currentEnemy.directionChangeDelay;
				currentEnemy.currentDirection *= -1;
				currentEnemy.timeSinceFlip = currentEnemy.directionChangeDelay;
				ChangeDirIfYSame (currentEnemy);
			}
		}

		for (int i = 0; i < activeEnemies.Count; i++)
		{
			EnemyController currentEnemy = activeEnemies [i];
			float moveAmount = (1 * currentEnemy.currentDirection) * Time.deltaTime;
			currentEnemy.transform.position += new Vector3 (moveAmount, 0, 0);

			if (currentEnemy.CheckIfDead ())
			{
				Destroy (currentEnemy.gameObject);
				activeEnemies.Remove (currentEnemy);
			}
		}
	}

	void ChangeDirIfYSame (EnemyController currEnemy)
	{
		foreach (EnemyController other in activeEnemies)
		{
			if (other != currEnemy) 
			{
				if (other.LayerID == currEnemy.LayerID)
				{
					other.currentDirection = currEnemy.currentDirection;
				}
			}
		}
	}

	public void GetEnemies ()
	{
		activeEnemies.Clear();
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject other in enemies) {
			activeEnemies.Add(other.GetComponent<EnemyController>());
		}
	}
}

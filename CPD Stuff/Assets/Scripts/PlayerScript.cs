using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    //public member variables
    public int maxHealth = 5;
    public int currentHealth;
    public float speed = 5;
    public float fireRate = 1;
    public float InvulnTime = 1;
    public float delayTime;
    public Text scoreOutput;
    public GameObject bullet;

    //private member variables
    private Vector3 pos = new Vector3(0, -3, 0);
    private Vector3 prevPos = new Vector3(0, -3, 0);
    private int score = 0;
    
    private int m_direction = 0;
    private bool leftPress = false;
    private bool rightPress = false;
    private bool canFire = true;
    private float screenHalfWidthInWorldUnits;
	
    //used to initialize shtuff
    void Start()
    {
        float halfSelfWidth = transform.localScale.x / 2;
        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize - halfSelfWidth;
        currentHealth = maxHealth;
    }

	// Update is called once per frame
	void Update ()
    {
		if(Input.GetAxis("Horizontal") > 0)
		{
			SwitchOn (1);
		}
		else
		{
			SwitchOff (-1);
		}

		if(Input.GetAxis("Horizontal") < 0)
		{
			SwitchOn (-1);
		}
		else
		{
			SwitchOff (-1);
		}

		if(Input.GetAxisRaw("Fire1") != 0)
		{
			FireShot (1);
		}
        //end-game stuff
        if (currentHealth <= 0)// || end-game constraints met)
        {
            EndGame();
        }

        //deals with fireRate
        if (canFire == false)
        {
            fireRate -= Time.deltaTime;
        }
        if (fireRate <= 0)
        {
            canFire = true;
            fireRate = 1;
        }

        if (delayTime > 0)
        {
            delayTime -= Time.deltaTime;
        }
        else
        {
            delayTime = 0;
        }

        //moves player and keeps them in screen
        prevPos = transform.position;
        if (leftPress || rightPress)
        {
            if (transform.position.x > -screenHalfWidthInWorldUnits && transform.position.x < screenHalfWidthInWorldUnits)
            {
                MovePlayer(m_direction);
            }
            if (transform.position.x < -screenHalfWidthInWorldUnits || transform.position.x > screenHalfWidthInWorldUnits)
            {
                transform.position = prevPos;
            }
        }
	}
    //shoots bullet from player
    public void FireShot(int shoot)
    {
        if (shoot == 1 && canFire)
        {
            Vector3 bulletPos = transform.position + (Vector3.up * 0.1f);
            Instantiate(bullet, bulletPos, Quaternion.identity);

            canFire = false;
        }
    }

    //moves player according to button input
    void MovePlayer(int direction)
    {
        
        if (direction == -1)
        {
            
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (direction == 1)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {

        }
    }

    //SwitchOn and SwitchOff both used to move player
    public void SwitchOff(int direction)
    {
        if (direction == -1)
        {
            leftPress = false;
        }
        else if (direction == 1)
        {
            rightPress = false;
        }
    }

    public void SwitchOn(int direction)
    {
        if (direction == -1)
        {
            leftPress = true;
        }
        else if (direction == 1)
        {
            rightPress = true;
        }
        m_direction = direction;
    }

    //calls end game screen when out of health
    void EndGame()
    {
        
    }

   void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.tag == "Pickup")
        {
            currentHealth = maxHealth;
            Destroy(hit.gameObject);
        }
    }
}

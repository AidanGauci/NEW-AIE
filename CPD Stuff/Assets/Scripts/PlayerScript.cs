using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    //public member variables
    public int maxHealth = 5;
    public int currentHealth;
	public int score = 0;
    public float speed = 5;
    public float fireRate = 1;
    public float InvulnTime = 1;
    public float delayTime;
    public GameObject bullet;
	[HideInInspector]
	public bool canShoot = true;

    //private member variables
    private Vector3 pos = new Vector3(0, -3, 0);
    private Vector3 prevPos = new Vector3(0, -3, 0);
    private float currentFireRate;
    private int m_direction = 0;
    private bool leftPress = false;
    private bool rightPress = false;
    private bool canFire = false;
    private float screenHalfWidthInWorldUnits;
	
    //used to initialize shtuff
    void Start()
    {
        float halfSelfWidth = transform.localScale.x / 2;
        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize - halfSelfWidth;
        currentHealth = maxHealth;
        currentFireRate = fireRate;
    }

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            SwitchOn(1);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            SwitchOn(-1);
        }
        else
        {
            SwitchOff(-1);
            SwitchOff(1);
        }

        if (Input.GetAxisRaw("Jump") != 0 && canShoot)
        {
            FireShot();
        }
        else
        {
            StopShoot();
        }



        //end-game stuff
        if (currentHealth <= 0)// || end-game constraints met)
        {
            EndGame();
        }

        //deals with fireRate
        if (currentFireRate > 0)
        {
            currentFireRate -= Time.deltaTime;
        }
        if (currentFireRate <= 0 && canFire)
        {
            Vector3 bulletPos = transform.position + (Vector3.up * 0.1f);
            Instantiate(bullet, bulletPos, Quaternion.identity);
            currentFireRate = fireRate;
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
    public void FireShot()
    {
        canFire = true;
    }

    public void StopShoot()
    {
        canFire = false;
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

    public void makeFlash()
    {
        StartCoroutine("PlayerFlash");
    }

    IEnumerator PlayerFlash()
    {
        for (int i = 0; i < 20; i++)
        {
            Debug.Log("Loop " + i);
            GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
            yield return new WaitForSeconds(0.05f);
        }
    }
}

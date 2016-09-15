using UnityEngine;
using System.Collections.Generic;

public class BarrierScript : MonoBehaviour {

    //public member variables
    public GameObject cube;
    public GameObject leftSide;
    public GameObject rightSide;

    public Sprite FullBarrier;
    public Sprite HalfBarrier;
    public Sprite DeadBarrier;

    public Sprite FullRightBarrier;
    public Sprite HalfRightBarrier;
    public Sprite DeadRightBarrier;

    public Sprite FullLeftBarrier;
    public Sprite HalfLeftBarrier;
    public Sprite DeadLeftBarrier;

    //private member variables
    private List<GameObject> cubeList = new List<GameObject>();

	// Use this for initialization
	void Start ()
    {
        CreateBarrier();
	}
	
	// Update is called once per frame
	void Update ()
    {
       // Debug.Log(cubeList.Count);
	    for (int count = 0; count < cubeList.Count; count++)
        {
            //kill cubes that have no health left
            if (cubeList[count].GetComponent<CubeScript>().health <= 0)
            {
                Destroy(cubeList[count]);
                cubeList.RemoveAt(count);
            }
            else if (cubeList[count].GetComponent<CubeScript>().health == 1)
            {
                if (cubeList[count].GetComponent<SpriteRenderer>().sprite == HalfBarrier)
                {
                    cubeList[count].GetComponent<SpriteRenderer>().sprite = DeadBarrier;
                }
                else if (cubeList[count].GetComponent<SpriteRenderer>().sprite == HalfRightBarrier)
                { 
                    cubeList[count].GetComponent<SpriteRenderer>().sprite = DeadRightBarrier;
                }
                else if (cubeList[count].GetComponent<SpriteRenderer>().sprite == HalfLeftBarrier)
                {
                    cubeList[count].GetComponent<SpriteRenderer>().sprite = DeadLeftBarrier;
                }
            }
            else if (cubeList[count].GetComponent<CubeScript>().health == 2 || cubeList[count].GetComponent<CubeScript>().health == 3)
            {
				if (cubeList[count].GetComponent<SpriteRenderer>().sprite == FullBarrier || cubeList[count].GetComponent<SpriteRenderer>().sprite == DeadBarrier)
                {
                    cubeList[count].GetComponent<SpriteRenderer>().sprite = HalfBarrier;
                }
				else if (cubeList[count].GetComponent<SpriteRenderer>().sprite == FullRightBarrier || cubeList[count].GetComponent<SpriteRenderer>().sprite == DeadRightBarrier)
                {
                    cubeList[count].GetComponent<SpriteRenderer>().sprite = HalfRightBarrier;
                }
				else if (cubeList[count].GetComponent<SpriteRenderer>().sprite == FullLeftBarrier || cubeList[count].GetComponent<SpriteRenderer>().sprite == DeadLeftBarrier)
                {
                    cubeList[count].GetComponent<SpriteRenderer>().sprite = HalfLeftBarrier;
                }
            }
			else if (cubeList[count].GetComponent<CubeScript>().health == 4)
			{
				if (cubeList[count].GetComponent<SpriteRenderer>().sprite == HalfBarrier)
				{
					cubeList[count].GetComponent<SpriteRenderer>().sprite = FullBarrier;
				}
				else if (cubeList[count].GetComponent<SpriteRenderer>().sprite == HalfRightBarrier)
				{ 
					cubeList[count].GetComponent<SpriteRenderer>().sprite = FullRightBarrier;
				}
				else if (cubeList[count].GetComponent<SpriteRenderer>().sprite == HalfLeftBarrier)
				{
					cubeList[count].GetComponent<SpriteRenderer>().sprite = FullLeftBarrier;
				}
			}

        }
	}
    //creates two barriers in front of the player
    void CreateBarrier()
    {
        //first barrier
        int count = 0;
        float xPos = 1.83f;
        cubeList.Add((GameObject)Instantiate(rightSide, new Vector3(xPos, -2), Quaternion.identity));
        xPos -= 0.38f;
        while (count < 2)
        {
            cubeList.Add((GameObject)Instantiate(cube, new Vector3(xPos, -2), Quaternion.identity));
            xPos -= 0.38f;
            count++;
        }
        cubeList.Add((GameObject)Instantiate(leftSide, new Vector3(xPos, -2), Quaternion.identity));

        //second barrier
        xPos = -0.85f;
        cubeList.Add((GameObject)Instantiate(rightSide, new Vector3(xPos, -2), Quaternion.identity));
        xPos -= 0.38f;
        while (count < 4)
        {
            cubeList.Add((GameObject)Instantiate(cube, new Vector3(xPos, -2), Quaternion.identity));
            xPos -= 0.38f;
            count++;
        }
        cubeList.Add((GameObject)Instantiate(leftSide, new Vector3(xPos, -2), Quaternion.identity));
    }

	public void UpHealth()
	{
		for (int count = 0; count < cubeList.Count; count++)
		{
			if (cubeList [count].GetComponent<CubeScript> ().health != 5) 
			{
				cubeList [count].GetComponent<CubeScript> ().health++;
			}
		}
	}

}

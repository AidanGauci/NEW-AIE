using UnityEngine;
using System.Collections.Generic;

public class BarrierScript : MonoBehaviour {

    //private member variables
    private List<GameObject> cubeList = new List<GameObject>();

    //public member variables
    public GameObject cube;

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
            if (cubeList[count].GetComponent<CubeScript>().health == 0)
            {
                Destroy(cubeList[count]);
                cubeList.RemoveAt(count);
                
            }
        }
	}
    //creates two barriers in front of the player
    void CreateBarrier()
    {
        //first barrier
        int count = 0;
        float xPos = 1.83f;
        while (count < 4)
        {
            cubeList.Add((GameObject)Instantiate(cube, new Vector3(xPos, -2), Quaternion.identity));
            xPos -= 0.38f;
            count++;
        }

        //second barrier
        xPos = -0.85f;
        while (count < 8)
        {
            cubeList.Add((GameObject)Instantiate(cube, new Vector3(xPos, -2), Quaternion.identity));
            xPos -= 0.38f;
            count++;
        }
    }
}

using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {        
        if (Input.GetKeyUp(KeyCode.A))
        {
            FindObjectOfType<CubeScript>().health--;
        }
        
	}
}

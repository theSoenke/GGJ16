using UnityEngine;
using System.Collections;

public class testScript : MonoBehaviour {

    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        IsInsideHitbox();

    }

    void IsInsideHitbox()
    {
        if (UsefullStuff.InsideCollision(transform.position))
            print("mööööööööööööp");
    }
}

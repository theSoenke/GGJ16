using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public GameObject player;
	public GameObject[] lights;
	public GameObject shadow;
	public Transform[] spawnPoints;         
	private int level = 0;
	private int lightsLeft;

	void Start(){
		spawnShadows(5);
		lightsLeft = lights.Length;
	}

	void goDeeper(){
		level++;
		spawnShadows (3);
		//darker
		//creepyer
		//shit der passiert, wenn deeper
	}



	void spawnShadows(int anzahl){
		for(int i=0; i<anzahl;i++){
			int spawnPointIndex = Random.Range (0, spawnPoints.Length);
			Instantiate (shadow, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);
		}
	}


	void Update () {
		if (player.GetComponent<PlayerController>().Health == 0) {
			//loose
		}
		if (lights.Length < lightsLeft) {
			goDeeper ();
		}
		if (lightsLeft == 0) {
			//win
		}
	}
}

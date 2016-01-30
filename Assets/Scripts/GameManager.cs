using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	GameObject player;
    public static GameManager instance;
	public GameObject shadow;
	public Transform[] spawnPoints;         
	private int level = 1;
	private int lightsLeft;
	private GameObject[] lights;
    public GameObject[] stage;

	void Start()
    {
		lights = GameObject.FindGameObjectsWithTag ("Light");
        instance = this;
		spawnShadows(1);
		lightsLeft = lights.Length;
        player = PlayerController.Instance.gameObject;
	}

	void LightCollected()
    {

    }

    void Degenerate()
    {
        stage[level].SetActive(false);
        level++;
        stage[level].SetActive(true);
    }

    


	public void spawnShadows(int anzahl)
    {
		for(int i=0; i<anzahl;i++)
        {
			int spawnPointIndex = Random.Range (0, spawnPoints.Length);
			Instantiate (shadow, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);
		}
	}


	void Update ()
    {
		lights = GameObject.FindGameObjectsWithTag ("Light");
		if (PlayerController.Instance.Health <= 0) {
			//loose
		}
		if (lights.Length == 0)
        {
			//win
		}		
	}
}

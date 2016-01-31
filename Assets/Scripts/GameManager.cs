using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	GameObject player;
    public static GameManager instance;
	public GameObject shadow;
	public Transform[] spawnPoints;         
	private int level = 1;
	private int lightsLeft;
	private GameObject[] lights;
    public GameObject[] stage;
    public int[] _stageBaseShadowCount;

	void Awake()
    {
        player = PlayerController.Instance.gameObject;
        lights = GameObject.FindGameObjectsWithTag ("Light");
        instance = this;
		spawnShadows(1);
		lightsLeft = lights.Length;
        
	}

	void LightCollected()
    {

    }

    public void Degenerate()
    {
        GetComponent<EffectManager>().StartBlurVision();
        stage[level - 1].SetActive(false);
        level++;
        stage[level - 1].SetActive(true);
        spawnShadows(_stageBaseShadowCount[level - 1]);
        SoundControllerScript._instance.SwitchTrack(SoundControllerScript._instance._currentTrack + 1);
    }

    


	public void spawnShadows(int anzahl)
    {
        List<Transform> validSPs = new List<Transform>();
        foreach(Transform sp in spawnPoints)
        {
            if(Vector3.Distance(sp.position, player.transform.position) >= 10)
            {
                validSPs.Add(sp);
            }
        }

		for(int i=0; i<anzahl;i++)
        {
            if (validSPs.Count <= 0)
                break;
            int spawnPointIndex = Random.Range (0, validSPs.Count);
			Instantiate (shadow, validSPs [spawnPointIndex].position, validSPs[spawnPointIndex].rotation);
            validSPs.RemoveAt(spawnPointIndex);           
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

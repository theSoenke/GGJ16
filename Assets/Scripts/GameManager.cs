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
    public List<GameObject> _spawnedShadows;

	void Awake()
    {
        player = PlayerController.Instance.gameObject;
        lights = GameObject.FindGameObjectsWithTag ("Light");
        instance = this;
		spawnShadows(1);
		lightsLeft = lights.Length;
        _spawnedShadows = new List<GameObject>();

    }

	void LightCollected()
    {                                               //todo
        lightsLeft--;

        if(lightsLeft == 0)
        {
            Win();
        }
        
    }

    public void Degenerate()
    {
        if (level < stage.Length)
        {
            GetComponent<EffectManager>().StartBlurVision();
            stage[level - 1].SetActive(false);
            level++;
            stage[level - 1].SetActive(true);
            spawnShadows(_stageBaseShadowCount[level - 1]);
            StunShadows();
            SoundControllerScript._instance.SwitchTrack(SoundControllerScript._instance._currentTrack + 1);
        }
        else
            Lose();
    }

    void StunShadows()
    {
        foreach(GameObject sh in _spawnedShadows)
        {
            sh.GetComponent<ShadowControllerScript>().Stun();
        }
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
			_spawnedShadows.Add((GameObject) Instantiate (shadow, validSPs [spawnPointIndex].position, validSPs[spawnPointIndex].rotation));
            validSPs.RemoveAt(spawnPointIndex);           
		}
	}

    void Lose()
    {
        print("NOOB");
    }

    void Win()
    {

    }

	void Update ()
    {
		
			
	}
}

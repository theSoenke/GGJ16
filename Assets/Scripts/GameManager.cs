using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

	public GameObject shadow;
	public Transform[] spawnPoints;         
	private int level = 1;
	private int lightsLeft;
	private List<GameObject> lights;
    public GameObject[] stage;
    public int[] _stageBaseShadowCount;
    public List<GameObject> _spawnedShadows;
    public GameObject _UI;
    public Image[] _slot;
    public Sprite _img;

    private GameObject player;
    private bool _gameOver;
    private bool _wonGame;

    void Awake()
    {
        instance = this;

        player = GameObject.FindGameObjectWithTag("Player");

        lights = new List<GameObject>();

        spawnShadows(1);
    }

    void Start()
    {
        lights.AddRange( GameObject.FindGameObjectsWithTag("Light"));
        lightsLeft = lights.Count;
    }

    public bool Won
    {
        get
        {
            return _wonGame;
        }
    }

    public bool GameOver
    {
        get
        {
            return _gameOver;
        }
    }

	public void LightCollected(GameObject light)
    {
        lightsLeft--;
        switch (lightsLeft)
        {
            case 1 : _slot[1].sprite = _img; break;
            case 2: _slot[2].sprite = _img; break;
            case 3: _slot[3].sprite = _img; break;
            case 4: _slot[4].sprite = _img; break;
        }
        StartCoroutine(ShowUI());
        
        print(lightsLeft);
        lights.Remove(light);

        if(lightsLeft <= 0 || lights.Count <= 0)
        {
            Win();
        }
    }

    IEnumerator ShowUI()
    {
        float startTime = Time.time;
        while (Time.time - startTime <= 2)
        {
            _UI.SetActive(true);
            yield return null;
        }
        _UI.SetActive(false);
        yield return null;
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
            Loose();
    }

    void StunShadows()
    {
        foreach (GameObject sh in _spawnedShadows)
        {
            sh.GetComponent<ShadowControllerScript>().Stun();
        }
    }


    public void spawnShadows(int anzahl)
    {
        List<Transform> validSPs = new List<Transform>();
        foreach (Transform sp in spawnPoints)
        {
            if (Vector3.Distance(sp.position, player.transform.position) >= 10)
            {
                validSPs.Add(sp);
            }
        }

        for (int i = 0; i < anzahl; i++)
        {
            if (validSPs.Count <= 0)
                break;
            int spawnPointIndex = Random.Range(0, validSPs.Count);
            _spawnedShadows.Add((GameObject)Instantiate(shadow, validSPs[spawnPointIndex].position, validSPs[spawnPointIndex].rotation));
            validSPs.RemoveAt(spawnPointIndex);
        }
    }

    void Loose()
    {
        print("NOOB");
        _gameOver = true;
        _wonGame = false;
        LoadOutro();
    }

    void Win()
    {
        print("Yay");
        _gameOver = true;
        _wonGame = true;
        LoadOutro();
    }

    void LoadOutro()
    {
        SceneManager.LoadScene("Outro", LoadSceneMode.Single);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Win();
            Debug.Log("cheater");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Loose();
            Debug.Log("cheater");
        }
    }
}

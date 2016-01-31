﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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

    private bool _gameOver;
    private bool _wonGame;

    void Awake()
    {
        player = PlayerController.Instance.gameObject;

        instance = this;
        spawnShadows(1);

        _spawnedShadows = new List<GameObject>();

    }

    void Start()
    {
        lights = GameObject.FindGameObjectsWithTag("Light");
        lightsLeft = lights.Length;

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

    void LightCollected()
    {                                               //todo
        lightsLeft--;

        if (lightsLeft == 0)
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
        Time.timeScale = 0;
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

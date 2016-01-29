using UnityEngine;
using System.Collections;

public class ShadowControllerScript : MonoBehaviour
{
    [Header("Behaviour")]

    public float _targetRefreshRate;
    public float _flightDistance;
    public float _baseSpeed;
    public float _rushSpeed;
    public float _fleeSpeed;
    public float _sneakSpeed;

    [Header("Behaviour Propabilities")]
    [Range(0,1)]
    public float _rushBaseChance;
    [Range(0, 1)]
    public float _sneakBaseChance;
    [Range(0, 1)]
    public float _teleportBaseChance;
    [Range(0, 1)]
    public float _frontalBaseChance;
    [Range(0, 1)]
    public float _chillBaseChance;

    float _rushChance;
    float _sneakChance;
    float _teleportChance;
    float _frontalChance;
    float _chillChance;




    Behaviour _currentBehaviour;


    [Header("Meta")]

    public GameObject _player;                                                  // public is dummy
    Vector3 _playerViewDirection;
    NavMeshAgent _nma;
    Vector3 _target;

    float _targetRefreshTimer;

	
	void Awake ()
    {
        // _player = PlayerControllerScript._controller.gameObject;        
        _nma = GetComponent<NavMeshAgent>();
        _targetRefreshTimer = _targetRefreshRate;
        _playerViewDirection = _player.transform.forward;
        _nma.speed = _baseSpeed;
    }	
	
	void Update ()
    {
        RefreshNMA();
	}

    void RefreshNMA()
    {
        if (_targetRefreshTimer <= 0)
        {
            _nma.SetDestination(_target);


            _targetRefreshTimer = _targetRefreshRate;
        }
        _targetRefreshRate -= Time.deltaTime;
    }

    void CalculateBehaviour()
    {       
        float ran = Random.Range(0, 5);
        //if(ran <= )
    }

    void Flee()
    {
        Vector3 fleeTo;
        fleeTo = (transform.position - _player.transform.position) * _flightDistance;
        _target = fleeTo;
        _nma.speed = _fleeSpeed;
        _currentBehaviour = Behaviour.flee;
    }

    void SneakAttack()
    {
        _nma.speed = _sneakSpeed;                                   //todo
        _currentBehaviour = Behaviour.sneakAttack;
    }

    void FrontalAttack()
    {
        _nma.speed = _baseSpeed;
        _target = _player.transform.position;
        _currentBehaviour = Behaviour.frontalAttack;
    }

    void RushAttack()
    {
        _nma.speed = _rushSpeed;
        _target = _player.transform.position;
        _currentBehaviour = Behaviour.rushAttack;
    }

    void Chill()
    {
        _target = CreateRandomTarget();
        _nma.speed = _baseSpeed;
        _currentBehaviour = Behaviour.chill;
    }

    void Teleport(Vector3 pos)
    {
        _nma.Warp(pos);
        _currentBehaviour = Behaviour.teleport;
    }

    Vector3 CreateRandomTarget()
    {
        return new Vector3();                       //dummy
    }
    
}

enum Behaviour
{
    chill = 0,
    frontalAttack = 1,
    rushAttack = 2,
    sneakAttack = 3,
    teleport = 4,
    flee = 5
};

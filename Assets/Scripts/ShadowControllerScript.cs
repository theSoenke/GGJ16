using UnityEngine;
using System.Collections;

public class ShadowControllerScript : MonoBehaviour
{
    [Header("Behaviour")]

    public float _viewRange;
    public float _targetRefreshRate;
    public float _flightDistance;
    public float _baseSpeed;
    public float _rushSpeed;
    public float _fleeSpeed;
    public float _sneakSpeed;

    [Header("Behaviour Propabilities")]
    [Range(0, 1)]
    public float _rushBaseChance;
    [Range(0, 1)]
    public float _sneakBaseChance;
    [Range(0, 1)]
    public float _frontalBaseChance;
    [Range(0, 1)]
    public float _chillBaseChance;

    float _rushChance;
    float _sneakChance;
    float _frontalChance;
    float _chillChance;




    Behaviour _currentBehaviour;


    [Header("Meta")]

    GameObject _player;
    PlayerController _playerScript;
    int _playerHealth;
    Vector3 _playerViewDirection;
    NavMeshAgent _nma;
    GameObject _target;

    float _targetRefreshTimer;
    bool _playerSeen = false;
    float _behaviourChangeTimer;


    void Start()
    {
        _player = PlayerController.Instance.gameObject;
        _playerScript = PlayerController.Instance;
        _nma = GetComponent<NavMeshAgent>();
        _targetRefreshTimer = _targetRefreshRate;
        _playerViewDirection = _player.transform.forward;

        Chill();
    }

    void Update()
    {
        RefreshNMA();
        if (_playerSeen)
        {
            RefreshBehaviourChange();
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            AttackPlayer();
        }
    }



    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("LightTrail"))
        {
            // TODO freeze shadow?
            Debug.Log("Hit shadow");
        }
    }


    void CheckDoesNoticePlayer()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) <= _viewRange && !_playerSeen)
        {
            _playerSeen = true;
            print("player seen");
            CalculateBehaviour();
        }
    }

    void RefreshBehaviourChange()
    {

        if (_behaviourChangeTimer <= 0)
        {
            print("asdf");
            CalculateBehaviour();
        }
        _behaviourChangeTimer -= Time.deltaTime;
    }

    void RefreshNMA()
    {
        if (_targetRefreshTimer <= 0)
        {

            if (_target != null)
                _nma.SetDestination(_target.transform.position);
            CheckDoesNoticePlayer();

            _targetRefreshTimer = _targetRefreshRate;
        }
        _targetRefreshTimer -= Time.deltaTime;
    }


    void CalculatePropabilities()
    {
        float min = 0;
        _rushChance = _rushBaseChance;
        min += _rushChance - min;

        _sneakChance = min + _sneakBaseChance;
        min += _sneakChance - min;

        _frontalChance = min + _frontalBaseChance;
        min += _frontalChance - min;

        _chillChance = min + _chillBaseChance;
        min += _chillChance - min;

        _rushChance /= min;
        _sneakChance /= min;
        _frontalChance /= min;
        _chillChance /= min;
        print("chill: " + _chillChance);

    }

    void CalculateBehaviour()
    {
        CalculatePropabilities();
        float ran = Random.value;
        if (ran <= _rushChance)
        {
            RushAttack();
        }
        else if (ran <= _sneakChance)
        {
            SneakAttack();
        }
        else if (ran <= _frontalChance)
        {
            FrontalAttack();
        }
        else if (ran <= _chillChance)
        {
            Chill();
        }
        else
        {
            print("error: weightedRNG failure");
        }
    }

    void AttackPlayer()
    {
        _playerScript.DamagePlayer(20);
        Destroy(gameObject);
    }



    void Flee()
    {
        Vector3 fleeTo;
        fleeTo = (transform.position - _player.transform.position) * _flightDistance;
        _target = null;
        _nma.SetDestination(fleeTo);
        _nma.speed = _fleeSpeed;
        _currentBehaviour = Behaviour.flee;
        _behaviourChangeTimer = 3 + Random.Range(0, 3);
    }

    void SneakAttack()
    {
        _nma.speed = _sneakSpeed;                                   //todo
        _currentBehaviour = Behaviour.sneakAttack;
        _behaviourChangeTimer = 10 + Random.Range(0, 5);
    }

    void FrontalAttack()
    {
        _nma.speed = _baseSpeed;
        _target = _player;
        _currentBehaviour = Behaviour.frontalAttack;
        _behaviourChangeTimer = 20 + Random.Range(-5, 5);
    }

    void RushAttack()
    {
        _nma.speed = _rushSpeed;
        _target = _player;
        _currentBehaviour = Behaviour.rushAttack;
        _behaviourChangeTimer = 17 + Random.Range(-5, 5);
    }

    void Chill()
    {
        _target = null;
        _nma.SetDestination(CreateRandomTarget());
        print(_nma.destination);
        _nma.speed = _baseSpeed;
        _currentBehaviour = Behaviour.chill;
        _behaviourChangeTimer = 7 + Random.Range(-1, 5);
    }

    Vector3 CreateRandomTarget()
    {
        Vector3 pos = new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50));

        while (UsefullStuff.InsideCollision(pos))
            pos = new Vector3(Random.Range(-60, 60), Random.Range(10, 15), Random.Range(-60, 60));

        return pos;
    }
}

enum Behaviour
{
    chill = 0,
    frontalAttack = 1,
    rushAttack = 2,
    sneakAttack = 3,
    flee = 5,
    idle = 6
};

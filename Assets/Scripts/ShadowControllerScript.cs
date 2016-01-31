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

    [Header("Effects")]
    public GameObject explosionEffect;

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

    AudioSource _pain;

    float _targetRefreshTimer;
    bool _playerSeen = false;
    float _behaviourChangeTimer;


    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerScript = _player.GetComponent<PlayerController>();
        _nma = GetComponent<NavMeshAgent>();
        _targetRefreshTimer = _targetRefreshRate;
        _playerViewDirection = _player.transform.forward;
        _pain = GetComponents<AudioSource>()[1];

        Chill();
    }

    void Update()
    {
        RefreshBehaviourChange();
        if(_targetRefreshTimer<=0 && _currentBehaviour != Behaviour.chill)
        {
            _nma.SetDestination(_player.transform.position);
            _targetRefreshTimer = _targetRefreshRate;
        }
        _targetRefreshTimer -= Time.deltaTime;
    }

    private void refreshNMA()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("LightBall"))
        {
            _pain.Play();
            Chill();

            GameObject explosion = (GameObject)Instantiate(explosionEffect, transform.position, Quaternion.identity);
            ParticleSystem particle = explosion.GetComponent<ParticleSystem>();

            Destroy(collider.gameObject);
            Destroy(explosion, particle.duration);

            Debug.Log("Hit shadow");
        }
        if (collider.gameObject.tag.Equals("Player"))
        {
            AttackPlayer();
            print("player damaged");
        }
    }

    void RefreshBehaviourChange()
    {
        if (_behaviourChangeTimer <= 0)
        {
            CalculateBehaviour();
        }
        _behaviourChangeTimer -= Time.deltaTime;
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
        _playerScript.DamagePlayer();
        GameManager.instance._spawnedShadows.Remove(gameObject);
        Destroy(gameObject);
    }



    /*    void Flee()
        {

            Vector3 fleeTo;
            int tries = 1;
            fleeTo = (_player.transform.position - transform.position ) * _flightDistance;     
            while (UsefullStuff.InsideCollision(fleeTo))
            {
                fleeTo += new Vector3(Random.Range(-5, 5), Random.Range(-1, 2), Random.Range(-5, 5));
                tries++;
                if (tries >= 10)
                {
                    GameManager.instance.spawnShadows(1);
                    Destroy(gameObject);
                }
            }
            print("flee");
            _target = null;
            print(fleeTo);
            _nma.SetDestination(fleeTo);
            _nma.speed = _fleeSpeed;
            _currentBehaviour = Behaviour.flee;
            _behaviourChangeTimer = 10 + Random.Range(0, 5);
        } */

    void SneakAttack()
    {
        print("sneak");
        _nma.speed = _sneakSpeed;                                   //todo
        _currentBehaviour = Behaviour.sneakAttack;
        _behaviourChangeTimer = 10 + Random.Range(0, 5);
    }

    public void Stun()
    {
        _nma.speed = 0;
        _currentBehaviour = Behaviour.stuned;
        _behaviourChangeTimer = 3.5f;
    }

    void FrontalAttack()
    {
        print("frontal");
        _nma.speed = _baseSpeed;
        _nma.SetDestination(_player.transform.position);
        _currentBehaviour = Behaviour.frontalAttack;
        _behaviourChangeTimer = 20 + Random.Range(-5, 2);
    }

    void RushAttack()
    {
        print("rush");
        _nma.speed = _rushSpeed;
        _nma.SetDestination(_player.transform.position);
        _currentBehaviour = Behaviour.rushAttack;
        _behaviourChangeTimer = 5 + Random.Range(-3, 5);
    }

    void Chill()
    {
        _nma.SetDestination(CreateRandomTarget());
        print("chill");
        _nma.speed = _baseSpeed;
        _currentBehaviour = Behaviour.chill;
        _behaviourChangeTimer = 2 + Random.Range(0, 2);

    }

    Vector3 CreateRandomTarget()
    {
        GameObject gm = (GameObject) GameObject.FindGameObjectWithTag("GameManager") ;
        GameManager gmm = gm.GetComponent<GameManager>();



        return gmm.spawnPoints[Random.Range(0, gmm.spawnPoints.Length)].position;
    }

    void OnDrawGizmos()
    {
        if (_nma != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_nma.destination, 5);
        }
    }
}

enum Behaviour
{
    chill = 0,
    frontalAttack = 1,
    rushAttack = 2,
    sneakAttack = 3,
    idle = 4,
    stuned = 5
};

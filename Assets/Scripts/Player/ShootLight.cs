using UnityEngine;

public class ShootLight : MonoBehaviour
{
    public GameObject lightTrail;
    public Transform weaponPosition;
    public float lightSpeed = 100f;
    public float lightTrailLifetime;
    public float durationBetweenShots = 1f;

    private AudioSource _shootAS;
    private float _cooldown;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ThrowLight();
        }

        _cooldown -= Time.deltaTime;
    }

    void Awake()
    {
        _shootAS = GetComponents<AudioSource>()[2];
    }

    private void ThrowLight()
    {
        if (_cooldown <= 0)
        {
            _shootAS.Play();
            GameObject trailGo = (GameObject)Instantiate(lightTrail, weaponPosition.position, Quaternion.identity);
            Rigidbody trailRigidbody = trailGo.GetComponent<Rigidbody>();

            Vector3 forceVector = Camera.main.transform.forward * lightSpeed;
            trailRigidbody.AddForce(forceVector);
            Destroy(trailGo, lightTrailLifetime);

            _cooldown = durationBetweenShots;
        }
        else
        {
            // TODO play sound
        }
    }
}

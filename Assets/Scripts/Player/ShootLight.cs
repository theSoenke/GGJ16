using UnityEngine;

public class ShootLight : MonoBehaviour
{
    public GameObject lightTrail;
    public Transform weaponPosition;
    public float lightSpeed = 100f;
    public float lightTrailLifetime;

    private AudioSource _shootAS;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ThrowLight();
        }
    }

    void Awake()
    {
        _shootAS = GetComponents<AudioSource>()[2];
    }

    private void ThrowLight()
    {
        if (PlayerController.Instance.LightsNum > 0)
        {
            _shootAS.Play();
            GameObject trailGo = (GameObject)Instantiate(lightTrail, weaponPosition.position, Quaternion.identity);
            Rigidbody trailRigidbody = trailGo.GetComponent<Rigidbody>();

            Vector3 forceVector = Camera.main.transform.forward * lightSpeed;
            trailRigidbody.AddForce(forceVector);
            Destroy(trailGo, lightTrailLifetime);

            PlayerController.Instance.UsedLight();
        }
    }
}

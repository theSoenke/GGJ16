using UnityEngine;
using System.Collections;

public class ShootLight : MonoBehaviour
{
    public GameObject lightTrail;
    public Transform weaponPosition;
    public float lightSpeed = 100f;
    public float destructionTime = 5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ThrowLight();
        }
    }

    private void ThrowLight()
    {
        GameObject trailGo = (GameObject)Instantiate(lightTrail, weaponPosition.position, Quaternion.identity);
        Rigidbody trailRigidbody = trailGo.GetComponent<Rigidbody>();

        Vector3 forceVector = weaponPosition.TransformDirection(weaponPosition.forward * lightSpeed);
        trailRigidbody.AddForce(forceVector);
    }
}

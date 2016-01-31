using UnityEngine;

public class Activator : MonoBehaviour
{
    public GameObject activateGo;

    void OnTriggerEnter(Collider other)
    {
        activateGo.SetActive(true);
    }
}

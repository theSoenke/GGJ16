using UnityEngine;

public class MouseRotator : MonoBehaviour
{
    public Vector2 rotationRange = new Vector3(70, 70);
    public float rotationSpeed = 10;
    public float dampingTime = 0.2f;

    private Vector3 targetAngles;
    private Vector3 followAngles;
    private Vector3 followVelocity;
    private Quaternion originalRotation;

    void Start()
    {
        originalRotation = transform.localRotation;
    }
    void Update()
    {
        transform.localRotation = originalRotation;

        float inputHorizontal = 0;
        float inputVertical = 0;

        inputHorizontal = Input.GetAxis("Mouse X");
        inputVertical = Input.GetAxis("Mouse Y");

        if (targetAngles.y > 180)
        {
            targetAngles.y -= 360; followAngles.y -= 360;
        }
        if (targetAngles.x > 180)
        {
            targetAngles.x -= 360; followAngles.x -= 360;
        }
        if (targetAngles.y < -180)
        {
            targetAngles.y += 360; followAngles.y += 360;
        }
        if (targetAngles.x < -180)
        {
            targetAngles.x += 360; followAngles.x += 360;
        }

        targetAngles.y += inputHorizontal * rotationSpeed;
        targetAngles.x += inputVertical * rotationSpeed;

        targetAngles.y = Mathf.Clamp(targetAngles.y, -rotationRange.y * 0.5f, rotationRange.y * 0.5f);
        targetAngles.x = Mathf.Clamp(targetAngles.x, -rotationRange.x * 0.5f, rotationRange.x * 0.5f);


        followAngles = Vector3.SmoothDamp(followAngles, targetAngles, ref followVelocity, dampingTime);
        transform.localRotation = originalRotation * Quaternion.Euler(-followAngles.x, followAngles.y, 0);
    }
}

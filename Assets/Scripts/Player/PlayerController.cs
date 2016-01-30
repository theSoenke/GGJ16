using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [Header("Movement")]
    public float speed = 8f;
    public float strafeSpeed = 4f;
    private float jumpPower = 5f;
    public float walkSpeed = 3f;
    public bool lockCursor = true;

    private CapsuleCollider capsule;
    private const float jumpRayLength = 0.7f;
    public bool grounded { get; private set; }
    private Vector2 input;
    private IComparer rayHitComparer;

    private int collectedLights = 0;
    private int health = 100;

    public int LightsNum
    {
        get
        {
            return collectedLights;
        }
    }

    public int Health
    {
        get
        {
            return health;
        }
    }

    public void DamagePlayer(int damage)
    {
        health -= damage;
    }

    void Awake()
    {
        Instance = this;
        capsule = GetComponent<Collider>() as CapsuleCollider;
        grounded = true;
        Screen.lockCursor = lockCursor;
        rayHitComparer = new RayHitComparer();
    }

    void OnDisable()
    {
        Screen.lockCursor = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Light"))
        {
            Debug.Log("Collected light");
            collectedLights++;
            Destroy(other.gameObject);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Screen.lockCursor = lockCursor;
        }
    }

    public void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool jump = Input.GetButton("Jump");

        bool walkOrRun = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = walkOrRun ? speed : walkSpeed;

        input = new Vector2(horizontal, vertical);

        // normalize input if it exceeds 1 in combined length:
        if (input.sqrMagnitude > 1) input.Normalize();

        // Get a vector which is desired move as a world-relative direction, including speeds
        Vector3 movement = transform.forward * input.y * currentSpeed + transform.right * input.x * strafeSpeed;

        // preserving current y velocity (for falling, gravity)
        float yv = GetComponent<Rigidbody>().velocity.y;

        // add jump power
        if (grounded && jump)
        {
            yv += jumpPower;
            grounded = false;
        }

        // Set the rigidbody's velocity according to the ground angle and desired move
        GetComponent<Rigidbody>().velocity = movement + Vector3.up * yv;

        // Ground Check:
        // Create a ray that points down from the centre of the character.
        Ray ray = new Ray(transform.position, -transform.up);

        // Raycast slightly further than the capsule (as determined by jumpRayLength)
        RaycastHit[] hits = Physics.RaycastAll(ray, capsule.height * jumpRayLength);
        System.Array.Sort(hits, rayHitComparer);


        if (grounded || GetComponent<Rigidbody>().velocity.y < jumpPower * .5f)
        {
            // Default value if nothing is detected:
            grounded = false;
            // Check every collider hit by the ray
            for (int i = 0; i < hits.Length; i++)
            {
                // Check it's not a trigger
                if (!hits[i].collider.isTrigger)
                {
                    // The character is grounded, and we store the ground angle (calculated from the normal)
                    grounded = true;

                    GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 0, GetComponent<Rigidbody>().velocity.z);
                    break;
                }
            }
        }
    }


    //used for comparing distances
    class RayHitComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            return ((RaycastHit)x).distance.CompareTo(((RaycastHit)y).distance);
        }
    }
}

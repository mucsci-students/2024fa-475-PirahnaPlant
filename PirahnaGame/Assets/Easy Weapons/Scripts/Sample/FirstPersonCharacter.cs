using UnityEngine;
using System.Collections;

public class FirstPersonCharacter : MonoBehaviour
{
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float strafeSpeed = 4f;
    [SerializeField] private float jumpPower = 5f;
    public GameObject moneyManager;

    [SerializeField] private AdvancedSettings advanced = new AdvancedSettings();
    [SerializeField] private bool lockCursor = true;

    [System.Serializable]
    public class AdvancedSettings
    {
        public float gravityMultiplier = 1f;
        public PhysicMaterial zeroFrictionMaterial;
        public PhysicMaterial highFrictionMaterial;
        public float groundStickyEffect = 5f;
    }

    private CapsuleCollider capsule;
    private const float jumpRayLength = 0.7f;
    public bool grounded { get; private set; }
    private Vector2 input;
    private IComparer rayHitComparer;

    // Reference to PauseMenu script
    private PauseMenu pauseMenu;

    void Awake()
    {
        capsule = GetComponent<Collider>() as CapsuleCollider;
        grounded = true;
        rayHitComparer = new RayHitComparer();

        // Find the MenuManager GameObject and access the PauseMenu script
        GameObject menuManager = GameObject.Find("MenuManager"); // Assuming the object is named "MenuManager"
        if (menuManager != null)
        {
            pauseMenu = menuManager.GetComponent<PauseMenu>();
        }

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
		if (pauseMenu != null && pauseMenu.paused) return;
        // Handle cursor visibility and lock state (escape key to unlock)
        if (Input.GetMouseButtonUp(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void FixedUpdate()
    {
        // Check if the game is paused before processing input or movement
        if (pauseMenu != null && pauseMenu.paused) return;  // Skip movement if game is paused

        float speed = runSpeed;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool jump = Input.GetButton("Jump");

        input = new Vector2(h, v);

        // Normalize input if it exceeds 1 in combined length:
        if (input.sqrMagnitude > 1) input.Normalize();

        // Get a vector which is desired move as a world-relative direction, including speeds
        Vector3 desiredMove = transform.forward * input.y * speed + transform.right * input.x * strafeSpeed;

        // Preserve current y velocity (for falling, gravity)
        float yv = GetComponent<Rigidbody>().velocity.y;

        // Add jump power if grounded
        if (grounded && jump)
        {
            yv += jumpPower;
            grounded = false;
        }

        // Set the Rigidbody's velocity according to the ground angle and desired move
        GetComponent<Rigidbody>().velocity = desiredMove + Vector3.up * yv;

        // Use low/high friction depending on whether we're moving or not
        if (desiredMove.magnitude > 0 || !grounded)
        {
            GetComponent<Collider>().material = advanced.zeroFrictionMaterial;
        }
        else
        {
            GetComponent<Collider>().material = advanced.highFrictionMaterial;
        }

        // Ground Check:
        Ray ray = new Ray(transform.position, -transform.up);

        RaycastHit[] hits = Physics.RaycastAll(ray, capsule.height * jumpRayLength);
        System.Array.Sort(hits, rayHitComparer);

        if (grounded || GetComponent<Rigidbody>().velocity.y < jumpPower * .5f)
        {
            grounded = false;
            for (int i = 0; i < hits.Length; i++)
            {
                if (!hits[i].collider.isTrigger)
                {
                    grounded = true;
                    GetComponent<Rigidbody>().position = Vector3.MoveTowards(GetComponent<Rigidbody>().position, hits[i].point + Vector3.up * capsule.height * 0.5f, Time.deltaTime * advanced.groundStickyEffect);
                    GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 0, GetComponent<Rigidbody>().velocity.z);
                    break;
                }
            }
        }

        Debug.DrawRay(ray.origin, ray.direction * capsule.height * jumpRayLength, grounded ? Color.green : Color.red);

        // Add extra gravity
        GetComponent<Rigidbody>().AddForce(Physics.gravity * (advanced.gravityMultiplier - 1));
    }

    class RayHitComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            return ((RaycastHit)x).distance.CompareTo(((RaycastHit)y).distance);
        }
    }
}

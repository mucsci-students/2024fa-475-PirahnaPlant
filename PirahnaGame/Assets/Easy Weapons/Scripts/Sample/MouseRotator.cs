using UnityEngine;

public class MouseRotator : MonoBehaviour
{
    public Vector2 rotationRange = new Vector2(70, 70);  // Rotation limits for X and Y axes
    public float rotationSpeed = 10f;  // Speed of rotation
    public float dampingTime = 0.2f;   // Smoothing for rotation
    public bool autoZeroVerticalOnMobile = true;
    public bool autoZeroHorizontalOnMobile = false;
    public bool relative = true;

    private Vector3 targetAngles;
    private Vector3 followAngles;
    private Vector3 followVelocity;
    private Quaternion originalRotation;

    // Reference to PauseMenu script to check paused state
    private PauseMenu pauseMenu;

    void Start()
    {
        originalRotation = transform.localRotation;

        // Find the MenuManager GameObject and access the PauseMenu script
        GameObject menuManager = GameObject.Find("MenuManager"); // Assuming the object is named "MenuManager"
        if (menuManager != null)
        {
            pauseMenu = menuManager.GetComponent<PauseMenu>();
        }
    }

    void Update()
    {
        // Skip rotation logic if the game is paused
        if (pauseMenu != null && pauseMenu.paused) return;

        // Reset rotation to the original rotation before applying new input
        transform.localRotation = originalRotation;

        float inputH = 0;
        float inputV = 0;

        // Mouse input handling based on relative setting
        if (relative)
        {
            inputH = Input.GetAxis("Mouse X");
            inputV = Input.GetAxis("Mouse Y");

            // Wrap values to prevent sudden spring-back from positive to negative
            if (targetAngles.y > 180) { targetAngles.y -= 360; followAngles.y -= 360; }
            if (targetAngles.x > 180) { targetAngles.x -= 360; followAngles.x -= 360; }
            if (targetAngles.y < -180) { targetAngles.y += 360; followAngles.y += 360; }
            if (targetAngles.x < -180) { targetAngles.x += 360; followAngles.x += 360; }

            // Apply mouse input to target angles, scaled by rotation speed
            targetAngles.y += inputH * rotationSpeed;
            targetAngles.x += inputV * rotationSpeed;

            // Clamp target angles to stay within defined rotation range
            targetAngles.y = Mathf.Clamp(targetAngles.y, -rotationRange.y * 0.5f, rotationRange.y * 0.5f);
            targetAngles.x = Mathf.Clamp(targetAngles.x, -rotationRange.x * 0.5f, rotationRange.x * 0.5f);
        }
        else
        {
            inputH = Input.mousePosition.x;
            inputV = Input.mousePosition.y;

            // Set values to allowed range based on mouse position relative to screen size
            targetAngles.y = Mathf.Lerp(-rotationRange.y * 0.5f, rotationRange.y * 0.5f, inputH / Screen.width);
            targetAngles.x = Mathf.Lerp(-rotationRange.x * 0.5f, rotationRange.x * 0.5f, inputV / Screen.height);
        }

        // Smoothly interpolate to the target angles
        followAngles = Vector3.SmoothDamp(followAngles, targetAngles, ref followVelocity, dampingTime);

        // Apply the smoothed rotation to the object
        transform.localRotation = originalRotation * Quaternion.Euler(-followAngles.x, followAngles.y, 0);
    }
}

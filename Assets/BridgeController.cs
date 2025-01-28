using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    public Transform leftDoorPivot; // Assign LeftDoorPivot in Inspector
    public Transform rightDoorPivot; // Assign RightDoorPivot in Inspector

    public float openAngle = 90f; // Angle for doors to open
    public float openSpeed = 2f; // Speed of door opening/closing
    public float waitTime = 2f; // Time to wait before closing again

    private bool isOpening = true; // Toggle for opening and closing
    private float timer;

    void Start()
    {
        timer = waitTime; // Initialize the timer
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            isOpening = !isOpening; // Switch between opening and closing
            timer = waitTime; // Reset the timer
        }

        RotateDoors();
    }
    void RotateDoors()
    {
        float targetLeftAngle = isOpening ? openAngle : 0f;
        float targetRightAngle = isOpening ? -openAngle : 0f;

        // Rotate the pivot objects
        leftDoorPivot.localRotation = Quaternion.Lerp(
            leftDoorPivot.localRotation,
            Quaternion.Euler(0f, 0f, targetLeftAngle),
            Time.deltaTime * openSpeed
        );

        rightDoorPivot.localRotation = Quaternion.Lerp(
            rightDoorPivot.localRotation,
            Quaternion.Euler(0f, 0f, targetRightAngle),
            Time.deltaTime * openSpeed
        );
    }
}

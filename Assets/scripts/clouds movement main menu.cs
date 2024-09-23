using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;   // Control the speed of movement
    [SerializeField] private float moveDistance = 5f; // Distance the cloud moves before changing direction

    private Vector3 startPos;
    private bool movingRight = true;

    void Start()
    {
        startPos = transform.position; // Store the starting position of the cloud
    }

    void Update()
    {
        // Calculate movement direction
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        // Reverse direction when the cloud reaches the move distance limit
        if (Vector2.Distance(startPos, transform.position) >= moveDistance)
        {
            movingRight = !movingRight;
            startPos = transform.position; // Reset the position to maintain the oscillating distance
        }
    }
}

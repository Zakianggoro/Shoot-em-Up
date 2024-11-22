using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Speed of the player movement
    public float minX = -5f; // Minimum X position
    public float maxX = 5f;  // Maximum X position

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate the movement direction
        Vector3 movement = new Vector3(horizontalInput * speed * Time.deltaTime, 0, 0);

        // Move the player only on the X-axis
        transform.Translate(movement, Space.World);

        // Clamp the player's position within boundaries
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}

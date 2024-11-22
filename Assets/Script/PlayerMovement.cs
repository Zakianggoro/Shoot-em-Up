using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Speed of the player movement

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate the movement direction
        Vector3 movement = new Vector3(horizontalInput * speed * Time.deltaTime, 0, 0);

        // Move the player only on the X-axis
        transform.Translate(movement, Space.World);
    }
}
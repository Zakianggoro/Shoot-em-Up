using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 10; // Health of the enemy
    public int points = 10; // Points awarded for killing this enemy
    public float speed = 2f; // Speed of the enemy

    private Transform player;

    void Start()
    {
        // Find the player in the scene
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(5);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            PlayerShooting playerShooting = playerObject.GetComponent<PlayerShooting>();
            if (playerShooting != null)
            {
                playerShooting.AddScore(points);
            }
        }

        Destroy(gameObject); 
    }
}

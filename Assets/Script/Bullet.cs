using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 5;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }

    void Start()
    {
        Destroy(gameObject, 5f);
    }
}

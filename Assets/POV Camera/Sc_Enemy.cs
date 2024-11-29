using UnityEngine;

public class Sc_Enemy : MonoBehaviour
{
    public float health = 100f;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject); // Musuh hancur ketika HP <= 0
        }
    }
}

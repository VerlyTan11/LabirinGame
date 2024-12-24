using UnityEngine;

public class BearAI : MonoBehaviour
{
    public float detectionRadius = 40f; // Radius deteksi untuk pemain
    public float attackRadius = 15f; // Radius serangan
    public float walkSpeed = 5f; // Kecepatan jalan
    public float runSpeed = 4f; // Kecepatan lari
    public int attackDamage = 10; // Damage yang diberikan oleh weapon

    private Transform player; // Referensi ke pemain
    private Animator animator; // Animator bear
    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null)
        {
            GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
            if (foundPlayer != null)
            {
                player = foundPlayer.transform;
            }
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRadius)
        {
            AttackPlayer();
        }
        else if (distanceToPlayer <= detectionRadius)
        {
            ChasePlayer();
        }
        else
        {
            Idle();
        }
    }

    void ChasePlayer()
    {
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsAttacking", false);

        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * walkSpeed * Time.deltaTime;
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }

    void AttackPlayer()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            animator.SetBool("IsAttacking", true);
            animator.SetBool("IsWalking", false);
            Invoke("DealDamage", 0.5f); // Delay untuk memberikan damage
        }
    }

    void DealDamage()
    {
        if (player != null)
        {
            CharacterHealth playerHealth = player.GetComponent<CharacterHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }
        isAttacking = false;
    }

    void Idle()
    {
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsAttacking", false);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}

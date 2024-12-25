// using UnityEngine;

// public class BearAI : MonoBehaviour
// {
//     public float detectionRadius = 40f; // Radius deteksi untuk pemain
//     public float attackRadius = 15f; // Radius serangan
//     public float walkSpeed = 5f; // Kecepatan jalan
//     public float runSpeed = 4f; // Kecepatan lari
//     public int attackDamage = 10; // Damage yang diberikan oleh weapon
//     public float raycastDistance = 1f; // Jarak raycast untuk deteksi ground

//     private Transform player; // Referensi ke pemain
//     private Animator animator; // Animator bear
//     private bool isAttacking = false;

//     void Start()
//     {
//         animator = GetComponent<Animator>();
//     }

//     void Update()
//     {
//         if (player == null)
//         {
//             GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
//             if (foundPlayer != null)
//             {
//                 player = foundPlayer.transform;
//             }
//             return;
//         }

//         float distanceToPlayer = Vector3.Distance(transform.position, player.position);

//         if (distanceToPlayer <= attackRadius)
//         {
//             AttackPlayer();
//         }
//         else if (distanceToPlayer <= detectionRadius)
//         {
//             ChasePlayer();
//         }
//         else
//         {
//             Idle();
//         }
//     }

//     void ChasePlayer()
//     {
//         animator.SetBool("IsWalking", true);
//         animator.SetBool("IsAttacking", false);

//         Vector3 direction = (player.position - transform.position).normalized;

//         // Cek apakah ada ground di jalur bear menggunakan raycast
//         if (!IsGroundBlocked(direction))
//         {
//             transform.position += direction * walkSpeed * Time.deltaTime;
//             transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
//         }
//         else
//         {
//             // Jika ada ground, hentikan pergerakan
//             Idle();
//         }
//     }

//     void AttackPlayer()
//     {
//         if (!isAttacking)
//         {
//             isAttacking = true;
//             animator.SetBool("IsAttacking", true);
//             animator.SetBool("IsWalking", false);
//             Invoke("DealDamage", 0.5f); // Delay untuk memberikan damage
//         }
//     }

//     void DealDamage()
//     {
//         if (player != null)
//         {
//             CharacterHealth playerHealth = player.GetComponent<CharacterHealth>();
//             if (playerHealth != null)
//             {
//                 playerHealth.TakeDamage(attackDamage);
//             }
//         }
//         isAttacking = false;
//     }

//     void Idle()
//     {
//         animator.SetBool("IsWalking", false);
//         animator.SetBool("IsAttacking", false);
//     }

//     bool IsGroundBlocked(Vector3 direction)
//     {
//         RaycastHit hit;
//         Vector3 rayOrigin = transform.position + Vector3.up * 0.5f; // Mulai ray dari sedikit di atas posisi bear
//         if (Physics.Raycast(rayOrigin, direction, out hit, raycastDistance))
//         {
//             if (hit.collider.CompareTag("Ground"))
//             {
//                 Debug.Log("Ground detected, movement blocked.");
//                 return true; // Ada ground yang menghalangi
//             }
//         }
//         return false; // Tidak ada ground yang menghalangi
//     }

//     void OnDrawGizmosSelected()
//     {
//         Gizmos.color = Color.yellow;
//         Gizmos.DrawWireSphere(transform.position, detectionRadius);

//         Gizmos.color = Color.red;
//         Gizmos.DrawWireSphere(transform.position, attackRadius);

//         // Tampilkan ray untuk debug
//         Gizmos.color = Color.blue;
//         Gizmos.DrawRay(transform.position + Vector3.up * 0.5f, transform.forward * raycastDistance);
//     }
// }


using UnityEngine;

public class BearAI : MonoBehaviour
{
    public float detectionRadius = 40f; // Radius deteksi untuk pemain
    public float attackRadius = 15f; // Radius serangan
    public float walkSpeed = 5f; // Kecepatan jalan
    public float runSpeed = 4f; // Kecepatan lari
    public int attackDamage = 10; // Damage yang diberikan oleh weapon
    public float raycastDistance = 2f; // Jarak raycast untuk deteksi ground

    private Transform player; // Referensi ke pemain
    private Animator animator; // Animator bear
    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        // Adjust spawn position to be on ground
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 2f, Vector3.down, out hit, 5f))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                transform.position = hit.point; // Posisikan Bear tepat di ground
            }
        }
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
        direction.y = 0; // Hindari perubahan pada sumbu y

        // Cek apakah ada ground di jalur bear menggunakan raycast
        if (!IsGroundBlocked(direction))
        {
            transform.position += direction * walkSpeed * Time.deltaTime;
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        }
        else
        {
            Debug.Log("Path is blocked, stopping movement.");
            Idle();
        }
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

    bool IsGroundBlocked(Vector3 direction)
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position + Vector3.up * 0.5f; // Mulai ray dari sedikit di atas posisi bear
        if (Physics.Raycast(rayOrigin, direction, out hit, raycastDistance))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                Debug.Log("Ground detected, movement blocked.");
                return true; // Ada ground yang menghalangi
            }
        }
        return false; // Tidak ada ground yang menghalangi
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);

        // Tampilkan ray untuk debug
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position + Vector3.up * 0.5f, transform.forward * raycastDistance);
    }
}

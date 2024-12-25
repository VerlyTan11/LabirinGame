// using System.Collections;
// using UnityEngine;

// public class CharacterHealth : MonoBehaviour
// {
//     public float maxHealth = 100f; // Kesehatan maksimal
//     private float currentHealth; // Kesehatan saat ini
//     public Transform healthBar; // Referensi ke image darah
//     private Vector3 initialScale; // Skala awal health bar
//     private Vector3 spawnPosition; // Posisi spawn awal
//     private Quaternion spawnRotation; // Rotasi spawn awal

//     void Start()
//     {
//         // Simpan posisi dan rotasi spawn awal
//         spawnPosition = transform.position;
//         spawnRotation = transform.rotation;

//         // Inisialisasi kesehatan
//         ResetHealth();
//     }

//     void Update()
//     {
//         // Logika tambahan untuk debug atau efek visual bisa ditambahkan di sini
//     }

//     void UpdateHealthBar()
//     {
//         if (healthBar != null)
//         {
//             // Hitung skala baru berdasarkan kesehatan saat ini
//             float healthPercentage = Mathf.Clamp(currentHealth / maxHealth, 0, 1);
//             healthBar.transform.localScale = new Vector3(initialScale.x * healthPercentage, initialScale.y, initialScale.z);
//         }
//     }

//     public void TakeDamage(float damage)
//     {
//         currentHealth -= damage;
//         UpdateHealthBar();

//         // Periksa jika kesehatan habis
//         if (currentHealth <= 0)
//         {
//             Respawn();
//         }
//     }

//     void Respawn()
//     {
//         // Reset posisi
//         transform.position = spawnPosition;
//         transform.rotation = spawnRotation;

//         // Reset kesehatan
//         ResetHealth();

//         // Optional: Matikan sementara collider atau tambahkan efek kebal setelah respawn
//         // Contoh: StartCoroutine(RespawnInvincibility());
//     }

//     void ResetHealth()
//     {
//         currentHealth = maxHealth;

//         // Reset skala health bar
//         if (healthBar != null)
//         {
//             healthBar.gameObject.SetActive(true);

//             // Hitung persentase kesehatan
//             float healthPercentage = Mathf.Clamp(currentHealth / maxHealth, 0, 1);

//             // Atur ulang skala health bar ke ukuran penuh
//             if (initialScale == Vector3.zero) // Jika initialScale belum di-set
//             {
//                 initialScale = healthBar.transform.localScale;
//             }

//             // Terapkan initialScale pada health bar untuk memulihkan ukuran penuh
//             healthBar.transform.localScale = new Vector3(initialScale.x * healthPercentage, initialScale.y, initialScale.z);
//         }
//     }

//     void OnTriggerEnter(Collider other)
//     {
//         // Deteksi jika terkena weapon dari monster Bear
//         if (other.CompareTag("Weapon"))
//         {
//             WeaponDamage weaponDamage = other.GetComponent<WeaponDamage>();
//             if (weaponDamage != null)
//             {
//                 TakeDamage(weaponDamage.damage);
//             }
//         }
//     }

//     // Optional: Coroutine untuk memberikan periode kebal setelah respawn
//     /*
//     IEnumerator RespawnInvincibility()
//     {
//         // Nonaktifkan collider atau tandai sebagai tidak dapat diserang
//         // Tunggu beberapa detik
//         yield return new WaitForSeconds(2f);

//         // Aktifkan kembali collider atau status normal
//     }
//     */
// }

using System.Collections;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Kesehatan maksimal
    private float currentHealth; // Kesehatan saat ini
    public Transform healthBar; // Referensi ke image darah
    private Vector3 initialScale; // Skala awal health bar
    private Vector3 spawnPosition; // Posisi spawn awal
    private Quaternion spawnRotation; // Rotasi spawn awal

    void Start()
    {
        // Simpan posisi dan rotasi spawn awal
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;

        // Inisialisasi kesehatan
        ResetHealth();
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            float healthPercentage = Mathf.Clamp(currentHealth / maxHealth, 0, 1);
            healthBar.transform.localScale = new Vector3(initialScale.x * healthPercentage, initialScale.y, initialScale.z);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        // Reset posisi dan rotasi ke spawn point terakhir
        transform.position = spawnPosition;
        transform.rotation = spawnRotation;

        // Reset kesehatan
        ResetHealth();

        Debug.Log("Respawned to last maze.");
    }

    public void SetSpawnPoint(Vector3 position, Quaternion rotation)
    {
        // Perbarui spawn point untuk respawn berikutnya
        spawnPosition = position;
        spawnRotation = rotation;
    }

    void ResetHealth()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            float healthPercentage = Mathf.Clamp(currentHealth / maxHealth, 0, 1);
            if (initialScale == Vector3.zero)
            {
                initialScale = healthBar.transform.localScale;
            }
            healthBar.transform.localScale = new Vector3(initialScale.x * healthPercentage, initialScale.y, initialScale.z);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            WeaponDamage weaponDamage = other.GetComponent<WeaponDamage>();
            if (weaponDamage != null)
            {
                TakeDamage(weaponDamage.damage);
            }
        }
    }
}

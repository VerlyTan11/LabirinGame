// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PortalTeleport : MonoBehaviour
// {
//     // Array untuk menyimpan lokasi teleport
//     public Transform[] teleportLocations;

//     // Event trigger ketika karakter memasuki portal
//     private void OnTriggerEnter(Collider other)
//     {
//         // Pastikan objek yang masuk adalah karakter (misalnya, tag "Player")
//         if (other.CompareTag("Player"))
//         {
//             // Pilih lokasi acak dari array
//             int randomIndex = Random.Range(0, teleportLocations.Length);
//             Transform randomTarget = teleportLocations[randomIndex];

//             // Teleport karakter ke posisi target acak
//             other.transform.position = randomTarget.position;

//             // Opsional: tambahkan efek visual/suara di sini
//             Debug.Log("Karakter telah dipindahkan ke target acak: " + randomTarget.name);
//         }
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    // Array untuk menyimpan lokasi teleport
    public Transform[] teleportLocations;

    // Referensi ke karakter pemain
    private CharacterHealth playerHealth;

    private void Start()
    {
        // Temukan komponen CharacterHealth pada pemain
        playerHealth = FindObjectOfType<CharacterHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Pastikan objek yang masuk adalah karakter (misalnya, tag "Player")
        if (other.CompareTag("Player"))
        {
            // Pilih lokasi acak dari array
            int randomIndex = Random.Range(0, teleportLocations.Length);
            Transform randomTarget = teleportLocations[randomIndex];

            // Teleport karakter ke posisi target acak
            other.transform.position = randomTarget.position;

            // Simpan lokasi ini di playerHealth untuk respawn
            if (playerHealth != null)
            {
                playerHealth.SetSpawnPoint(randomTarget.position, randomTarget.rotation);
            }

            // Opsional: tambahkan efek visual/suara di sini
            Debug.Log("Karakter telah dipindahkan ke target acak: " + randomTarget.name);
        }
    }
}

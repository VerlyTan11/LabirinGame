using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportStart : MonoBehaviour
{
    // Referensi ke objek target
    public Transform targetObject;

    // Event trigger ketika karakter memasuki portal
    private void OnTriggerEnter(Collider other)
    {
        // Pastikan objek yang masuk adalah karakter (misalnya, tag "Player")
        if (other.CompareTag("Player"))
        {
            // Teleport karakter ke posisi target
            other.transform.position = targetObject.position;

            // Opsional: tambahkan efek visual/suara di sini
            Debug.Log("Karakter telah dipindahkan ke target Paradise");
        }
    }
}

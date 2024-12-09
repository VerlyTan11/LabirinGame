using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowButtonWithTeleport : MonoBehaviour
{
    [Header("UI Settings")]
    public GameObject uiObject; // UI untuk tombol

    [Header("Teleport Settings")]
    public Transform teleportTarget; // Objek target teleport

    private bool canTeleport = false; // Flag apakah player berada di trigger

    void Start()
    {
        if (uiObject != null)
        {
            uiObject.SetActive(false); // Sembunyikan UI saat start
        }
        else
        {
            Debug.LogWarning("UI Object belum diset di Inspector.");
        }
    }

    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            if (uiObject != null)
            {
                uiObject.SetActive(true); // Tampilkan UI
            }
            canTeleport = true; // Izinkan teleport
        }
    }

    void OnTriggerExit(Collider player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            if (uiObject != null)
            {
                uiObject.SetActive(false); // Sembunyikan UI saat keluar trigger
            }
            canTeleport = false; // Larang teleport
        }
    }

    public void TeleportPlayer()
    {
        if (canTeleport)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null && teleportTarget != null)
            {
                player.transform.position = teleportTarget.position; // Teleport ke target
                Debug.Log("Player telah dipindahkan ke target teleport.");

                // Sembunyikan UI setelah teleport
                if (uiObject != null)
                {
                    Destroy(uiObject);
                }

                // Hancurkan objek setelah teleport selesai
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("Teleport gagal: Player atau teleportTarget tidak ditemukan.");
            }
        }
    }
}

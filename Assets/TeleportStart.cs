using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportStart : MonoBehaviour
{
    public GameObject Player; // Objek Player
    public Transform target;  // Transform dari target teleportasi

    // Method untuk teleportasi
    public void TeleportPlayer()
    {
        if (Player != null && target != null)
        {
            Player.transform.position = target.position; // Pindahkan Player ke posisi target
        }
        else
        {
            Debug.LogWarning("Player atau Target belum diatur!");
        }
    }
}
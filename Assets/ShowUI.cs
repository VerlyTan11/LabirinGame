using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUI : MonoBehaviour
{
    public GameObject uiObject; // Objek UI
    private bool isUIActive = false; // Cek apakah UI sedang aktif

    void Start()
    {
        uiObject.SetActive(false); // Sembunyikan UI saat start
    }

    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Player" && !isUIActive)
        {
            uiObject.SetActive(true); // Tampilkan UI
            isUIActive = true; // Tandai UI aktif
        }
    }

    void OnTriggerExit(Collider player)
    {
        if (player.gameObject.tag == "Player" && isUIActive)
        {
            uiObject.SetActive(false); // Sembunyikan UI
            isUIActive = false; // Reset status UI
        }
    }

    public void DestroyUI()
    {
        if (uiObject != null)
        {
            Destroy(uiObject); // Hancurkan UI
            isUIActive = false; // Reset status
        }
    }
}
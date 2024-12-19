using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Water : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) // Perbaikan nama metode
    {
        // Periksa tag tanpa else if
        if (!collision.gameObject.CompareTag("Player"))
        {
            return; // Keluar dari metode jika bukan Player
        }

        // Jika tag cocok, pindah ke scene berikutnya
        SceneManager.LoadScene("GameOver");
    }
}

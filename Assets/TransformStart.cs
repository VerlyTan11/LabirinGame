using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TransformStart : MonoBehaviour
{
    public GameObject uiObject; // Objek UI
    private bool isUIActive = false; // Cek apakah UI sedang aktif

    public Vector3 targetPosition = new Vector3(92.1f, 21.5f, 220.5f); // Posisi target

    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Player" && !isUIActive)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(targetPosition, out hit, 5.0f, NavMesh.AllAreas))
            {
                player.gameObject.transform.position = hit.position; // Pindahkan pemain ke posisi yang valid di NavMesh
            }
            else
            {
                player.gameObject.transform.position = targetPosition; // Gunakan posisi default jika NavMesh gagal
            }

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

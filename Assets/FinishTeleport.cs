using UnityEngine;

public class FinishTeleport : MonoBehaviour
{
    public Transform targetObject; // Objek target untuk teleport

    private void OnTriggerEnter(Collider other)
    {
        // Pastikan objek yang masuk adalah pemain
        if (other.CompareTag("Player"))
        {
            // Periksa apakah semua gems telah dikumpulkan
            if (UIManager.Instance.AreAllGemsCollected())
            {
                // Teleport pemain
                other.transform.position = targetObject.position;
                Debug.Log("Semua gems telah dikumpulkan! Pemain dipindahkan.");
            }
            else
            {
                // Tampilkan pesan jika belum semua gems terkumpul
                Debug.Log("Gems belum lengkap. Kumpulkan semua gems untuk melanjutkan.");
            }
        }
    }
}
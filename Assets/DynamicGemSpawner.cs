using System.Collections.Generic;
using UnityEngine;

public class DynamicGemSpawner : MonoBehaviour
{
    public GameObject[] gemPrefabs; // Prefab gems (8 jenis berbeda)
    public Transform labirinBounds; // Parent object labirin untuk batas area
    public Transform[] excludeZones; // Area platform yellow yang harus dihindari
    public int numGemsToSpawn = 8; // Jumlah gems yang ingin di-spawn

    private BoxCollider labirinCollider; // Collider untuk batas labirin

    void Start()
    {
        // Pastikan labirinBounds memiliki BoxCollider
        labirinCollider = labirinBounds.GetComponent<BoxCollider>();
        if (labirinCollider == null)
        {
            Debug.LogError("Labirin Bounds tidak memiliki BoxCollider!");
            return;
        }

        SpawnGemsRandomly();
    }

    void SpawnGemsRandomly()
    {
        int spawnedGems = 0;
        int maxAttempts = 200; // Batas untuk menghindari loop tak berujung
        int attempts = 0;

        while (spawnedGems < numGemsToSpawn && attempts < maxAttempts)
        {
            // Randomkan posisi dalam area labirin
            Vector3 randomPosition = GetRandomPositionWithinBounds();

            // Periksa apakah posisi ini valid
            if (randomPosition != Vector3.zero && IsPositionValid(randomPosition))
            {
                // Pilih prefab gem secara unik
                GameObject gemPrefab = gemPrefabs[spawnedGems % gemPrefabs.Length];

                // Spawn gem di posisi tersebut
                GameObject spawnedGem = Instantiate(gemPrefab, randomPosition, Quaternion.identity);

                // Atur orientasi gems agar tegak terhadap sumbu Y
                spawnedGem.transform.rotation = Quaternion.Euler(0, 0, 0);

                Debug.Log("Spawned gem position: " + spawnedGem.transform.position);
                Debug.Log("Spawned gem rotation: " + spawnedGem.transform.rotation.eulerAngles);

                spawnedGems++;
            }

            attempts++;
        }

        if (spawnedGems < numGemsToSpawn)
        {
            Debug.LogWarning("Tidak cukup posisi valid untuk semua gems!");
        }
    }

    Vector3 GetRandomPositionWithinBounds()
    {
        // Ambil batas area labirin dari collider
        Bounds bounds = labirinCollider.bounds;

        // Randomkan posisi dalam area labirin
        float spawnMargin = 5f; // Margin untuk memperkecil area spawn
        float x = Random.Range(bounds.min.x + spawnMargin, bounds.max.x - spawnMargin);
        float z = Random.Range(bounds.min.z + spawnMargin, bounds.max.z - spawnMargin);

        // Gunakan raycast untuk menentukan tinggi (Y) di atas permukaan labirin
        Vector3 rayStart = new Vector3(x, bounds.max.y + 10f, z); // Mulai dari atas
        Ray ray = new Ray(rayStart, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("LabirinJalur")) // Pastikan hanya spawn di jalur labirin
            {
                return new Vector3(x, hit.point.y + 0.2f, z); // Tambahkan offset agar gems berada di atas permukaan
            }
            else
            {
                return Vector3.zero; // Posisi tidak valid jika bukan jalur
            }
        }
        else
        {
            return Vector3.zero; // Jika raycast tidak menemukan jalur
        }
    }

    bool IsPositionValid(Vector3 position)
    {
        // Periksa apakah posisi berada di excludeZones
        foreach (Transform excludeZone in excludeZones)
        {
            float distance = Vector3.Distance(position, excludeZone.position);
            if (distance < excludeZone.localScale.x / 2f) // Sesuaikan ukuran lingkaran biru
            {
                Debug.Log("Posisi terlalu dekat dengan excludeZone: " + excludeZone.name);
                return false;
            }
        }

        // Pastikan posisi berada dalam area labirinBounds
        if (!labirinCollider.bounds.Contains(position))
        {
            Debug.Log("Posisi di luar area labirinBounds.");
            return false;
        }

        // Pastikan gems tidak berada di luar dinding labirin
        if (Physics.CheckSphere(position, 0.5f, LayerMask.GetMask("Wall")))
        {
            Debug.Log("Posisi terkena dinding labirin.");
            return false;
        }

        return true;
    }
}
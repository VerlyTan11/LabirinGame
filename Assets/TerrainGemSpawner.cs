using System.Collections.Generic;
using UnityEngine;

public class TerrainGemSpawner : MonoBehaviour
{
    public GameObject[] gemPrefabs; // Array untuk prefab gems
    public Terrain terrain; // Referensi ke Terrain
    public Transform[] excludeZones; // Area platform yellow yang harus dihindari
    public int numGemsToSpawn = 8; // Jumlah gems yang ingin di-spawn
    public float hoverHeight = 1.5f; // Ketinggian melayang di atas terrain
    public float floatSpeed = 2.0f; // Kecepatan gerakan naik turun
    public float floatAmplitude = 0.5f; // Amplitudo gerakan naik turun

    void Start()
    {
        SpawnGemsOnTerrain();
    }

    void SpawnGemsOnTerrain()
    {
        int spawnedGems = 0;
        int maxAttempts = 100; // Batas percobaan untuk mencegah loop tak berujung
        int attempts = 0;

        while (spawnedGems < numGemsToSpawn && attempts < maxAttempts)
        {
            // Dapatkan posisi random dalam Terrain
            Vector3 randomPosition = GetRandomPositionOnTerrain(out Vector3 surfaceNormal);

            // Periksa apakah posisi ini valid
            if (IsPositionValid(randomPosition, surfaceNormal))
            {
                // Pilih prefab gem secara acak
                GameObject gemPrefab = gemPrefabs[spawnedGems % gemPrefabs.Length];

                // Spawn gem di posisi random yang valid
                GameObject spawnedGem = Instantiate(gemPrefab, randomPosition, gemPrefab.transform.rotation);

                // Tambahkan script animasi naik turun ke gems
                FloatingGem floatingGem = spawnedGem.AddComponent<FloatingGem>();
                floatingGem.floatSpeed = floatSpeed;
                floatingGem.floatAmplitude = floatAmplitude;

                // Debugging
                Debug.Log($"Spawned {spawnedGem.name} at {randomPosition} with FloatingGem script.");

                // Pastikan Rigidbody tidak mengganggu
                Rigidbody rb = spawnedGem.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                    rb.useGravity = false;
                    Debug.Log($"Adjusted Rigidbody for {spawnedGem.name}.");
                }

                spawnedGems++;
            }

            attempts++;
        }

        if (spawnedGems < numGemsToSpawn)
        {
            Debug.LogWarning("Tidak cukup posisi valid untuk semua gems!");
        }
    }

    Vector3 GetRandomPositionOnTerrain(out Vector3 surfaceNormal)
    {
        // Ambil dimensi Terrain
        float terrainWidth = terrain.terrainData.size.x;
        float terrainLength = terrain.terrainData.size.z;
        float terrainPosX = terrain.transform.position.x;
        float terrainPosZ = terrain.transform.position.z;

        // Batas untuk 3/4 area terrain
        float minX = terrainPosX + terrainWidth * 0.125f; // Mulai dari 1/8 panjang terrain
        float maxX = terrainPosX + terrainWidth * 0.875f; // Berakhir di 7/8 panjang terrain
        float minZ = terrainPosZ + terrainLength * 0.125f; // Mulai dari 1/8 lebar terrain
        float maxZ = terrainPosZ + terrainLength * 0.875f; // Berakhir di 7/8 lebar terrain

        // Randomkan posisi X dan Z dalam batas 3/4 area
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);

        // Gunakan Raycast untuk mendapatkan tinggi Terrain di titik tersebut
        Vector3 checkPosition = new Vector3(randomX, terrain.terrainData.size.y + terrain.transform.position.y, randomZ);
        Ray ray = new Ray(checkPosition, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            surfaceNormal = hit.normal; // Normal permukaan terrain
            Debug.Log($"Valid terrain hit at {hit.point} with normal {surfaceNormal}.");
            return new Vector3(randomX, hit.point.y + hoverHeight, randomZ);
        }

        surfaceNormal = Vector3.up; // Default jika tidak ada hit
        Debug.LogWarning("No terrain hit detected. Defaulting to flat surface.");
        return new Vector3(randomX, terrain.SampleHeight(new Vector3(randomX, 0, randomZ)) + terrain.transform.position.y + hoverHeight, randomZ);
    }

    bool IsPositionValid(Vector3 position, Vector3 surfaceNormal)
    {
        // Pastikan posisi tidak berada di excludeZones
        foreach (Transform excludeZone in excludeZones)
        {
            float distance = Vector3.Distance(position, excludeZone.position);
            if (distance < excludeZone.localScale.x / 2f) // Periksa jarak dari lingkaran biru
            {
                Debug.Log($"Position {position} is too close to excludeZone {excludeZone.name}.");
                return false;
            }
        }

        // Pastikan posisi tidak berada di dinding (normal terlalu curam)
        if (surfaceNormal.y < 0.7f) // Nilai normal y rendah berarti permukaan miring/vertikal
        {
            Debug.Log($"Position {position} is too steep with normal {surfaceNormal}.");
            return false;
        }

        return true; // Posisi valid
    }
}
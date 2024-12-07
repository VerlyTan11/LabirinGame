using System.Collections.Generic;
using UnityEngine;

public class TerrainGemSpawner : MonoBehaviour
{
    public GameObject[] gemPrefabs; // Array untuk prefab gems
    public Terrain terrain; // Referensi ke Terrain
    public Transform[] excludeZones; // Area platform yellow yang harus dihindari
    public int numGemsToSpawn = 8; // Jumlah gems yang ingin di-spawn

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
            Vector3 randomPosition = GetRandomPositionOnTerrain();

            // Periksa apakah posisi ini valid
            if (IsPositionValid(randomPosition))
            {
                // Pilih prefab gem secara acak
                GameObject gemPrefab = gemPrefabs[spawnedGems % gemPrefabs.Length];

                // Spawn gem di posisi random yang valid
                Instantiate(gemPrefab, randomPosition, Quaternion.identity);

                spawnedGems++;
            }

            attempts++;
        }

        if (spawnedGems < numGemsToSpawn)
        {
            Debug.LogWarning("Tidak cukup posisi valid untuk semua gems!");
        }
    }

    Vector3 GetRandomPositionOnTerrain()
    {
        // Ambil dimensi Terrain
        float terrainWidth = terrain.terrainData.size.x;
        float terrainLength = terrain.terrainData.size.z;
        float terrainPosX = terrain.transform.position.x;
        float terrainPosZ = terrain.transform.position.z;

        // Randomkan posisi X dan Z
        float randomX = Random.Range(terrainPosX, terrainPosX + terrainWidth);
        float randomZ = Random.Range(terrainPosZ, terrainPosZ + terrainLength);

        // Dapatkan tinggi Terrain di titik tersebut
        float terrainHeight = terrain.SampleHeight(new Vector3(randomX, 0, randomZ)) + terrain.transform.position.y;

        return new Vector3(randomX, terrainHeight, randomZ);
    }

    bool IsPositionValid(Vector3 position)
    {
        // Pastikan posisi tidak berada di excludeZones
        foreach (Transform excludeZone in excludeZones)
        {
            float distance = Vector3.Distance(position, excludeZone.position);
            if (distance < excludeZone.localScale.x / 2f) // Periksa jarak dari lingkaran biru
            {
                return false;
            }
        }

        // Pastikan posisi tidak berada di dinding atau area lain yang tidak valid
        return true;
    }
}
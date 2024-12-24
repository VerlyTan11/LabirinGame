using System.Collections.Generic;
using UnityEngine;

public class TerrainMonsterSpawner : MonoBehaviour
{
    public GameObject[] monsterPrefabs; // Array untuk prefab monster
    public Terrain terrain; // Referensi ke Terrain
    public Transform[] excludeZones; // Area platform yang harus dihindari
    public int numMonstersToSpawn = 50; // Jumlah monster yang ingin di-spawn
    public float hoverHeight = 0.0f; // Ketinggian spawn di atas terrain

    void Start()
    {
        SpawnMonstersOnTerrain();
    }

    void SpawnMonstersOnTerrain()
    {
        int spawnedMonsters = 0;
        int maxAttempts = 100; // Batas percobaan untuk mencegah loop tak berujung
        int attempts = 0;

        while (spawnedMonsters < numMonstersToSpawn && attempts < maxAttempts)
        {
            // Dapatkan posisi random dalam Terrain
            Vector3 randomPosition = GetRandomPositionOnTerrain(out Vector3 surfaceNormal);

            // Periksa apakah posisi ini valid
            if (IsPositionValid(randomPosition, surfaceNormal))
            {
                // Pilih prefab monster secara acak
                GameObject monsterPrefab = monsterPrefabs[spawnedMonsters % monsterPrefabs.Length];

                // Spawn monster di posisi random yang valid
                GameObject spawnedMonster = Instantiate(monsterPrefab, randomPosition, monsterPrefab.transform.rotation);

                // Debugging
                Debug.Log($"Spawned {spawnedMonster.name} at {randomPosition}.");

                spawnedMonsters++;
            }

            attempts++;
        }

        if (spawnedMonsters < numMonstersToSpawn)
        {
            Debug.LogWarning("Tidak cukup posisi valid untuk semua monster!");
        }
    }

    Vector3 GetRandomPositionOnTerrain(out Vector3 surfaceNormal)
    {
        float terrainWidth = terrain.terrainData.size.x;
        float terrainLength = terrain.terrainData.size.z;
        float terrainPosX = terrain.transform.position.x;
        float terrainPosZ = terrain.transform.position.z;

        float minX = terrainPosX + terrainWidth * 0.125f;
        float maxX = terrainPosX + terrainWidth * 0.875f;
        float minZ = terrainPosZ + terrainLength * 0.125f;
        float maxZ = terrainPosZ + terrainLength * 0.875f;

        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);

        Vector3 checkPosition = new Vector3(randomX, terrain.terrainData.size.y + terrain.transform.position.y, randomZ);
        Ray ray = new Ray(checkPosition, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            surfaceNormal = hit.normal;
            return new Vector3(randomX, hit.point.y + hoverHeight, randomZ);
        }

        surfaceNormal = Vector3.up;
        return new Vector3(randomX, terrain.SampleHeight(new Vector3(randomX, 0, randomZ)) + terrain.transform.position.y + hoverHeight, randomZ);
    }

    bool IsPositionValid(Vector3 position, Vector3 surfaceNormal)
    {
        foreach (Transform excludeZone in excludeZones)
        {
            float distance = Vector3.Distance(position, excludeZone.position);
            if (distance < excludeZone.localScale.x / 2f)
            {
                Debug.Log($"Position {position} is too close to excludeZone {excludeZone.name}.");
                return false;
            }
        }

        if (surfaceNormal.y < 0.7f)
        {
            Debug.Log($"Position {position} is too steep with normal {surfaceNormal}.");
            return false;
        }

        return true;
    }
}

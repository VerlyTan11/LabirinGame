using System.Collections.Generic;
using UnityEngine;

public class TerrainGemSpawner : MonoBehaviour
{
    public GameObject[] gemPrefabs;
    public Terrain terrain;
    public Transform[] excludeZones;
    public Collider validSpawnArea; // Zona spawn valid
    public int numGemsToSpawn = 8;
    public float hoverHeight = 2.5f;
    public float floatSpeed = 2.0f;
    public float floatAmplitude = 0.5f;
    public float maxTerrainHeight = 20f; // Batas tinggi maksimum yang diizinkan

    void Start()
    {
        SpawnGemsOnTerrain();
    }

    void SpawnGemsOnTerrain()
    {
        int spawnedGems = 0;
        int maxAttempts = 100;
        int attempts = 0;

        while (spawnedGems < numGemsToSpawn && attempts < maxAttempts)
        {
            Vector3 randomPosition = GetRandomPositionOnTerrain(out Vector3 surfaceNormal);

            if (IsPositionValid(randomPosition, surfaceNormal))
            {
                GameObject gemPrefab = gemPrefabs[spawnedGems % gemPrefabs.Length];
                GameObject spawnedGem = Instantiate(gemPrefab, randomPosition, gemPrefab.transform.rotation);

                FloatingGem floatingGem = spawnedGem.AddComponent<FloatingGem>();
                floatingGem.floatSpeed = floatSpeed;
                floatingGem.floatAmplitude = floatAmplitude;

                Rigidbody rb = spawnedGem.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                    rb.useGravity = false;
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
        float terrainWidth = terrain.terrainData.size.x;
        float terrainLength = terrain.terrainData.size.z;
        float terrainPosX = terrain.transform.position.x;
        float terrainPosZ = terrain.transform.position.z;

        float minX = terrainPosX + terrainWidth * 0.2f;
        float maxX = terrainPosX + terrainWidth * 0.8f;
        float minZ = terrainPosZ + terrainLength * 0.2f;
        float maxZ = terrainPosZ + terrainLength * 0.8f;

        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);

        Vector3 checkPosition = new Vector3(randomX, terrain.terrainData.size.y + terrain.transform.position.y, randomZ);
        Ray ray = new Ray(checkPosition, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.CompareTag("Ground")) // Pastikan hanya terrain
            {
                surfaceNormal = hit.normal;
                return new Vector3(randomX, hit.point.y + hoverHeight, randomZ);
            }
        }

        surfaceNormal = Vector3.up;
        return new Vector3(randomX, terrain.SampleHeight(new Vector3(randomX, 0, randomZ)) + terrain.transform.position.y + hoverHeight, randomZ);
    }

    bool IsPositionValid(Vector3 position, Vector3 surfaceNormal)
    {
        // Periksa zona spawn valid
        if (validSpawnArea != null && !validSpawnArea.bounds.Contains(position))
        {
            Debug.Log($"Position {position} is outside the valid spawn area.");
            return false;
        }

        // Periksa tinggi maksimum
        if (position.y > maxTerrainHeight)
        {
            Debug.Log($"Position {position} is too high.");
            return false;
        }

        // Periksa sudut kemiringan
        if (Vector3.Angle(Vector3.up, surfaceNormal) > 30f)
        {
            Debug.Log($"Position {position} is too steep with angle {Vector3.Angle(Vector3.up, surfaceNormal)}.");
            return false;
        }

        return true;
    }
}
using System.Collections.Generic;
using UnityEngine;

public class TerrainMonsterSpawner : MonoBehaviour
{
    public GameObject[] monsterPrefabs; // Array untuk prefab monster
    public Terrain terrain; // Referensi ke Terrain
    public Transform[] excludeZones; // Area platform yang harus dihindari
    public int numMonstersToSpawn = 50; // Jumlah monster yang ingin di-spawn
    public float hoverHeight = -0.1f; // Ketinggian spawn di atas terrain

//     void Start()
//     {
//         SpawnMonstersOnTerrain();
//     }

//     void SpawnMonstersOnTerrain()
//     {
//         int spawnedMonsters = 0;
//         int maxAttempts = 100; // Batas percobaan untuk mencegah loop tak berujung
//         int attempts = 0;

//         while (spawnedMonsters < numMonstersToSpawn && attempts < maxAttempts)
//         {
//             // Dapatkan posisi random dalam Terrain
//             Vector3 randomPosition = GetRandomPositionOnTerrain(out Vector3 surfaceNormal);

//             // Periksa apakah posisi ini valid
//             if (IsPositionValid(randomPosition, surfaceNormal))
//             {
//                 // Pilih prefab monster secara acak
//                 GameObject monsterPrefab = monsterPrefabs[spawnedMonsters % monsterPrefabs.Length];

//                 // Spawn monster di posisi random yang valid
//                 GameObject spawnedMonster = Instantiate(monsterPrefab, randomPosition, monsterPrefab.transform.rotation);

//                 // Debugging
//                 Debug.Log($"Spawned {spawnedMonster.name} at {randomPosition}.");

//                 spawnedMonsters++;
//             }

//             attempts++;
//         }

//         if (spawnedMonsters < numMonstersToSpawn)
//         {
//             Debug.LogWarning("Tidak cukup posisi valid untuk semua monster!");
//         }
//     }

//     Vector3 GetRandomPositionOnTerrain(out Vector3 surfaceNormal)
//     {
//         float terrainWidth = terrain.terrainData.size.x;
//         float terrainLength = terrain.terrainData.size.z;
//         float terrainPosX = terrain.transform.position.x;
//         float terrainPosZ = terrain.transform.position.z;

//         float minX = terrainPosX + terrainWidth * 0.125f;
//         float maxX = terrainPosX + terrainWidth * 0.875f;
//         float minZ = terrainPosZ + terrainLength * 0.125f;
//         float maxZ = terrainPosZ + terrainLength * 0.875f;

//         float randomX = Random.Range(minX, maxX);
//         float randomZ = Random.Range(minZ, maxZ);

//         Vector3 checkPosition = new Vector3(randomX, terrain.terrainData.size.y + terrain.transform.position.y, randomZ);
//         Ray ray = new Ray(checkPosition, Vector3.down);
//         if (Physics.Raycast(ray, out RaycastHit hit))
//         {
//             surfaceNormal = hit.normal;
//             return new Vector3(randomX, hit.point.y + hoverHeight, randomZ);
//         }

//         surfaceNormal = Vector3.up;
//         return new Vector3(randomX, terrain.SampleHeight(new Vector3(randomX, 0, randomZ)) + terrain.transform.position.y + hoverHeight, randomZ);
//     }

//     bool IsPositionValid(Vector3 position, Vector3 surfaceNormal)
//     {
//         foreach (Transform excludeZone in excludeZones)
//         {
//             float distance = Vector3.Distance(position, excludeZone.position);
//             if (distance < excludeZone.localScale.x / 2f)
//             {
//                 Debug.Log($"Position {position} is too close to excludeZone {excludeZone.name}.");
//                 return false;
//             }
//         }

//         if (surfaceNormal.y < 0.7f)
//         {
//             Debug.Log($"Position {position} is too steep with normal {surfaceNormal}.");
//             return false;
//         }

//         return true;
//     }
// }
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
            // Dapatkan posisi random di Terrain
            Vector3 randomPosition = GetRandomPositionAboveMaze(out Vector3 surfaceNormal);

            // Periksa apakah posisi ini valid
            if (IsPositionValid(randomPosition, surfaceNormal))
            {
                // Pilih prefab monster secara acak
                GameObject monsterPrefab = monsterPrefabs[spawnedMonsters % monsterPrefabs.Length];

                // Spawn monster di posisi random yang valid
                GameObject spawnedMonster = Instantiate(monsterPrefab, randomPosition, monsterPrefab.transform.rotation);

                // Tambahkan Rigidbody untuk efek gravitasi
                Rigidbody rb = spawnedMonster.AddComponent<Rigidbody>(); // Menambahkan Rigidbody
                rb.useGravity = true; // Pastikan gravitasi aktif
                rb.constraints = RigidbodyConstraints.FreezeRotation; // Mencegah rotasi tidak diinginkan

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

    Vector3 GetRandomPositionAboveMaze(out Vector3 surfaceNormal)
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

        float fixedY = 4.983995f - 5f; // Set fixed Y position here

        // Raycast dari atas ke bawah untuk mendeteksi tanah (tag "Ground")
        Vector3 startPosition = new Vector3(randomX, terrain.transform.position.y + 200f, randomZ);
        Ray ray = new Ray(startPosition, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground"))) // Menggunakan Layer "Ground"
        {
            surfaceNormal = hit.normal;

            // Ensure the monster is placed at the fixed Y position (4.983995)
            return new Vector3(hit.point.x, fixedY + hoverHeight, hit.point.z);
        }

        surfaceNormal = Vector3.up;
        return new Vector3(randomX, fixedY + hoverHeight, randomZ); // Default position with fixed Y
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 2f); // Untuk melihat area spawn di Scene View
    }
}

public class Monster : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionStay(Collision collision)
    {
        // Jika monster bertabrakan dengan Ground (tanah)
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Menangkap titik kontak tabrakan
            foreach (ContactPoint contact in collision.contacts)
            {
                // Pastikan monster tetap di atas tanah (di atas titik kontak)
                Vector3 currentPosition = transform.position;

                // Mengatur posisi monster agar berada sedikit di atas permukaan tanah
                transform.position = new Vector3(currentPosition.x, contact.point.y + 0.1f, currentPosition.z);
            }
        }
    }
}
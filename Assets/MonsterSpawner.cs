using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab; // Prefab monster
    public int numberOfMonsters = 20; // Jumlah monster yang akan di-generate
    public Vector3 spawnAreaCenter; // Pusat area spawn
    public Vector3 spawnAreaSize; // Ukuran area spawn

    void Start()
    {
        SpawnMonsters();
    }

    void SpawnMonsters()
    {
        for (int i = 0; i < numberOfMonsters; i++)
        {
            // Hitung posisi spawn acak dalam area
            Vector3 spawnPosition = GetRandomPosition();

            // Instantiate monster di posisi acak
            Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
        }
    }

    Vector3 GetRandomPosition()
    {
        // Hitung posisi acak dalam area yang ditentukan
        float randomX = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
        float randomY = Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2);
        float randomZ = Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2);

        return spawnAreaCenter + new Vector3(randomX, randomY, randomZ);
    }

    private void OnDrawGizmosSelected()
    {
        // Gambar area spawn di editor
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(spawnAreaCenter, spawnAreaSize);
    }
}

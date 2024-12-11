using UnityEngine;

public class FloatingGem : MonoBehaviour
{
    public float floatSpeed = 2.0f; // Kecepatan naik turun
    public float floatAmplitude = 0.5f; // Amplitudo gerakan naik turun
    public string gemType; // Jenis gems, misalnya "Heart", "Star"

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position; // Simpan posisi awal
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.CollectGem(gemType); // Laporkan ke UIManager
            Destroy(gameObject); // Hancurkan gems
        }
    }
}
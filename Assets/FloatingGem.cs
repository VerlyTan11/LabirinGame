using UnityEngine;

public class FloatingGem : MonoBehaviour
{
    public float floatSpeed = 2.0f; // Kecepatan naik turun
    public float floatAmplitude = 0.5f; // Amplitudo gerakan naik turun

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position; // Simpan posisi awal
        Debug.Log($"FloatingGem initialized for {gameObject.name}: floatSpeed={floatSpeed}, floatAmplitude={floatAmplitude}");
    }

    void Update()
    {
        // Hitung posisi baru untuk naik turun
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
        Debug.Log($"Gem {gameObject.name} position updated to {transform.position}");
    }
}
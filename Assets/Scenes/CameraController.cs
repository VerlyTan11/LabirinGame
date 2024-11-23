using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject hero;
    public float defaultDistance = 5f; // Jarak default kamera ke hero
    public float minDistance = 2f; // Jarak minimum kamera
    public float maxDistance = 10f; // Jarak maksimum kamera
    public float zoomSpeed = 2f; // Kecepatan zoom
    public float rotationSpeed = 100f; // Kecepatan rotasi kamera
    private float mouseX = 0f; // Posisi rotasi horizontal
    private float mouseY = 0f; // Posisi rotasi vertikal
    private float currentDistance; // Jarak kamera saat ini

    void Start()
    {
        // Temukan hero berdasarkan tag "Player"
        hero = GameObject.FindGameObjectWithTag("Player");

        if (hero == null)
        {
            Debug.LogError("Hero tidak ditemukan! Pastikan objek memiliki tag 'Player'.");
            return;
        }

        // Set jarak kamera awal
        currentDistance = defaultDistance;

        // Set rotasi kamera awal
        transform.rotation = Quaternion.Euler(20f, 0f, 0f);

        // Set posisi awal kamera
        UpdateCameraPosition();
    }

    void Update()
    {
        HandleMouseRotation();
        HandleZoom();
        UpdateCameraPosition();
    }

    void HandleMouseRotation()
    {
        // Rotasi kamera berdasarkan pergerakan mouse
        float mouseDeltaX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseDeltaY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        mouseX += mouseDeltaX;
        mouseY -= mouseDeltaY;

        // Batasi rotasi vertikal agar kamera tidak berputar terbalik
        mouseY = Mathf.Clamp(mouseY, -30f, 60f); // Sesuaikan batas sesuai kebutuhan
    }

    void HandleZoom()
    {
        // Zoom menggunakan tombol angka 1 (zoom in) dan 2 (zoom out)
        if (Input.GetKey(KeyCode.Alpha1))
        {
            currentDistance -= zoomSpeed * Time.deltaTime; // Kurangi jarak untuk zoom in
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            currentDistance += zoomSpeed * Time.deltaTime; // Tambah jarak untuk zoom out
        }

        // Batasi jarak zoom agar tetap dalam rentang min dan max
        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);
    }

    void UpdateCameraPosition()
    {
        // Rotasi kamera berdasarkan input mouse
        Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0);

        // Tentukan arah kamera
        Vector3 direction = rotation * Vector3.back * currentDistance;

        // Tambahkan offset untuk posisi kamera
        Vector3 offset = new Vector3(0, 2, 0); // Offset vertikal (misalnya untuk mengikuti bagian atas hero)

        // Hitung posisi target kamera
        Vector3 targetPosition = hero.transform.position + direction + offset;

        // Pastikan kamera tidak turun ke bawah permukaan
        if (targetPosition.y < 0)
        {
            targetPosition.y = 0;
        }

        // Set posisi kamera ke target
        transform.position = targetPosition;

        // Kamera selalu menghadap hero
        transform.LookAt(hero.transform.position + new Vector3(0, 1, 0)); // Tambahkan offset agar lebih ke tengah hero
    }
}

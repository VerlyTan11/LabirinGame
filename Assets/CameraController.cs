using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float distanceFromPlayer = 5f; // Jarak kamera dari pemain
    public float heightAboveGround = 2f;  // Tinggi kamera di atas pemain
    public LayerMask groundLayer;         // Layer untuk tanah
    public float mouseSensitivity = 2f;   // Sensitivitas rotasi kamera dengan mouse

    private Transform player;             // Transform pemain yang ditandai dengan tag "Player"
    private float currentYaw = 0f;        // Rotasi horizontal kamera
    private float currentPitch = 0f;      // Rotasi vertikal kamera
    private float minPitch = -30f;        // Batas minimal rotasi vertikal
    private float maxPitch = 60f;         // Batas maksimal rotasi vertikal

    void Start()
    {
        // Mencari objek dengan tag "Player" untuk mendapatkan transformnya
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found. Make sure your player object has the 'Player' tag.");
        }

        // Menyembunyikan kursor untuk pengalaman bermain yang lebih baik
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (player == null) return;  // Keluar jika tidak ada objek player

        // Mengambil input mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Mengupdate rotasi berdasarkan input mouse
        currentYaw += mouseX;
        currentPitch -= mouseY;
        currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch); // Membatasi rotasi vertikal

        // Menghitung posisi kamera di belakang dan di atas pemain
        Vector3 offset = new Vector3(0, heightAboveGround, -distanceFromPlayer);
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);
        Vector3 desiredPosition = player.position + rotation * offset;

        // Raycast ke bawah dari posisi yang diinginkan untuk mengecek tanah
        if (Physics.Raycast(desiredPosition, Vector3.down, out RaycastHit hit, heightAboveGround, groundLayer))
        {
            // Sesuaikan posisi kamera agar tidak menembus tanah
            desiredPosition.y = hit.point.y + heightAboveGround;
        }

        // Menetapkan posisi kamera
        transform.position = desiredPosition;

        // Mengatur kamera agar menghadap ke arah pemain
        transform.LookAt(player.position + Vector3.up * heightAboveGround);
    }
}

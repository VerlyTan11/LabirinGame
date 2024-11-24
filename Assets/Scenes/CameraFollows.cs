using UnityEngine;

public class CameraFollows : MonoBehaviour
{
    public Transform target; // Target yang akan diikuti oleh kamera (Lady Pirate)
    public Vector3 offset; // Jarak antara kamera dan target
    public float rotateSpeed = 5f; // Kecepatan rotasi kamera berdasarkan input mouse

    private float currentAngle; // Untuk menyimpan sudut rotasi saat ini

    void LateUpdate()
    {
        // Mengambil input dari mouse untuk rotasi kamera
        float horizontalInput = Input.GetAxis("Mouse X") * rotateSpeed;
        currentAngle += horizontalInput; // Menambah sudut rotasi berdasarkan input mouse

        // Rotasi kamera mengitari target
        Quaternion rotation = Quaternion.Euler(0, currentAngle, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        // Update posisi kamera
        transform.position = desiredPosition;

        // Kamera selalu menghadap target
        transform.LookAt(target);
    }
}

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PlayerScript : MonoBehaviour
{
    private Animator animator;       // Komponen Animator
    private Rigidbody rb;            // Komponen Rigidbody
    private Collider col;            // Komponen Collider

    public float moveSpeed = 10f;     // Kecepatan berjalan
    public float runMultiplier = 2f; // Faktor pengali untuk lari
    public float jumpForce = 5f;     // Kekuatan lompat
    public float rotationSpeed = 100f; // Kecepatan rotasi kamera
    public float extraGravity = 10f; // Gaya gravitasi tambahan

    private float rotationX = 0f;
    public float minimumY = -60f;    // Batas bawah rotasi kamera vertikal
    public float maximumY = 60f;     // Batas atas rotasi kamera vertikal

    private bool isJumping = false;  // Mengecek apakah sedang lompat


    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        rb.freezeRotation = true; // Membekukan rotasi agar tidak terpengaruh fisika
    }

    void FixedUpdate()
    {
        if (isJumping)
        {
            rb.AddForce(Vector3.down * extraGravity, ForceMode.Acceleration);
        }
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleCameraRotation();
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal") * moveSpeed;
        float vertical = Input.GetAxis("Vertical") * moveSpeed;

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        if (isRunning)
        {
            horizontal *= runMultiplier;
            vertical *= runMultiplier;
        }

        Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical;
        Vector3 newPosition = rb.position + moveDirection * Time.deltaTime;

        // Gunakan Rigidbody.MovePosition untuk memastikan pergerakan mematuhi sistem fisika
        rb.MovePosition(newPosition);

        // Atur animasi berjalan/lari
        if (moveDirection.magnitude > 0)
        {
            animator.SetBool("isWalking", !isRunning);
            animator.SetBool("isRunning", isRunning);
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetBool("isJumping", true);
            isJumping = true;
        }
    }

    private void HandleCameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, minimumY, maximumY);

        transform.Rotate(0, mouseX, 0);

        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            mainCamera.transform.localEulerAngles = new Vector3(rotationX, 0, 0);
        }
    }
void OnFootstep()
    {
        // Fungsi tambahan untuk suara langkah
        // Implementasi dapat ditambahkan sesuai kebutuhan
    }
    private void OnCollisionEnter(Collision collision)
{
    // Reset status lompat saat menyentuh tanah
    if (collision.gameObject.CompareTag("Ground"))
    {
        // Menambahkan sedikit penanganan fisika, misalnya mengatur posisi player agar tidak menembus tanah
        Vector3 collisionNormal = collision.contacts[0].normal; 
        if (collisionNormal.y > 0.5f) // pastikan kita menyentuh permukaan datar
        {
            animator.SetBool("isJumping", false);
            isJumping = false;
        }
    }

    if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isJumping", false);
            isJumping = false;
        }
}
}

// using UnityEngine;

// [RequireComponent(typeof(Rigidbody))]
// public class PlayerScript : MonoBehaviour
// {
//     private Animator animator;      // Komponen Animator
//     private Rigidbody rb;           // Komponen Rigidbody

//     public float moveSpeed = 5f;    // Kecepatan berjalan
//     public float runMultiplier = 2f; // Faktor pengali untuk lari
//     public float jumpForce = 5f;    // Kekuatan lompat
//     public float rotationSpeed = 100f; // Kecepatan rotasi kamera

//     public float minimumY = -60f; // Batas bawah rotasi kamera vertikal
//     public float maximumY = 60f;  // Batas atas rotasi kamera vertikal
//     private float rotationX = 0f;

//     private bool isJumping = false; // Mengecek apakah sedang lompat

//     void Awake()
//     {
//         // Ambil komponen Animator dan Rigidbody
//         animator = GetComponent<Animator>();
//         rb = GetComponent<Rigidbody>();
//         rb.freezeRotation = true; // Membekukan rotasi agar tidak terpengaruh fisika
//     }

//     void Update()
//     {
//         HandleMovement();
//         HandleJump();
//         HandleCameraRotation();
//     }

//     private void HandleMovement()
//     {
//         // Ambil input pergerakan
//         float translation = Input.GetAxis("Vertical") * moveSpeed;
//         float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

//         // Cek jika tombol lari (Shift kiri) ditekan
//         bool isRunning = Input.GetKey(KeyCode.LeftShift);
//         if (isRunning)
//         {
//             translation *= runMultiplier;
//         }

//         // Normalisasi kecepatan berdasarkan waktu
//         translation *= Time.deltaTime;
//         rotation *= Time.deltaTime;

//         // Gerakkan karakter maju/mundur
//         transform.Translate(0, 0, translation);
//         // Putar karakter
//         transform.Rotate(0, rotation, 0);

//         // Atur animasi berdasarkan status berjalan atau lari
//         if (translation != 0)
//         {
//             animator.SetBool("isWalking", !isRunning);
//             animator.SetBool("isRunning", isRunning);
//         }
//         else
//         {
//             animator.SetBool("isWalking", false);
//             animator.SetBool("isRunning", false);
//         }
//     }

//     private void HandleJump()
//     {
//         // Lompat jika tombol Space ditekan dan belum lompat
//         if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
//         {
//             rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
//             animator.SetBool("isJumping", true);
//             isJumping = true;
//         }
//     }

//     private void HandleCameraRotation()
//     {
//         // Rotasi kamera berdasarkan gerakan mouse
//         float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
//         float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

//         rotationX -= mouseY;
//         rotationX = Mathf.Clamp(rotationX, minimumY, maximumY);

//         // Rotasi pemain pada sumbu Y
//         transform.Rotate(0, mouseX, 0);

//         // Rotasi kamera pada sumbu X
//         Camera mainCamera = Camera.main;
//         if (mainCamera != null)
//         {
//             mainCamera.transform.localEulerAngles = new Vector3(rotationX, 0, 0);
//         }
//     }

//     private void OnCollisionEnter(Collision collision)
//     {
//         // Reset status lompat saat menyentuh tanah
//         if (collision.gameObject.CompareTag("Ground"))
//         {
//             animator.SetBool("isJumping", false);
//             isJumping = false;
//         }
//     }

//     void OnFootstep()
//     {
//         // Fungsi tambahan untuk suara langkah
//         // Implementasi dapat ditambahkan sesuai kebutuhan
//     }
// }



using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerScript : MonoBehaviour
{
    private Animator animator;      // Komponen Animator
    private Rigidbody rb;           // Komponen Rigidbody

    public float moveSpeed = 20f;    // Kecepatan berjalan
    public float runMultiplier = 1f; // Faktor pengali untuk lari
    public float jumpForce = 30f;    // Kekuatan lompat
    public float extraGravity = 10f; // Gaya gravitasi tambahan
    public float rotationSpeed = 100f; // Kecepatan rotasi kamera

    public float minimumY = -60f; // Batas bawah rotasi kamera vertikal
    public float maximumY = 60f;  // Batas atas rotasi kamera vertikal
    private float rotationX = 0f;

    private bool isJumping = false; // Mengecek apakah sedang lompat

   void Awake()
    {
        // Ambil komponen Animator dan Rigidbody
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Membekukan rotasi agar tidak terpengaruh fisika
    }

    void FixedUpdate()
    {
        // Tambahkan gravitasi ekstra untuk membuat jatuh lebih cepat
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
        // Ambil input pergerakan
        float vertical = Input.GetAxis("Vertical") * moveSpeed; // Maju/mundur
        float horizontal = Input.GetAxis("Horizontal") * moveSpeed; // Kiri/kanan

        // Cek jika tombol lari (Shift kiri) ditekan
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        if (isRunning)
        {
            vertical *= runMultiplier;
            horizontal *= runMultiplier;
        }

        // Normalisasi kecepatan berdasarkan waktu
        vertical *= Time.deltaTime;
        horizontal *= Time.deltaTime;

        // Gerakkan karakter secara horizontal (kiri/kanan) dan vertikal (maju/mundur)
        transform.Translate(horizontal, 0, vertical);

        // Atur animasi berdasarkan status berjalan atau lari
        if (vertical != 0 || horizontal != 0)
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
        // Lompat jika tombol Space ditekan dan belum lompat
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            // Tambahkan gaya lompat
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            
            // Atur animator dan status lompat
            animator.SetBool("isJumping", true);
            isJumping = true;
        }
    }

    private void HandleCameraRotation()
    {
        // Rotasi kamera berdasarkan gerakan mouse
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, minimumY, maximumY);

        // Rotasi pemain pada sumbu Y
        transform.Rotate(0, mouseX, 0);

        // Rotasi kamera pada sumbu X
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            mainCamera.transform.localEulerAngles = new Vector3(rotationX, 0, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Reset status lompat saat menyentuh tanah
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isJumping", false);
            isJumping = false;
        }
    }

    void OnFootstep()
    {
        // Fungsi tambahan untuk suara langkah
        // Implementasi dapat ditambahkan sesuai kebutuhan
    }
}
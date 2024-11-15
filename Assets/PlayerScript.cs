using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Animator animator;
    private bool isRunning;
    private bool isJumping;
    public float walkSpeed = 2f;   // Kecepatan berjalan
    public float runSpeed = 5f;    // Kecepatan berlari
    public float jumpForce = 5f;   // Kecepatan lompat
    private Rigidbody rb;          // Rigidbody untuk fisika
    private float currentRotationY = 0f;  // Menyimpan rotasi saat ini pada sumbu Y

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Menggunakan Input Axis untuk gerakan yang lebih smooth
        float horizontal = Input.GetAxisRaw("Horizontal"); // A dan D
        float vertical = Input.GetAxisRaw("Vertical");     // W dan S

        bool shiftPressed = Input.GetKey(KeyCode.LeftShift);
        isJumping = Input.GetKeyDown(KeyCode.Space); // Menangani lompat dengan tombol Space
        float speed = isRunning ? runSpeed : walkSpeed;

        // Membuat vector untuk gerakan
        Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized;

        // Mengecek apakah karakter bergerak
        bool isMoving = movement.magnitude > 0;

        if (isMoving)
        {
            // Menggerakkan karakter (perubahan pada posisi sumbu X, Y, Z)
            transform.Translate(movement * speed * Time.deltaTime, Space.World); // Menggunakan Space.World agar posisi X bergerak sesuai input

            // Memutar karakter berdasarkan input horizontal dan vertikal
            if (horizontal != 0 || vertical != 0)
            {
                // Hitung rotasi berdasarkan arah input (horizontal dan vertikal)
                currentRotationY = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;

                // Terapkan rotasi pada karakter
                transform.rotation = Quaternion.Euler(0f, currentRotationY, 0f);
            }
        }

        // Mengatur animasi berlari
        if (isMoving && shiftPressed && !isRunning)
        {
            isRunning = true;
            animator.SetBool("isRunning", true);
        }
        else if ((!isMoving || !shiftPressed) && isRunning)
        {
            isRunning = false;
            animator.SetBool("isRunning", false);
        }

        // Mengatur animasi berjalan dan lompat
        animator.SetBool("isWalking", isMoving && !shiftPressed);
        animator.SetBool("isJumping", isJumping);

        // Menangani lompatan
        if (isJumping)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnFootstep()
    {
        // Fungsi ini dapat digunakan untuk menambahkan suara langkah kaki atau fungsionalitas lain jika diperlukan
    }
}

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerScript : MonoBehaviour
{
    private Animator animator;      // Komponen Animator
    private Rigidbody rb;           // Komponen Rigidbody

    public float moveSpeed = 5f;    // Kecepatan berjalan
    public float runMultiplier = 2f; // Faktor pengali untuk lari
    public float jumpForce = 5f;    // Kekuatan lompat
    public float rotationSpeed = 100f; // Kecepatan rotasi

    private bool isJumping = false; // Mengecek apakah sedang lompat

    void Awake()
    {
        // Ambil komponen Animator dan Rigidbody
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleJump();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // Ambil input pergerakan
        float translation = Input.GetAxis("Vertical") * moveSpeed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Cek jika tombol lari (Shift kiri) ditekan
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        if (isRunning)
        {
            translation *= runMultiplier;
        }

        // Normalisasi kecepatan berdasarkan waktu
        translation *= Time.fixedDeltaTime;
        rotation *= Time.fixedDeltaTime;

        // Gerakkan karakter maju/mundur
        transform.Translate(0, 0, translation);
        // Putar karakter
        transform.Rotate(0, rotation, 0);

        // Atur animasi berdasarkan status berjalan atau lari
        if (translation != 0)
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
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetBool("isJumping", true);
            isJumping = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Reset status lompat saat menyentuh tanah
        animator.SetBool("isJumping", false);
        isJumping = false;
    }
      void OnFootstep()
    {
        // Fungsi tambahan untuk suara langkah
    }
}

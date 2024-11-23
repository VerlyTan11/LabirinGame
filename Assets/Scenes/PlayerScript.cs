using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerScript : MonoBehaviour
{
    private Animator animator;
    public float moveSpeed = 3f;   // Kecepatan bergerak
    public float runMultiplier = 2f; // Faktor pengali kecepatan berlari
    public float jumpForce = 5f;   // Kekuatan lompat
    private Rigidbody rb;
    private Vector3 moveDirection; // Menyimpan arah gerakan terakhir
    private bool canJump = true; // Apakah karakter bisa melompat
    private bool canDoubleJump = false; // Apakah karakter bisa melakukan double jump

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        // Mengatur arah gerakan berdasarkan input tombol
        Vector3 inputDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) inputDirection += Vector3.forward;  // Maju
        if (Input.GetKey(KeyCode.S)) inputDirection += Vector3.back;    // Mundur
        if (Input.GetKey(KeyCode.A)) inputDirection += Vector3.left;    // Kiri
        if (Input.GetKey(KeyCode.D)) inputDirection += Vector3.right;   // Kanan

        // Normalisasi arah agar kecepatan tetap konstan di diagonal
        inputDirection = inputDirection.normalized;

        // Jika ada input, ubah arah gerakan dan rotasi karakter
        if (inputDirection != Vector3.zero)
        {
            moveDirection = inputDirection; // Simpan arah input terakhir
            transform.rotation = Quaternion.LookRotation(moveDirection); // Putar karakter sesuai arah
        }
        else
        {
            moveDirection = Vector3.zero; // Setel ke nol jika tidak ada input
        }

        // Cek apakah Shift ditekan untuk berlari
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float speed = isRunning ? moveSpeed * runMultiplier : moveSpeed;

        // Gerakkan karakter hanya jika ada arah gerakan
        if (moveDirection != Vector3.zero)
        {
            Vector3 movement = moveDirection * speed * Time.deltaTime;
            rb.MovePosition(rb.position + movement);
        }

        // Mengatur animasi berdasarkan status gerakan
        animator.SetBool("isWalking", inputDirection != Vector3.zero && !isRunning);
        animator.SetBool("isRunning", inputDirection != Vector3.zero && isRunning);
    }

    private void HandleJump()
    {
        // Jika tombol Space ditekan dan karakter bisa melompat
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetBool("isJumping", true);
            canJump = false; // Matikan kemampuan lompat pertama
            canDoubleJump = true; // Aktifkan kemampuan double jump
        }
        // Jika tombol Shift Left + Space ditekan dan double jump tersedia
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Space) && canDoubleJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canDoubleJump = false; // Matikan kemampuan double jump
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Reset kemampuan lompat saat menyentuh tanah
        if (collision.contacts[0].normal.y > 0.5f) // Pastikan menyentuh permukaan datar
        {
            canJump = true;
            canDoubleJump = false;
            animator.SetBool("isJumping", false);
        }
    }

    void OnFootstep()
    {
        // Fungsi tambahan untuk suara langkah (opsional)
    }
}

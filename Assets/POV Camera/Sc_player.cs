using UnityEngine;

// public class Sc_player : MonoBehaviour {
//     public float minimumY = -60f;
//     public float maximumY = 60f;
//     private float speedPutar = 5f;
//     private float normalSpeed = 5f;
//     private float runSpeed = 20f;
//     float rotationX = 0f;
//     float rotationY = 0f;
//     private Animator anim;

//     void Start() {
//         anim = GetComponent<Animator>();
//     }

//     void Update () {
//         // Perputaran koordinat Y camera
//         rotationY = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * speedPutar;
//         rotationX += Input.GetAxis("Mouse Y") * speedPutar;
//         rotationX = Mathf.Clamp(rotationX, minimumY, maximumY);

//         Vector3 rotasiCamera = this.transform.localEulerAngles;
//         rotasiCamera.y = rotationY;
//         this.transform.localEulerAngles = rotasiCamera;

//         // Perputaran koordinat X Player
//         Vector3 rotasiPlayer = this.transform.Find("Main Camera").localEulerAngles;
//         rotasiPlayer.x = -rotationX;
//         this.transform.Find("Main Camera").localEulerAngles = rotasiPlayer;

//         // Gerakkan maju mundur Player
//         float h = Input.GetAxis("Horizontal");
//         float v = Input.GetAxis("Vertical");
//         float currentSpeed = Input.GetKey(KeyCode.LeftShift) && v > 0 ? runSpeed : normalSpeed;
//         this.transform.Translate(new Vector3(h, 0, v) * Time.deltaTime * currentSpeed);

//         // Aktifkan animasi jalan jika ada pergerakan
//         if (anim != null) {
//             bool isJalan = h != 0 || v != 0;
//             anim.SetBool("isJalan", isJalan);
//         }

//         // Lompat
//         if (Input.GetKeyDown(KeyCode.Space)) {
//             this.GetComponent<Rigidbody>().velocity = Vector3.up * 7f;
//         }
//     }
// }

// using UnityEngine;

// public class Sc_player : MonoBehaviour {
//     public float minimumY = -60f;
//     public float maximumY = 60f;
//     private float speedPutar = 5f;
//     private float normalSpeed = 5f;
//     private float runSpeed = 20f;
//     float rotationX = 0f;
//     float rotationY = 0f;
//     private Animator anim;
//     private bool isJumping = false; // Variabel untuk melacak status lompat
//     public float jumpHeight = 20f; // Tinggi standar lompatan

//     void Start() {
//         anim = GetComponent<Animator>();
//     }

//     void Update () {
//         // Perputaran koordinat Y camera
//         rotationY = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * speedPutar;
//         rotationX += Input.GetAxis("Mouse Y") * speedPutar;
//         rotationX = Mathf.Clamp(rotationX, minimumY, maximumY);

//         Vector3 rotasiCamera = this.transform.localEulerAngles;
//         rotasiCamera.y = rotationY;
//         this.transform.localEulerAngles = rotasiCamera;

//         // Perputaran koordinat X Player
//         Vector3 rotasiPlayer = this.transform.Find("Main Camera").localEulerAngles;
//         rotasiPlayer.x = -rotationX;
//         this.transform.Find("Main Camera").localEulerAngles = rotasiPlayer;

//         // Gerakkan maju mundur Player
//         float h = Input.GetAxis("Horizontal");
//         float v = Input.GetAxis("Vertical");
//         float currentSpeed = Input.GetKey(KeyCode.LeftShift) && v > 0 ? runSpeed : normalSpeed;
//         this.transform.Translate(new Vector3(h, 0, v) * Time.deltaTime * currentSpeed);

//         // Aktifkan animasi jalan jika ada pergerakan
//         if (anim != null) {
//             bool isJalan = h != 0 || v != 0;
//             anim.SetBool("isJalan", isJalan);
//         }

//         // Lompat
//         if (Input.GetKeyDown(KeyCode.Space) && !isJumping) {
//             this.GetComponent<Rigidbody>().velocity = Vector3.up * jumpHeight; // Gunakan tinggi lompatan 20
//             isJumping = true; // Set status lompat menjadi true
//         }
//     }

//     private void OnCollisionEnter(Collision collision) {
//         // Reset status lompat saat pemain menyentuh tanah
//         if (collision.gameObject.CompareTag("Ground")) {
//             isJumping = false;
//         }
//     }

//     void FixedUpdate() {
//         // Reset status lompat saat kecepatan vertikal mendekati nol (pemain turun)
//         if (Mathf.Abs(this.GetComponent<Rigidbody>().velocity.y) < 0.1f) {
//             isJumping = false;
//         }
//     }
// }

public class Sc_player : MonoBehaviour {
    public float minimumY = -60f; // kamera
    public float maximumY = 60f;
    private float speedPutar = 5f;
    private float normalSpeed = 50f; // Kecepatan normal tetap
    float rotationX = 0f;
    float rotationY = 0f;
    private Animator anim;
    private bool isJumping = false; // Variabel untuk melacak status lompat
    public float jumpHeight = 0.1f; // Tinggi lompatan

    private Rigidbody rb;

    void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>(); // Mendapatkan komponen Rigidbody
        rb.freezeRotation = true; // Membekukan rotasi pada Rigidbody agar tidak terpengaruh fisika
        rb.useGravity = true; // Memastikan gravitasi aktif
    }

    void Update () {
        // Perputaran koordinat Y camera
        rotationY = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * speedPutar;
        rotationX += Input.GetAxis("Mouse Y") * speedPutar;
        rotationX = Mathf.Clamp(rotationX, minimumY, maximumY);

        Vector3 rotasiCamera = this.transform.localEulerAngles;
        rotasiCamera.y = rotationY;
        this.transform.localEulerAngles = rotasiCamera;

        // Perputaran koordinat X Player
        Vector3 rotasiPlayer = this.transform.Find("Main Camera").localEulerAngles;
        rotasiPlayer.x = -rotationX;
        this.transform.Find("Main Camera").localEulerAngles = rotasiPlayer;

        // Gerakkan maju mundur Player
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(h, 0, v) * Time.deltaTime * normalSpeed;
        rb.MovePosition(transform.position + transform.TransformDirection(movement));

        // Aktifkan animasi jalan jika ada pergerakan
        if (anim != null) {
            bool isJalan = h != 0 || v != 0;
            anim.SetBool("isJalan", isJalan);
        }

        // Lompat
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping) {
            rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
            isJumping = true;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        // Reset status lompat saat pemain menyentuh tanah
        if (collision.gameObject.CompareTag("Ground")) {
            isJumping = false;
        }
    }
}

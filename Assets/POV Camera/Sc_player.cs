using UnityEngine;

public class Sc_player : MonoBehaviour {
    public float minimumY = -60f;
    public float maximumY = 60f;
    private float speedPutar = 5f;
    private float normalSpeed = 5f;
    private float runSpeed = 20f;
    float rotationX = 0f;
    float rotationY = 0f;
    private Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
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
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) && v > 0 ? runSpeed : normalSpeed;
        this.transform.Translate(new Vector3(h, 0, v) * Time.deltaTime * currentSpeed);

        // Aktifkan animasi jalan jika ada pergerakan
        if (anim != null) {
            bool isJalan = h != 0 || v != 0;
            anim.SetBool("isJalan", isJalan);
        }

        // Lompat
        if (Input.GetKeyDown(KeyCode.Space)) {
            this.GetComponent<Rigidbody>().velocity = Vector3.up * 7f;
        }
    }
}

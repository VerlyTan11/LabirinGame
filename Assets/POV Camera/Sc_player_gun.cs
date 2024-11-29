using UnityEngine;

public class Sc_player_gun : MonoBehaviour
{
    public GameObject[] objWeapon;
    public GameObject[] objMuzzleFlash;
    [SerializeField] GameObject objBulletHole;
    private int senjataAktif = 0;
    private Animator anim;
    public AudioSource audioSource;
    public AudioClip shotSound;
    public AudioClip shootingSound;
    public AudioClip walkSound;
    public AudioClip reloadSound;
    private bool isJalan = false;

    void Start()
    {
        gantiSenjata(0);
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Mengganti Senjata Dengan Angka Keyboard
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gantiSenjata(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gantiSenjata(1);
        }

        // Menembak
        if (Input.GetMouseButton(0))
        {
            shaking_shooting_and_flash();
            tampil_raycast();

            // Play sound when shooting
            if (audioSource != null && shotSound != null)
            {
                audioSource.PlayOneShot(shotSound);
            }
        }

        // Reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartReload();
        }

        CheckWalkingSound();

        
    }

    private void gantiSenjata(int noSenjata)
    {
        if (this.transform.Find("senjata_aktif") != null)
        {
            Destroy(this.transform.Find("senjata_aktif").gameObject);
        }
        GameObject playerWeapon = Instantiate(objWeapon[noSenjata], this.transform.position, this.transform.rotation) as GameObject;
        playerWeapon.transform.SetParent(this.transform);
        playerWeapon.name = "senjata_aktif";
        senjataAktif = noSenjata;

         if ( reloadSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(reloadSound);
        }

    }

    private void StartReload()
    {
        // Play reload sound
        if ( reloadSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(reloadSound);
        }
        
        // Trigger reload animation
        anim.SetBool("reloading", true);
        
        // Schedule the end of reload after the duration of the animation
        Invoke("selesai_reload", 0.5f);
    }

    private void selesai_reload()
    {
        anim.SetBool("reloading", false);
    }

    void shaking_shooting_and_flash()
    {
        Vector3 pos = GameObject.Find("Player").transform.position;
        pos.x -= (Random.value - 0.5f) * 0.5f;
        pos.y -= (Random.value - 0.5f) * 0.5f;
        pos.z -= 0.05f;
        GameObject.Find("Player").transform.position = pos;

        int randFlash = Random.Range(0, objMuzzleFlash.Length);
        Vector3 posFlash = GameObject.Find("tempat_flash").transform.position;
        GameObject objFlash = Instantiate(objMuzzleFlash[randFlash], posFlash, this.transform.rotation) as GameObject;
        objFlash.transform.parent = GameObject.Find("tempat_flash").transform;
        Destroy(objFlash, 0.1f);

        if (shootingSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootingSound);
        }
    }

    void tampil_raycast()
{
    Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
    RaycastHit hit;

    if (Physics.Raycast(ray, out hit))
    {
        // Jika mengenai objek dengan tag "Enemy"
        if (hit.transform.CompareTag("Enemy"))
        {
            // Buat clone bullet hole di posisi tabrakan dengan rotasi yang sesuai
            GameObject bulletHole = Instantiate(objBulletHole, hit.point, Quaternion.LookRotation(hit.normal));
            
            // Set bullet hole sebagai anak dari objek Enemy agar menempel di permukaannya
            bulletHole.transform.SetParent(hit.transform, true);

            // Menambahkan efek dorongan pada objek Enemy jika ada Rigidbody
            Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(-hit.normal * 500); // Sesuaikan daya dorong sesuai kebutuhan
            }
        }
    }
    else
    {
        Debug.Log("Tidak ada objek yang terkena.");
    }
}


    private void CheckWalkingSound()
    {
        if (isJalan && !audioSource.isPlaying)
        {
            audioSource.clip = walkSound;
            audioSource.loop = true;
            audioSource.Play();
        }
        else if (!isJalan && audioSource.isPlaying && audioSource.clip == walkSound)
        {
            audioSource.Stop();
        }
    }
}

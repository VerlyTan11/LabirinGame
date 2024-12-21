using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider; // Slider UI
    public AudioSource audioSource; // Audio Source di GameObject

    void Start()
    {
        // Set volume dari PlayerPrefs atau default 0.5
        float savedVolume = PlayerPrefs.GetFloat("Volume", 0.5f);
        volumeSlider.value = savedVolume;
        audioSource.volume = savedVolume;

        // Tambah listener untuk perubahan volume
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    public void ChangeVolume(float volume)
    {
        audioSource.volume = volume; // Atur volume real-time
        PlayerPrefs.SetFloat("Volume", volume); // Simpan pengaturan volume
    }
}

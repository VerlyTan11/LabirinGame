using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    // Referensi tombol Play dan Quit
    public Button playButton;
    public Button quitButton;

    void Start()
    {
        // Memastikan tombol diatur melalui Inspector
        if (playButton == null || quitButton == null)
        {
            Debug.LogError("PlayButton atau QuitButton belum diassign di Inspector.");
            return;
        }

        // Menambahkan listener untuk tombol
        playButton.onClick.AddListener(OnPlayButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);

        // Tombol Quit tidak aktif saat awal
        quitButton.gameObject.SetActive(false);
    }

    public void OnPlayButtonClicked()
    {
        Debug.Log("Play Button Clicked!");

        // Sembunyikan tombol Play
        playButton.gameObject.SetActive(false);

        // Tampilkan tombol Quit
        quitButton.gameObject.SetActive(true);

        // Logika tambahan untuk memulai game bisa ditambahkan di sini
    }

    public void OnQuitButtonClicked()
    {
        Debug.Log("Quit Button Clicked!");

        // Keluar dari aplikasi (hanya berfungsi di versi build)
        Application.Quit();
    }
}

using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public GameObject mainMenuCanvas; // Drag MainMenuCanvas ke sini di Inspector

    void Start()
    {
        // Pastikan permainan terhenti saat menu utama aktif
        Time.timeScale = 0;  // Game pause ketika menu utama aktif
    }

    public void PlayGame()
    {
        // Sembunyikan seluruh MainMenuCanvas yang mencakup background dan UI lainnya
        if (mainMenuCanvas != null)
        {
            mainMenuCanvas.SetActive(false); // Sembunyikan seluruh menu utama dan background
        }

        // Unpause permainan (melanjutkan game)
        Time.timeScale = 1;

        // Log untuk memastikan bahwa fungsi PlayGame dipanggil
        Debug.Log("Game Started, Main Menu and Background Hidden");
    }

    public void QuitGame()
    {
        // Quit aplikasi (berfungsi di build)
        Debug.Log("Game Quit");
        Application.Quit();
    }
}

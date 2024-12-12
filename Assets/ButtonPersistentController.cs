using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPersistentController : MonoBehaviour
{
    private static ButtonPersistentController instance;

    private void Awake()
    {
        // Cek apakah instance sudah ada
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Hapus duplikat
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Jaga button tetap ada
    }

    // Fungsi untuk Restart Game
    public void RestartGame()
    {
        Time.timeScale = 1; // Reset waktu jika game di-pause
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart scene saat ini
    }
}
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Referensi tombol UI
    public Button startButton;
    public Button pauseButton;

    // Referensi teks pada tombol
    private Text startButtonText;
    private Text pauseButtonText;

    // Status permainan
    private bool isGameRunning = false;
    private bool isGamePaused = false;

    void Start()
    {
        // Ambil komponen Text dari tombol
        startButtonText = startButton.GetComponentInChildren<Text>();
        pauseButtonText = pauseButton.GetComponentInChildren<Text>();

        // Tambahkan listener ke tombol
        startButton.onClick.AddListener(StartGame);
        pauseButton.onClick.AddListener(TogglePause);

        // Awalnya hanya tombol Start yang aktif
        startButton.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);

        // Ubah teks awal tombol
        startButtonText.text = "Start Game";
        pauseButtonText.text = "Pause";
    }

    void Update()
    {
        // Game logic saat berjalan
        if (isGameRunning && !isGamePaused)
        {
            // Contoh logika game (gerakan dummy)
            Debug.Log("Game is running...");
        }
    }

    void StartGame()
    {
        Debug.Log("Start button clicked!");
        isGameRunning = true;
        isGamePaused = false;

        // Aktifkan tombol Pause, sembunyikan tombol Start
        startButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);

        // Debug log dan ubah teks jika diperlukan
        Debug.Log("Game started!");
    }

    void TogglePause()
    {
        Debug.Log("Pause button clicked!");
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            Debug.Log("Game paused.");
            pauseButtonText.text = "Resume";
        }
        else
        {
            Debug.Log("Game resumed.");
            pauseButtonText.text = "Pause";
        }
    }
}

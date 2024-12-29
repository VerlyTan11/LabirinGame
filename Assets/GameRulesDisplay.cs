using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameRulesDisplay : MonoBehaviour
{
    public GameObject rulesCanvas; // Canvas Rules
    public Button startButton;    // Tombol Start

    void Start()
    {
        // Menampilkan canvas rules saat permainan dimulai
        rulesCanvas.SetActive(true);
        Time.timeScale = 0; // Pause game saat rules ditampilkan

        // Menambahkan listener pada tombol
        startButton.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        // Menghilangkan canvas rules dan memulai permainan
        rulesCanvas.SetActive(false);
        Time.timeScale = 1; // Melanjutkan game setelah rules ditutup
    }
}

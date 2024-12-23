using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;
using System;

public class TimerManager : MonoBehaviour
{
    private float timer;
    private bool isTiming;
    private string recordFilePath;

    public TextMeshProUGUI timerText; // Referensi ke UI Text untuk menampilkan timer
    public TextMeshProUGUI recordsText; // Referensi ke UI Text untuk menampilkan rekaman waktu
    public Button showRecordsButton; // Referensi ke tombol untuk menampilkan rekaman
    public GameObject recordsCanvas; // Referensi ke canvas yang menampilkan rekaman

    void Start()
    {
        timer = 0f;
        isTiming = false;
        recordFilePath = Path.Combine(Application.persistentDataPath, "TimerRecords.txt");

        if (timerText != null)
        {
            timerText.text = "Time: 0m 0.00s"; // Inisialisasi teks timer
        }

        if (recordsText != null)
        {
            recordsText.text = "Recorded Times:\n"; // Inisialisasi teks rekaman waktu
        }

        // Menambahkan listener untuk tombol
        if (showRecordsButton != null)
        {
            showRecordsButton.onClick.AddListener(ShowRecords);
        }

        // Sembunyikan canvas rekaman di awal
        if (recordsCanvas != null)
        {
            recordsCanvas.SetActive(false);
        }
    }

    void Update()
    {
        if (isTiming)
        {
            timer += Time.deltaTime;

            // Update UI Text dengan waktu saat ini
            if (timerText != null)
            {
                int minutes = (int)(timer / 60); // Hitung menit
                float seconds = timer % 60; // Sisa detik
                timerText.text = "Time: " + minutes + "m " + seconds.ToString("F2") + "s";
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Start"))
        {
            StartTimer();
        }
        else if (other.CompareTag("Finish"))
        {
            StopTimer();
            SaveRecord();
        }
    }

    void StartTimer()
    {
        timer = 0f; // Reset timer
        isTiming = true;
        Debug.Log("Timer started.");
    }

    void StopTimer()
    {
        isTiming = false;
        Debug.Log("Timer stopped. Time: " + FormatTime(timer));

        // Update UI Text saat timer berhenti
        if (timerText != null)
        {
            timerText.text = "Final Time: " + FormatTime(timer);
        }
    }

    void SaveRecord()
    {
        string record = "Time: " + FormatTime(timer);
        File.AppendAllText(recordFilePath, record + "\n");
        Debug.Log("Record saved: " + record);
    }

    string FormatTime(float timeInSeconds)
    {
        int minutes = (int)(timeInSeconds / 60); // Hitung menit
        float seconds = timeInSeconds % 60; // Sisa detik
        return minutes + "m " + seconds.ToString("F2") + "s";
    }

    public void ShowRecords()
    {
        if (File.Exists(recordFilePath))
        {
            // Baca semua rekaman dari file
            string[] records = File.ReadAllLines(recordFilePath);
            List<float> times = new List<float>();

            // Parse waktu dari format "Time: Xm Ys" menjadi float
            foreach (string record in records)
            {
                string[] parts = record.Split(' '); // Memisahkan kata berdasarkan spasi
                if (parts.Length >= 3 && parts[0] == "Time:")
                {
                    string timeStr = parts[1] + " " + parts[2]; // Format "Xm Ys"
                    float timeInSeconds = ConvertTimeToSeconds(timeStr);
                    times.Add(timeInSeconds); // Menambahkan waktu ke dalam list
                }
            }

            // Urutkan waktu dari yang terkecil ke yang terbesar
            times.Sort();

            // Menyusun hasil rekaman dalam format urut dan bersih
            string allRecords = "Best Recorded Times:\n";
            for (int i = 0; i < times.Count; i++)
            {
                allRecords += (i + 1) + ". " + FormatTime(times[i]) + "\n"; // Menampilkan nomor urut dan waktu
            }

            // Tampilkan di UI
            if (recordsText != null)
            {
                recordsText.text = allRecords;
            }

            // Tampilkan canvas rekaman
            if (recordsCanvas != null)
            {
                recordsCanvas.SetActive(true);
            }
        }
        else
        {
            Debug.LogWarning("No records found. File does not exist.");
            if (recordsText != null)
            {
                recordsText.text = "No records found.";
            }
        }
    }

    // Fungsi untuk mengkonversi waktu dari format "Xm Ys" ke detik
    float ConvertTimeToSeconds(string timeStr)
    {
        try
        {
            string[] timeParts = timeStr.Split(' '); // Pisahkan berdasarkan spasi
            float minutes = float.Parse(timeParts[0].Replace("m", "")); // Ambil menit
            float seconds = float.Parse(timeParts[1].Replace("s", "")); // Ambil detik
            return minutes * 60 + seconds; // Mengubah menit dan detik menjadi detik total
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to parse time: " + timeStr + " Error: " + e.Message);
            return 0f; // Return 0 jika gagal parsing
        }
    }
}

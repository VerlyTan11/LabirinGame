using UnityEngine;
using TMPro;
using System.IO;
public class HistoryButtonHandler : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject showTimeCanvas;
    public TextMeshProUGUI TimeRecord;

    private string recordFilePath;

    void Start()
    {
        recordFilePath = Path.Combine(Application.persistentDataPath, "TimerRecords.txt");

        if (Canvas != null) Canvas.SetActive(true);
        if (showTimeCanvas != null) showTimeCanvas.SetActive(false);
    }

    public void ShowHistory()
    {
        if (Canvas != null) Canvas.SetActive(false);
        if (showTimeCanvas != null) showTimeCanvas.SetActive(true);

        if (TimeRecord != null)
        {
            if (File.Exists(recordFilePath))
            {
                string records = File.ReadAllText(recordFilePath);
                TimeRecord.text = records;
            }
            else
            {
                TimeRecord.text = "No records found.";
            }
        }
    }

    public void BackToMainMenu()
    {
        if (Canvas != null) Canvas.SetActive(true);
        if (showTimeCanvas != null) showTimeCanvas.SetActive(false);
    }
}

using UnityEngine;

public class CanvasSwitcher : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject ShowTime;

    // Fungsi untuk beralih ke Canvas A
    public void ShowCanvasA()
    {
        ShowTime.SetActive(false);
        MainMenu.SetActive(true);
    }
}

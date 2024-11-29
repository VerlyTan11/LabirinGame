using UnityEngine;
using TMPro; // Tambahkan ini jika menggunakan TextMeshPro

public class ItemCollector : MonoBehaviour
{
    public TMP_Text itemCountText; // Gunakan TMP_Text jika menggunakan TextMeshPro
    private int itemCount = 0; // Jumlah item yang sudah diambil

    public void AddItem()
    {
        itemCount++;
        UpdateItemCountText();
    }

    private void UpdateItemCountText()
    {
        itemCountText.text = "Items Collected: " + itemCount;
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [System.Serializable]
    public class GemDisplay
    {
        public string gemType; // Jenis gems
        public Image gemImage; // Komponen Image di UI
        public Sprite foundSprite; // Sprite untuk gems yang sudah ditemukan
    }

    public List<GemDisplay> gemDisplays; // List untuk semua gems di UI

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CollectGem(string gemType)
    {
        foreach (var display in gemDisplays)
        {
            if (display.gemType == gemType)
            {
                // Ubah gambar menjadi sprite berwarna
                display.gemImage.sprite = display.foundSprite;
                Debug.Log($"Gem {gemType} collected and updated in UI!");
                return;
            }
        }

        Debug.LogWarning($"Gem {gemType} not found in UI!");
    }
}
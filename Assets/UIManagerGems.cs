using UnityEngine;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [System.Serializable]
    public class GemDisplay
    {
        public string gemType; // Jenis gems
        public GameObject gemIcon; // Prefab gems sebagai ikon
    }

    public List<GemDisplay> gemDisplays;

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
                Renderer renderer = display.gemIcon.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = Color.white; // Ubah warna menjadi putih
                    Debug.Log($"Gem {gemType} collected!");
                }
                return;
            }
        }

        Debug.LogWarning($"Gem {gemType} not found in UI!");
    }
}
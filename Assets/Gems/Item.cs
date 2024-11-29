using UnityEngine;

public class Item : MonoBehaviour
{
    private ItemCollector itemCollector;

    void Start()
    {
        // Temukan objek ItemCollector di scene
        itemCollector = FindObjectOfType<ItemCollector>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            Debug.Log("collision");

            // Tambahkan item ke dalam ItemCollector
            if (itemCollector != null)
            {
                itemCollector.AddItem();
            }
        }
    }
}

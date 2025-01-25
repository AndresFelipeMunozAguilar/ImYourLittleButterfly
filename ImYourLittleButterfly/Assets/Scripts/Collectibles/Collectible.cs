using UnityEngine;

public class Collectible : MonoBehaviour
{
    private bool isCollected = false;

    private CollectibleManager collectibleManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collectibleManager = CollectibleManager.instance;
        if (collectibleManager == null)
        {
            Debug.LogError("CollectibleManager instance is not set. Please ensure it is initialized.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if( other.CompareTag("Bubble") && !isCollected)
        {
            isCollected = true;
            collectibleManager.AddCollectible();
            Destroy(gameObject);
        }
    }
}

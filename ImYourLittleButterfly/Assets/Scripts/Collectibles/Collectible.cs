using UnityEngine;

public class Collectible : MonoBehaviour
{
    private bool isCollected = false;

    private CollectibleManager collectibleManager;

    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collectibleManager = CollectibleManager.instance;


        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bubble") && !isCollected)
        {
            isCollected = true;
            collectibleManager.AddCollectible();

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            
            Invoke("DestroyCollectible", 1f);
        }

        void DestroyCollectible()
        {
            Destroy(gameObject);
        }
    }
}

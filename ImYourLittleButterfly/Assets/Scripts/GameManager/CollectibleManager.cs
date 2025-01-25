using UnityEngine;
using TMPro;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager instance;

    private int collectiblesCount = 0;

    [SerializeField] private TMP_Text collectiblesDisplay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if( instance == null)
        {
            instance = this;
        }
 
    }

    public void AddCollectible()
    {
        collectiblesCount++;
    }

    private void OnGUI()
    {
        collectiblesDisplay.text = collectiblesCount.ToString();
    }
}

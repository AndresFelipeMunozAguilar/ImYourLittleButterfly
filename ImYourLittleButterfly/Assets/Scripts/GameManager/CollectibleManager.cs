using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager instance;

    private int collectiblesCount = 0;

    public int maxCollectibles = 3;

    private bool isLevelFinish = false;

    public float waitBeforeChangeScene = 1f;
    [SerializeField] private TMP_Text collectiblesDisplay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    public void AddCollectible()
    {
        collectiblesCount++;

        if (collectiblesCount == maxCollectibles)
        {
            collectiblesCount = 0;
            isLevelFinish = true;
            // Cambiar de escena
            Invoke("ChangeScene", waitBeforeChangeScene);
        }
    }

    private void OnGUI()
    {
        if (!isLevelFinish)
        {
            collectiblesDisplay.text = collectiblesCount.ToString() + "/" + maxCollectibles.ToString();
        }
        else
        {
            collectiblesDisplay.text = "You did it!";
        }

    }

    private void ChangeScene()
    {
        // Cambiar de escena
        SceneManager.LoadScene("WinningScreen");
    }
}

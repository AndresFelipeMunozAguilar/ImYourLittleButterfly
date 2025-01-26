using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Nombre de la escena a la que se cambiará
    [SerializeField] private string sceneName;

    // Método para cambiar de escena
    public void SwitchScene()
    {
        SceneManager.LoadScene("Level1");
    }
}

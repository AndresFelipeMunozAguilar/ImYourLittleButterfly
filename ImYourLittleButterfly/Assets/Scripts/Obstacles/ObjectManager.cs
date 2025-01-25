using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Collider2D))]
public class NewMonoBehaviourScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Burbuja"))
        {
            ResetLevel();
        }
    }

    private void ResetLevel()
    {
        //Efecto visual

        //Esperar 1 seg y reiniciar el nivel
        Invoke("ReloadScene", 1f);
    }

    private void ReloadScene() {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]

public class PlayerCollision : MonoBehaviour
{
    [Header("Animaci�n")]
    [SerializeField] private Animator animator;
    [SerializeField] private string nameTrigger;
    [SerializeField] private float animationCollisionLenght;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animationCollisionLenght += 0.5f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {

            GetComponent<Bubble>().isActive = false;
            GetComponent<Rigidbody2D>().simulated = false;
            ResetLevel();

        }
    }

    private void ResetLevel()
    {

        // Efecto visual
        // QUITAME, BORRAME, ELIMINAME SI YA SE TIENE LA ANIMACIÓN
        animator.SetTrigger(nameTrigger);

        

        //Esperar 1 seg y reiniciar el nivel
        Invoke("ReloadScene", animationCollisionLenght);
    }

    private void ReloadScene()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

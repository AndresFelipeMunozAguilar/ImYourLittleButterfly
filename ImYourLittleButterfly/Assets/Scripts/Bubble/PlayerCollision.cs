using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Collider2D))]
public class PlayerCollision : MonoBehaviour
{
    [Header("Animación")]
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
        if (collision.gameObject.CompareTag("Obstaculo"))
        {
            ResetLevel();
        }
    }

    private void ResetLevel()
    {
        //Efecto visual
        animator.SetTrigger(nameTrigger);
        //Esperar 1 seg y reiniciar el nivel
        Invoke("ReloadScene", animationCollisionLenght);
    }

    private void ReloadScene() {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

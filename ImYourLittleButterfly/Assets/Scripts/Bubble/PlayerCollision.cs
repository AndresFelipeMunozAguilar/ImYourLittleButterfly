using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]

public class PlayerCollision : MonoBehaviour
{
    [Header("Animaci�n")]
    [SerializeField] private Animator animator;
    [SerializeField] private string nameTrigger;
    [SerializeField] private float animationCollisionLenght;

    private AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animationCollisionLenght += 0.5f;
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {

            GetComponent<Bubble>().isActive = false;
            GetComponent<Rigidbody2D>().simulated = false;


            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                
            }

            ResetLevel();

        }
    }

    private void ResetLevel()
    {

        // Efecto visual
        animator.SetTrigger(nameTrigger);

        //Esperar 1 seg y reiniciar el nivel
        Invoke("ReloadScene", animationCollisionLenght);
    }

    private void ReloadScene()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Animations;

public class ComputerManager : MonoBehaviour
{
    [Header("Elementos que se activarán al encender")]
    [SerializeField] private GameObject[] objectsToActivate; // Objetos como diálogos, UI, etc.
    
    [Header("NPC y Burbuja de diálogo")]
    [SerializeField] private GameObject npcObject; // El NPC que aparece primero
    [SerializeField] private GameObject dialogueBubble; // La burbuja de diálogo

    [Header("Botón de encendido")]
    [SerializeField] private Button powerButton; // Asigna el botón desde el Inspector

    [Header("Efecto de sonido")]
    [SerializeField] private AudioClip powerOnSound; 

    [Header("Animator del monitor")]
    [SerializeField] private Animator monitorAnimator; // Asigna el Animator del monitor

    [Header("Tiempos de espera")]
    [SerializeField] public float activationDelay = 2f; // Tiempo que tarda la animación en completarse
    [SerializeField] public float npcAppearanceDelay = 1f; // Tiempo que tarda el NPC en aparecer después de encenderse
    [SerializeField] public float dialogueDelay = 1.5f; // Tiempo antes de que aparezca la burbuja

    [Header("Duración del FadeIn")]
    [SerializeField] public float fadeInDuration = 2f; // Duración del efecto FadeIn (se puede ajustar desde el Inspector)

    private AudioSource audioSource; 
    private bool isOn = false; // Estado de la "computadora"

    void Start()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            SetInvisible(obj);
        }
        audioSource = GetComponent<AudioSource>(); // Obtiene el AudioSource
        SetInvisible(npcObject);
        SetInvisible(dialogueBubble);

        // Asignar el evento al botón de encendido
        powerButton.onClick.AddListener(TurnOnComputer);
    }

    void TurnOnComputer()
    {
        if (isOn) return; // Evita múltiples activaciones

        isOn = true;
        Debug.Log("💻 Computadora encendida!");

        // Reproducir sonido de encendido
        if (powerOnSound && audioSource) 
        {
            audioSource.PlayOneShot(powerOnSound);
        }

        // Iniciar la animación del monitor
        monitorAnimator.SetTrigger("TurnOn"); // Usa el trigger "TurnOn" en el Animator

        // Iniciar la corrutina para activar los objetos después del delay
        StartCoroutine(ActivateObjectsWithDelay());
        
        // Opcional: Desactivar el botón después de encender
        powerButton.gameObject.SetActive(false);
    }

    IEnumerator ActivateObjectsWithDelay()
    {
        yield return new WaitForSeconds(activationDelay); // Espera antes de activar los objetos

        // Activar objetos con FadeIn
        foreach (GameObject obj in objectsToActivate)
        {
            if (!obj.activeSelf) // Solo aplica fade si el objeto no está activado ya
            {
                yield return StartCoroutine(FadeIn(obj)); // Aplicar efecto de Fade In
            }
        }

        yield return new WaitForSeconds(npcAppearanceDelay); // Espera antes de mostrar el NPC
        if (!npcObject.activeSelf) // Solo aplica fade si el NPC no está activado ya
        {
            yield return StartCoroutine(FadeIn(npcObject)); // Aplica FadeIn al NPC
        }

        yield return new WaitForSeconds(dialogueDelay); // Espera antes de mostrar la burbuja de diálogo
        if (!dialogueBubble.activeSelf) // **No se aplica FadeIn al panel de texto**
        {
            dialogueBubble.SetActive(true); // Solo activamos el panel de diálogo sin FadeIn
        }
    }

    IEnumerator FadeIn(GameObject obj)
{
    obj.SetActive(true); // Activar el objeto

    // Si el objeto tiene un SpriteRenderer (para objetos no UI)
    if (obj.GetComponent<SpriteRenderer>() != null)
    {
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = 0f; // Comienza con una opacidad de 0 (invisible)
        spriteRenderer.color = color;

        float duration = fadeInDuration; // Usar la variable fadeInDuration configurada en el Inspector
        float elapsedTime = 0f;
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / duration); // Interpolamos entre 0 y 1
            spriteRenderer.color = color; // Aplicamos el nuevo color con opacidad
            yield return null;
        }

        color.a = 1f; // Aseguramos que la opacidad sea 1 al final
        spriteRenderer.color = color;
    }
    // Si el objeto tiene un CanvasGroup (para objetos UI)
    else if (obj.GetComponent<CanvasGroup>() != null)
    {
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        float duration = fadeInDuration; // Usar la variable fadeInDuration configurada en el Inspector
        float elapsedTime = 0f;
        obj.SetActive(true); // Solo activamos el panel de diálogo, sin FadeIn
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = canvasGroup.alpha + (Time.deltaTime / duration); // Interpolamos entre 0 y 1
            yield return null;
        }
        }
    }


    private void SetInvisible(GameObject obj)
    {
        if (obj == null) return;

        // Si el objeto tiene SpriteRenderer (para objetos no UI)
        if (obj.GetComponent<SpriteRenderer>() != null)
        {
            SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
            Color color = spriteRenderer.color;
            color.a = 0f;
            spriteRenderer.color = color;
        }
        // Si el objeto tiene CanvasGroup (para objetos UI)
        else if (obj.GetComponent<CanvasGroup>() != null)
        {
            CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0f;
        }

        obj.SetActive(false); // Se desactiva hasta que se necesite
    }
}

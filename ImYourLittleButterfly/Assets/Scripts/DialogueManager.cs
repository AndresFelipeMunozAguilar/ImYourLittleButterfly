using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
public class DialogueManager : MonoBehaviour
{
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] public Sprite[] npcSprites;
    [SerializeField] public GameObject npcObject;   

    [Header("Elementos que se activarán al encender")]
    [SerializeField] private GameObject[] objectsToActivate; 
    [SerializeField] private GameObject Bubble;
    [SerializeField] private GameObject BubblePop;
    [SerializeField] private GameObject[] objectsObstacles; 
    [SerializeField] private AudioSource audioSource; // AudioSource para reproducir el sonido
    [SerializeField] private AudioClip typingSound; // Clip de sonido de máquina de escribir
    private bool isDialogueActive;
    private int lineIndex = 0;
    public float typingSpeed = 1f;
    int c = 0; // Variable para cambiar el sprite del NPC
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        SpriteRenderer spriteRenderer = npcObject.GetComponent<SpriteRenderer>();
        if (dialogueText.text == dialogueLines[lineIndex])
        {
            if (Input.GetMouseButtonDown(0))
            {
                NextDialougeLine();
                spriteRenderer.sprite = npcSprites[c];// Cambiar el sprite del NPC
                if(c==npcSprites.Length - 1) //Si c es igual a la longitud del array de sprites, reiniciar c
                {
                    c=0;
                }
                else
                {
                    c++;
                }; 
            }
        }
        if(lineIndex == 3)
        {
            StartCoroutine(ActivateObjectsWithDelay(Bubble));
        }
        else if
        (lineIndex == 4)
        {
            foreach (GameObject obj in objectsToActivate)
            {
                StartCoroutine(ActivateObjectsWithDelay(obj));
            }
        }
        else if(lineIndex == 5)
        {
           foreach (GameObject obj in objectsToActivate)
           {
               StartCoroutine(FadeOut(obj));
           }
            StartCoroutine(FadeOut(Bubble));
            Debug.Log("BubblePop activándose");
            StartCoroutine(ActivateObjectsWithDelay(BubblePop));
           foreach (GameObject obj in objectsObstacles)
           {
               StartCoroutine(ActivateObjectsWithDelay(obj));
           }
        }
        else if(lineIndex == 6)
        {
            foreach (GameObject obj in objectsObstacles)
            {
                StartCoroutine(FadeOut(obj));
            }
        }
    }
    public void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        isDialogueActive = true;
        lineIndex = 0;
        dialogueText.text = dialogueLines[lineIndex];
        StartCoroutine(ShowLine());
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = "";

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void NextDialougeLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
            if (typingSound != null && !audioSource.isPlaying)
            {
                audioSource.PlayOneShot(typingSound);
            } // Reproducir el sonido de la máquina de escribir
        }
        else
        {
            dialoguePanel.SetActive(false); // Desactivar el panel de diálogo
            isDialogueActive = false; // Desactivar el diálogo
            ChangeScene(); // Llamar a la función para cambiar de escena
        }
    }
    private void ChangeScene()
    {
        SceneManager.LoadScene("Level1"); 
    }

    IEnumerator ActivateObjectsWithDelay(GameObject obj)
    {
        yield return new WaitForSeconds(0.2f); // Espera antes de activar los objetos
        if (!obj.activeSelf) // Solo aplica fade si el objeto no está activado ya
        {
            yield return StartCoroutine(FadeIn(obj)); // Aplicar efecto de Fade In
        }
        
    }
    IEnumerator FadeIn(GameObject obj)
    {
        obj.SetActive(true); // Activar el objeto
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = 0f; // Comienza con una opacidad de 0 (invisible)
        spriteRenderer.color = color;
        float duration = 0.4f; 
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

    IEnumerator FadeOut(GameObject obj)
    {
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        float duration = 0.4f; 
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / duration); // Interpolamos entre 1 y 0
            spriteRenderer.color = color; // Aplicamos el nuevo color con opacidad
            yield return null;
        }
        color.a = 0f; // Aseguramos que la opacidad sea 0 al final
        spriteRenderer.color = color;
        obj.SetActive(false); // Desactivar el objeto
    }

}

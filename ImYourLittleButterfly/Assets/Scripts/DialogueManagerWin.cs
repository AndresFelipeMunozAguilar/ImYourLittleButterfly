using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Animations;

public class DialogueManagerWin : MonoBehaviour
{
    [SerializeField, TextArea(4, 6)] private string dialogueLines;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject[] objectsToActivate;
    private bool isDialogueActive;
    private int lineIndex = 0;
    public float typingSpeed = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            SetInvisible(obj);
        }
        StartCoroutine(ActivateObjectsWithDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SetInvisible(GameObject obj)
    {
        if (obj == null) return;
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        obj.SetActive(false); // Se desactiva hasta que se necesite
    }

    IEnumerator FadeIn(GameObject obj)
    {
        obj.SetActive(true); // Activar el objeto antes de hacer FadeIn
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        float duration = 5f; // Usar la variable fadeInDuration configurada en el Inspector
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = canvasGroup.alpha + (Time.deltaTime / duration); // Interpolamos entre 0 y 1
            yield return null;
        }
    }
    IEnumerator ActivateObjectsWithDelay()
    {
        yield return new WaitForSeconds(3f); // Espera antes de activar los objetos

        // Activar objetos con FadeIn
        foreach (GameObject obj in objectsToActivate)
        {
            if (!obj.activeSelf) // Solo aplica fade si el objeto no está activado ya
            {
                yield return StartCoroutine(FadeIn(obj)); // Aplicar efecto de Fade In
            }
        }
    }


}

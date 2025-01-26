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

    private bool isDialogueActive;
    private int lineIndex;
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
}

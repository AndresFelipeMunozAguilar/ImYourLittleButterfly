using UnityEngine;
using TMPro;
using System.Collections;
public class DialogueManager : MonoBehaviour
{
    [SerializeField, TextArea(4,6)] private string[] dialogueLines;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    private bool isDialogueActive;
    private int lineIndex;
    public float typingSpeed = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogueText.text == dialogueLines[lineIndex])
        {
         if(Input.GetMouseButtonDown(0))
             {
            NextDialougeLine();
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
            dialoguePanel.SetActive(false);
            isDialogueActive = false;
        }
    }
}

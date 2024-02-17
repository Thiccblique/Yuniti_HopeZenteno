using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    private Queue<string> sentences;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {

       
        sentences.Clear();

        foreach (string sentence in dialogue.setences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
       
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }   

    IEnumerator TypeSentence (string sentences)
    {
        dialogueText.text = "";
        foreach (char letter in sentences.ToCharArray())
        {
            dialogueText.text += letter;
            FindAnyObjectByType<AudioManager>().Play("Text");
            yield return new WaitForSeconds(.01f);
            yield return null;
        }
    }
    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        
    }


}

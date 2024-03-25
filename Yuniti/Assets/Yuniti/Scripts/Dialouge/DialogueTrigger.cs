using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public Animator gambleAnim;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && FindAnyObjectByType<GameManager>().inRound == false)
        {
            gambleAnim.SetBool("isOpen", true);
            TriggerDialogue();
            Destroy(gameObject);
        }
        
    }
    public void TriggerDialogue()
    {
        FindAnyObjectByType<DialogueManager>().StartDialogue(dialogue);
    }
}

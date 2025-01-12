using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NPC : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;

    public string[] dialogue;

    public static bool isDialogueActive;

    private int index;

    public float wordspeed;

    public bool playerInRange;

    public Text interactText;

    public AudioSource typingSound; 

    public static event Action OnDialogEnded;

    void Start()
    {
        dialogueBox.SetActive(false);
        isDialogueActive = false;

        if (interactText != null)
        {
            interactText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange && !isDialogueActive)
        {
            if (interactText != null && !interactText.gameObject.activeSelf)
            {
                interactText.gameObject.SetActive(true);
                interactText.text = "Press E to interact";
            }
        }
        else if (!playerInRange || isDialogueActive)
        {
            if (interactText != null && interactText.gameObject.activeSelf)
            {
                interactText.gameObject.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            if (dialogueBox.activeInHierarchy)
            {
                if (dialogueText.text == dialogue[index])
                {
                    NextLine();
                }
            }
            else
            {
                dialogueBox.SetActive(true);
                StartCoroutine(TypingText());
                isDialogueActive = true;
            }
        }
    }

    public void ZeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialogueBox.SetActive(false);
    }

    IEnumerator TypingText()
    {
        dialogueText.text = "";
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;

            
            if (typingSound != null && !typingSound.isPlaying)
            {
                typingSound.Play();
            }

            yield return new WaitForSeconds(wordspeed);
        }

        
        if (typingSound != null && typingSound.isPlaying)
        {
            typingSound.Stop();
        }
    }

    public void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            StartCoroutine(TypingText());
        }
        else
        {
            ZeroText();
            isDialogueActive = false;

            OnDialogEnded?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            ZeroText();
            isDialogueActive = false;
        }
    }
}

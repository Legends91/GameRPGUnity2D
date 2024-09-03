using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;

public class DialogueTrigger : MonoBehaviour
{
    public DialogSystem dialogueScript;
    private bool phathiennhanvat;
    private bool Solan = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Solan == true)
        {
            if (collision.tag == "Player")
            {
                phathiennhanvat = true;


                dialogueScript.ToggleIndicator(phathiennhanvat);
                Debug.Log("Nhan F de chat");
            }

        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            phathiennhanvat = false;
          //  dialogueScript.Gioithieu(phathiennhanvat);
            dialogueScript.ToggleIndicator(phathiennhanvat);
            dialogueScript.EndDialogue();
            Debug.Log("Khong the chat");
        }
    }

    void Update()
    {
        if (phathiennhanvat && Input.GetKeyDown(KeyCode.F))
        {
            dialogueScript.StartDialogue();

            Solan = false;
            Debug.Log("Bat dau chat");
        }
    }
}
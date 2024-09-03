using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    
    public GameObject window;
    public GameObject indicator;
    //public GameObject gioithieu;
    public Text dialogueText;
    public List<string> dialogues;
    public float tocdochu;
    public int sodong;
    public int sokytu;
    public bool batdau;
    public bool tieptuc;
   
    public void Awake()
    {
        ToggleWindow(false);
        ToggleIndicator(false);
        Gioithieu(false);
    }
    public void ToggleWindow(bool show)
    {
        window.SetActive(show);
    }

    public void ToggleIndicator(bool show)
    {
        indicator.SetActive(show);
    }

    public void Gioithieu(bool show)
    {
         //   gioithieu.SetActive(show);
    }

        public void StartDialogue()
    {

        if (batdau)
            return;
        batdau = true;
        Gioithieu(true);
        ToggleWindow(true);
        ToggleIndicator(false);
        GetDialogue(0);
        Debug.Log("Chat");
    }

    public void GetDialogue(int i)
    {
        sodong = i;
        sokytu = 0;
        dialogueText.text = string.Empty;
        StartCoroutine(Writing());
    }    
    public void EndDialogue()
    {
        //batdau = false;
        //tieptuc = false;
        StopAllCoroutines();
        ToggleWindow(false);
        ToggleIndicator(false);
    }    
    IEnumerator Writing()
    {
        string currentDialogue = dialogues[sodong];
        dialogueText.text += currentDialogue[sokytu];
        sokytu++;
        if(sokytu < currentDialogue.Length)
        {
            yield return new WaitForSeconds(tocdochu);
            StartCoroutine(Writing());
        }    
        else
        {
            tieptuc = true;
        }    
    }

    void Update()
    {
       if (!batdau) return;
        if (tieptuc && Input.GetKeyDown(KeyCode.F))
        {
            Gioithieu(false);
            tieptuc = false;
            sodong++;
            if (sodong < dialogues.Count)
            {
                GetDialogue(sodong);
            }
            else
            {
                EndDialogue();
            }
        }
    }
}


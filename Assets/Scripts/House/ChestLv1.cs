using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestLv1 : MonoBehaviour
{
    public Animator animator;
    public bool isOpenC = true;
    public Interact Interact;
    public GameObject panelGacha;
    private void Awake()
    {
        animator = GetComponent<Animator>();

        SetPanel(false);
    }

    public void SetPanel(bool control)
    {
        panelGacha.SetActive(control);
    }
    public void OpenChest()
    {
        if (isOpenC == true) 
        {
            animator.SetTrigger("Open");
            SetPanel(true);
        }
    }
    void Open()
    {
        //SetPanel(false);
        isOpenC = false;
    }
}

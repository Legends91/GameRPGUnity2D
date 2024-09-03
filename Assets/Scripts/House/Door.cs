using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator;
    public bool isOpen = false;
    public Interact Interact;
   // private Collider2D col;
    // Start is called before the first frame update
    private void Start()
    {
        //col = gameObject.zGetComponent<Collider2D>();
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
     
    public void ActiveDoor(bool isOpen)
    {
        animator.SetBool("OpenDoor", true);
        animator.SetTrigger("Open");
        isOpen = true;
       // col.enabled = false;    
    }
    public void ActiveDoorClose(bool isOpen) 
    {
        animator.SetBool("OpenDoor", false);
       // col.enabled = true;
    }
}

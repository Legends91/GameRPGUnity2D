using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinTop : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public bool isOpenC = false;
    public Interact Interact;
    // Start is called before the first frame update
    private void Start()
    {
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ActiveCabinTop(bool isOpenC)
    {
        animator.SetBool("OpenCabinTop", true);
        animator.SetTrigger("OpenCabin");
        isOpenC = true;
       
    }
    public void ActiveCabinTopClose(bool isOpenC)
    {
        animator.SetBool("OpenCabinTop", false);
       
    }
}

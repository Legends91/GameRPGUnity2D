using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator aniGate;

    private void Start()
    {
        aniGate = GetComponent<Animator>();
    }

    public void Open()
    {
        aniGate.SetTrigger("Open");
    }
    public void Close() 
    {
        aniGate.SetTrigger("Close");
    }
}

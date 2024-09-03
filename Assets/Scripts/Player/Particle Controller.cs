using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public ParticleSystem ExpParticle;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void ExpDrop()
    {
        ExpParticle.Play();
    }
}

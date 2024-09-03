using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    public ParticleSystem ps;

    List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();
    public PlayerStat stat;
    void Start()
    {
        stat =  GameObject.FindWithTag("Player").GetComponent<PlayerStat>();
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    private void OnParticleTrigger()
    {

        int particle= ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);

        for (int i = 0; i < particle; i++) 
        {
            ParticleSystem.Particle p = particles[i];
            stat.AddExp(1);
            p.remainingLifetime = 0;
            particles[i] = p;
        }
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesController : Singleton<ParticlesController>
{
    public List<ParticleSystem> particleList = new List<ParticleSystem>();

    private void Start()
    {
        
    }

    public void PlayFX(Vector3 position, int fxID)
    {
        if (particleList[fxID] == null)
            return;
        ParticleSystem fx = Instantiate(particleList[fxID], transform);
        fx.transform.position = position;
        fx.Play();
    }
}

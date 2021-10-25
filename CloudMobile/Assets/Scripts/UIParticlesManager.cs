using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIParticlesManager : MonoBehaviour
{
    public static UIParticlesManager Main;

    
    public ParticleSystem[] heartParticles;


    private void Awake()
    {
        if (Main == null)
        {
            Main = this;
        }
        else if (Main != this)
        {
            Destroy(this);
        }
    }

    public void StartHeartEffect()
    {

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainController : MonoBehaviour
{
    [SerializeField] private float startHeight = 50;

    [SerializeField] private ParticleSystem fallPs;

    private bool isRain = false;


    public void StartRaining()
    {
        isRain = true;
        fallPs.Play();
    }

    public void StopRaining()
    {
        isRain = false;
        fallPs.Stop();
    }
    

    private void Update()
    {
        Vector3 pos = fallPs.transform.position;
        pos.y += startHeight;

        fallPs.transform.position = pos;
    }
}

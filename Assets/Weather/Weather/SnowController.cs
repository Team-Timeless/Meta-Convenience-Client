using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowController : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;

    private bool isSnow = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            isSnow = !isSnow;

            if (isSnow)
            {
                ps.time = 0;
                ps.Play();
            }
            else
            {
                ps.Stop();
            }
        }
    }
}

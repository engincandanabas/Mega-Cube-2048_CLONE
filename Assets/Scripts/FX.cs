using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class FX : MonoBehaviour
{
    public static FX instance;
    [SerializeField] private ParticleSystem cubeExplosionFX;
    private ParticleSystem.MainModule cubeExplosionFXMainModule;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        cubeExplosionFXMainModule = cubeExplosionFX.main;
    }

    public void PlayCubeExplosionFX(Vector3 position, Color color)
    {
        cubeExplosionFXMainModule.startColor = new ParticleSystem.MinMaxGradient(color);
        cubeExplosionFX.transform.position = position;
        cubeExplosionFX.Play();
    }
}

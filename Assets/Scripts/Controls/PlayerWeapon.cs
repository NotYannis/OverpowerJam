using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerWeapon : MonoBehaviour
{
    [HideInInspector]
    new public ParticleSystem particleSystem;
    
    private void Awake()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }
}


using UnityEngine;

public class WaterSpout : MonoBehaviour
{
    [HideInInspector]
    new public ParticleSystem particleSystem;
    
    private void Awake()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }
}


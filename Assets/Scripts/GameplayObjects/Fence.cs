﻿using UnityEngine;

public class Fence : MonoBehaviour
{
    LayerMask destructibles;

    private void Awake()
    {
        destructibles = LayerMask.NameToLayer("Fruit");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.layer == destructibles)
        {
            Destroy(other.collider.gameObject);
            Score.Instance.IncreaseScore(-200);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class AimReticule : MonoBehaviour
{
    [SerializeField]
    private float distanceFromPlayer;

    PlayerPrototypeMovement prototypeMovement;

    private void Start()
    {
        prototypeMovement = GetComponentInParent<PlayerPrototypeMovement>();
    }

    private void Update()
    {
        if (new Vector3(prototypeMovement.spoutDirection.x, prototypeMovement.spoutDirection.y, 0).normalized != Vector3.zero)
            transform.localPosition = new Vector3(prototypeMovement.spoutDirection.x, prototypeMovement.spoutDirection.y, 0).normalized * distanceFromPlayer;
    }
}

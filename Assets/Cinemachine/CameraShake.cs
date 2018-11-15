using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraShake : MonoBehaviour
{
	private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
	    mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
	    if (Input.GetKeyDown(KeyCode.Space))
	    {
		    mainCamera.DOShakePosition(1f, Vector3.one * 5f, 1, 0f, false);
	    }
    }
}

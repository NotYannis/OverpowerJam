using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
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
	    Vector3 pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
	    pos.z = 0f;
	    transform.position = pos;
    }
}

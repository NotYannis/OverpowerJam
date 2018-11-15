using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
	private Camera mainCamera;
#pragma warning disable 649
	[SerializeField] private ParticleSystem softWaterParticles;
	[SerializeField] private ParticleSystem strongWaterParticles;
#pragma warning restore 649
	private ParticleSystem.EmissionModule waterParticlesEmission;
	private float zpos;

	// Start is called before the first frame update
	void Start ()
	{
		Application.targetFrameRate = 60;
	    mainCamera = Camera.main;
		zpos = transform.position.z;
	}

    // Update is called once per frame
    void Update()
    {
	    Vector3 pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
	    pos.z = zpos;
	    transform.position = pos;

	    if (Input.GetMouseButtonDown(0))
	    {
			strongWaterParticles.Stop();
			softWaterParticles.Play();
	    }

		if (Input.GetMouseButtonUp(0))
	    {
		    strongWaterParticles.Play();
		    softWaterParticles.Stop();
	    }
	}
}

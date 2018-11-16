using UnityEngine;

public class WaterSpout : MonoBehaviour
{
    public ParticleSystem normalParticleSystem;
	public ParticleSystem dripParticleSystem;

	private ParticleSystem[] normalParticleSystems;
	private ParticleSystem[] dripParticleSystems;
	private ParticleSystem[] burstParticleSystems;

	private void Start()
	{
		normalParticleSystems = normalParticleSystem.GetComponentsInChildren<ParticleSystem>();
		dripParticleSystems = dripParticleSystem.GetComponentsInChildren<ParticleSystem>();
	}

	public void PlayNormalParticles ()
	{
		for (int i = 0; i < normalParticleSystems.Length; i++)
		{
			normalParticleSystems[i].Play();
		}
	}

	public void StopNormalParticles()
	{
		for (int i = 0; i < normalParticleSystems.Length; i++)
		{
			normalParticleSystems[i].Stop();
		}
	}

	public void PlayDripParticles ()
	{
		for (int i = 0; i < dripParticleSystems.Length; i++)
		{
			dripParticleSystems[i].Play();
		}
	}

	public void StopDripParticles ()
	{
		for (int i = 0; i < dripParticleSystems.Length; i++)
		{
			dripParticleSystems[i].Stop();
		}
	}

	public void PlayBurstParticles ()
	{
		for (int i = 0; i < burstParticleSystems.Length; i++)
		{
			burstParticleSystems[i].Play();
		}
	}

	public void StopBurstParticles ()
	{
		for (int i = 0; i < burstParticleSystems.Length; i++)
		{
			burstParticleSystems[i].Stop();
		}
	}
}


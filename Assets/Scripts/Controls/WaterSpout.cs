using UnityEngine;

public class WaterSpout : MonoBehaviour
{
    public ParticleSystem normalParticleSystem;
	public ParticleSystem dripParticleSystem;
	public ParticleSystem blastParticleSystem;

	private ParticleSystem[] normalParticleSystems;
	public ParticleSystem[] dripParticleSystems;
	public ParticleSystem[] burstParticleSystems;

	private void Start()
	{
		normalParticleSystems = normalParticleSystem.GetComponentsInChildren<ParticleSystem>();
		dripParticleSystems = dripParticleSystem.GetComponentsInChildren<ParticleSystem>();
		burstParticleSystems = blastParticleSystem.GetComponentsInChildren<ParticleSystem>();
	}

	public void PlayNormalParticles ()
	{
		for (int i = 0; i < normalParticleSystems.Length; i++)
		{
			normalParticleSystems[i].Stop();
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
			dripParticleSystems[i].Stop();
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
			burstParticleSystems[i].Stop();
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


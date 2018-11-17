using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class WaterSpout : MonoBehaviour
{
    public ParticleSystem normalParticleSystem;
	public ParticleSystem dripParticleSystem;

	private ParticleSystem[] normalParticleSystems;
	private ParticleSystem[] dripParticleSystems;
	private ParticleSystem[] burstParticleSystems;

	private EventInstance jetSound;
	private EventInstance music;

	private void Start()
	{
		normalParticleSystems = normalParticleSystem.GetComponentsInChildren<ParticleSystem>();
		dripParticleSystems = dripParticleSystem.GetComponentsInChildren<ParticleSystem>();

		jetSound = RuntimeManager.CreateInstance("event:/Spray/Jet");
		//jetSound.start();
		jetSound.setParameterValue("Spray", 1f);

		music = RuntimeManager.CreateInstance("event:/Music");
		//music.start();
		music.setParameterValue("Spray", 1f);
	}

	public void PlayNormalParticles ()
	{
		for (int i = 0; i < normalParticleSystems.Length; i++)
		{
			normalParticleSystems[i].Play();
		}
		jetSound.setParameterValue("Spray", 1f);
		music.setParameterValue("Spray", 1f);
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

		jetSound.setParameterValue("Spray", 0f);
		music.setParameterValue("Spray", 0f);
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

	public void StopJet()
	{
		StopDripParticles();
		StopNormalParticles();
		jetSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
	}
}


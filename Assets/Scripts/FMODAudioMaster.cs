using System.Collections;
using System.Collections.Generic;
using FMOD;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using Debug = UnityEngine.Debug;

public class FMODAudioMaster : MonoBehaviour
{
	private static FMODAudioMaster _instance;

	private List<EventInstance> sounds;

	public static FMODAudioMaster Instance
	{
		get
		{
			if (!(_instance is FMODAudioMaster))
			{
				var objs = FindObjectsOfType<FMODAudioMaster>();
				if (objs.Length > 0)
				{
					_instance = objs[0];
				}
				if (objs.Length > 1)
				{
					Debug.LogError("There is more than one " + "GameStateController" + " in the scene.");
				}
				if (!(_instance is FMODAudioMaster))
				{
					GameObject obj = new GameObject();
					obj.hideFlags = HideFlags.HideAndDontSave;
					_instance = obj.AddComponent<FMODAudioMaster>();
				}
			}
			return _instance;
		}
	}

	public virtual void Awake ()
	{
		if (_instance is FMODAudioMaster)
		{
			Destroy(gameObject);
			return;
		}
		else
		{
			_instance = Instance;
			DontDestroyOnLoad(this);
		}

	}
}

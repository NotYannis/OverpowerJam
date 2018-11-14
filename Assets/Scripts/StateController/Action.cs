using UnityEngine;

namespace StateController
{
	public abstract class Action : ScriptableObject
	{
	   public abstract void Act<T>(T controller) where T : StateController;
	}
}
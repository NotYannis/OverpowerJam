using UnityEngine;

namespace StateControllerManagement
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide<T>(T controller) where T : StateController;
    }
}
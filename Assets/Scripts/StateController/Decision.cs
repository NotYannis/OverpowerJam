using UnityEngine;

namespace StateController
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide<T>(T controller) where T : StateController;
    }
}
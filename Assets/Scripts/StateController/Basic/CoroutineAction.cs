using UnityEngine;

namespace StateControllerManagement
{
    [CreateAssetMenu(fileName = "_CoroutineAction", menuName = "Actions/Coroutine Action")]
    public class CoroutineAction : ScriptableObject
    {
        [System.Serializable]
        public struct DoWhileAction
        {
            public Action action;
            public Decision decision;
        }

        [System.Serializable]
        public struct TimedAction
        {
            public Action action;
            public FloatVariable duration;
        }

        [Tooltip ("DoWhile Actions always happen before timed actions.")]
        public DoWhileAction[] doWhileActions;
        public TimedAction[] timedActions;
    }
}
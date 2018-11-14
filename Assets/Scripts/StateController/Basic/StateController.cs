using System.Collections;
using UnityEngine;

namespace StateControllerManagement
{
    public class StateController : MonoBehaviour
    {
        [SerializeField]
        protected bool debugEnabled;

        public State currentState;
        [HideInInspector]
        public State prevState;
        [SerializeField]
        protected State remainState;

        protected bool isActive = true;

        [Tooltip("Actions which will be executed during every state.\nExample: blinking")]
        [SerializeField]
        protected Action[] consistentActions;
        [SerializeField]
        public Transition[] anyStateTransitions;

        [HideInInspector]
        public float stateTimeElapsed = 0;
        [HideInInspector]
        public int numFramesElapsed = 0;

        float fixedTimeElapsed = 0;

        protected void Update()
        {
            if (!isActive)
            {
                return;
            }
            currentState.UpdateState(this);

            if (consistentActions is  Action[])
            {
                for (int i = 0; i < consistentActions.Length; ++i)
                {
                    consistentActions[i].Act(this);
                }
            }

            numFramesElapsed++;

            stateTimeElapsed += Time.deltaTime;
        }

        public void TransitionToState(State nextState)
        {
            if (nextState != remainState)
            {
                prevState = currentState;
                currentState = nextState;
                OnExitState();
            }
        }
        
        private void FixedUpdate()
        {
            fixedTimeElapsed += Time.fixedDeltaTime;
        }

        public bool CheckIfStateFrameCountElapsed(int numFrames)
        {
            return (numFrames >= numFramesElapsed);
        }

        public bool CheckIfStateCountDownElapsed(float duration)
        {
            return (stateTimeElapsed >= duration);
        }
        
        private void OnExitState()
        {
            prevState.ExitStateAction(this);
            fixedTimeElapsed = 0;
            numFramesElapsed = 0;
            stateTimeElapsed = 0;
            currentState.EnterStateAction(this);
        }
        
        public IEnumerator DoCoroutineAction(CoroutineAction coroutineAction)
        {

            for (int i = 0; i < coroutineAction.doWhileActions.Length; ++i)
            {
                while (!coroutineAction.doWhileActions[i].decision.Decide(this))
                {
                    coroutineAction.timedActions[i].action.Act(this);
                    yield return new WaitForEndOfFrame();
                }
            }

            for (int i = 0; i < coroutineAction.timedActions.Length; ++i)
            {
                float timer = 0;

                while (timer < coroutineAction.timedActions[i].duration.value)
                {
                    coroutineAction.timedActions[i].action.Act(this);
                    timer += Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }
                timer = 0;
            }
            yield return null;
        }
    }
}
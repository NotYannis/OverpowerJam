using UnityEngine;

namespace StateController
{
    [CreateAssetMenu(menuName = "States/Empty State")]
    public class State : ScriptableObject
    {
        public Action[] enterStateActions;
        public Action[] activeStateActions;
        public Action[] exitStateActions;

        [HideInInspector]
        public Action currentAction;
        [ArrayElementTitle("Transition")]
        public Transition[] transitions;
        public Color sceneGizmoColor = Color.grey;

        public void UpdateState<T>(T controller) where T : StateController
        {
            DoActions(controller);
            CheckTransitions(controller);
        }

        private void DoActions<T>(T controller) where T : StateController
        {
            for (int i = 0; i < activeStateActions.Length; i++)
            {
                currentAction = activeStateActions[i];
                currentAction.Act(controller);
            }
        }

        internal void EnterStateAction<T>(T controller) where T : StateController
        {
            for (int i = 0; i < enterStateActions.Length; i++)
            {
                enterStateActions[i].Act(controller);
            }
        }

        internal void ExitStateAction<T>(T controller) where T : StateController
        {
            for (int i = 0; i < exitStateActions.Length; i++)
            {
                exitStateActions[i].Act(controller);
            }
        }

        private void CheckTransitions<T>(T controller) where T : StateController
        {
            for (int i = 0; i < transitions.Length; i++)
            {

                bool decisionsSucceeded = true;
                for (int j = 0; j < transitions[i].decisions.Length; ++j)
                {
                    if (!transitions[i].decisions[j].Decide(controller))
                    {
                        decisionsSucceeded = false;
                    }
                }

                if (decisionsSucceeded)
                {
                    //Debug.Log("Transition number " + i + " State name " + this.name + "\nObject name " + controller.transform.name);

                    controller.TransitionToState(transitions[i].trueState);
                }
                else
                {
                    controller.TransitionToState(transitions[i].falseState);
                }
            }

            for (int i = 0; i < controller.anyStateTransitions.Length; ++i)
            {
                bool anyDecisionsSucceeded = true;

                for (int j = 0; j < controller.anyStateTransitions[i].decisions.Length; ++j)
                {
                    if (!controller.anyStateTransitions[i].decisions[j].Decide(controller))
                    {
                        anyDecisionsSucceeded = false;
                    }
                }

                if (anyDecisionsSucceeded)
                {
                    controller.TransitionToState(controller.anyStateTransitions[i].trueState);
                }
                else
                {
                    controller.TransitionToState(controller.anyStateTransitions[i].falseState);
                }
            }
        }
    }
}
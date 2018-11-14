namespace StateControllerManagement
{
    [System.Serializable]
    public class Transition
    {
        [ArrayElementTitle("Decision")]
        public Decision[] decisions;
        public State trueState;
        public State falseState;
    }
}
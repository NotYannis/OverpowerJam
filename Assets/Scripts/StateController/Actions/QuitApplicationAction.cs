using UnityEngine;
using StateControllerManagement;

[CreateAssetMenu (menuName = "Actions/General/Quit Application")]
public class QuitApplicationAction : Action
{
    public override void Act<T>(T controller)
    {
        Quit();
    }

    public void Act()
    {
        Quit();
    }

    private void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
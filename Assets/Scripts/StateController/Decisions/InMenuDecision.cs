using UnityEngine;
using StateControllerManagement;
using MenuManagement;

[CreateAssetMenu(fileName = "InMenu_Decision", menuName = "Decisions/General/In Menu Decision")]
public class InMenuDecision : Decision
{
    public override bool Decide<T>(T controller)
    {
        return !MenuManager.Instance.currentMenu.Equals(MenuManager.Instance.emptyCanvas);
    }
}
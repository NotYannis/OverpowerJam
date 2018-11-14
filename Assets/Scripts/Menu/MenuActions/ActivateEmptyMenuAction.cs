using UnityEngine;
using StateControllerManagement;
using MenuManagement;

[CreateAssetMenu(fileName = "ActivateEmptyMenu_Action", menuName = "Actions/Menu/Activate Empty Menu")]
public class ActivateEmptyMenuAction : Action 
{
	public override void Act<T>(T controller)
	{
        MenuManager.Instance.currentMenu = MenuManager.Instance.emptyCanvas;
	}
}
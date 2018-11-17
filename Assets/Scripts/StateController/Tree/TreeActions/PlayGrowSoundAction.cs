using UnityEngine;
using StateControllerManagement;
[CreateAssetMenu(menuName = "Actions/Tree/PlayGrowSoundAction")]
public class PlayGrowSoundAction : Action
{
    public override void Act<T>(T controller)
    {
	    PlaySound(controller as TreeStateController);
    }

	private void PlaySound(TreeStateController controller)
	{
		controller.PlayGrowSound();
	}
}
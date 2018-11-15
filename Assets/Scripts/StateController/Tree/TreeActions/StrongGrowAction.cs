using UnityEngine;

using StateControllerManagement;
[CreateAssetMenu(menuName = "Actions/Tree/StrongGrowAction")]
public class StrongGrowAction : Action
{
    public override void Act<T>(T controller)
    {
		HasBeenHitWyWater(controller as TreeStateController);
    }

	private void HasBeenHitWyWater(TreeStateController controller)
	{
		if (controller.isStronglyWatered)
		{
			controller.Grow();
		}
	}
}
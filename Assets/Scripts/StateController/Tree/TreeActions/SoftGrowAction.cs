using UnityEngine;
using StateControllerManagement;

[CreateAssetMenu(menuName = "Actions/Tree/SoftGrowAction")]
public class SoftGrowAction : Action
{
	public override void Act<T> (T controller)
	{
		HasBeenHitWyWater(controller as TreeStateController);
	}

	private void HasBeenHitWyWater (TreeStateController controller)
	{
		if (controller.isSoftlyWatered)
		{
			controller.Grow();
		}
	}
}
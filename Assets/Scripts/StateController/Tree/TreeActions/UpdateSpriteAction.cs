using UnityEngine;
using StateControllerManagement;

[CreateAssetMenu(menuName = "Actions/Tree/UpdateSpriteAction")]
public class UpdateSpriteAction : Action
{
	[SerializeField] SpriteVariable sprite;
    public override void Act<T>(T controller)
    {
	    UpdateSprite(controller as TreeStateController);
	}

	private void UpdateSprite(TreeStateController controller)
	{
		controller.UpdateSprite(sprite.value);
	}
}
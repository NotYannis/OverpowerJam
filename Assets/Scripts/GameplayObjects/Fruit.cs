using UnityEngine;

public class Fruit : MonoBehaviour
{
	[HideInInspector] public new Rigidbody2D rigidbody;
	[SerializeField] private GameObject shadow;
	[SerializeField] float height;
	public const float offset = -0.4f;
	private Vector2 acceleration;
	private float gravityVelocity;

	private bool falling;
    // Start is called before the first frame update
    void Awake()
    {
	    rigidbody = GetComponentInChildren<Rigidbody2D>();
	    lastPosition = rigidbody.position.y;
	    shadow = Instantiate(shadow, transform.position + Vector3.down * height, Quaternion.identity);
	    if (GameStateController.Instance.gameConfig != null)
	    {
		    rigidbody.mass = GameStateController.Instance.gameConfig.fruitMass;
		    rigidbody.drag = GameStateController.Instance.gameConfig.fruitLinearDrag;
		    rigidbody.angularDrag = GameStateController.Instance.gameConfig.fruitAngularDrag;
		    rigidbody.gravityScale = GameStateController.Instance.gameConfig.fruitGravityScale;
	    }
    }

	private void OnEnable()
	{
		shadow.SetActive(true);
	}

	private void OnDisable()
	{
		if(shadow != null)
			shadow.SetActive(false);
	}

	public void Fall()
	{
		falling = true;
		rigidbody.bodyType = RigidbodyType2D.Dynamic;
	}

	private float lastPosition;
	private void FixedUpdate()
	{
		Vector3 pos = rigidbody.position;
		pos.y -= height + offset;
		shadow.transform.position = pos;

		if (falling)
		{
			height += rigidbody.position.y - lastPosition;
		}
		lastPosition = rigidbody.position.y;
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject == shadow)
		{
			falling = false;
			other.gameObject.GetComponent<Collider2D>().enabled = false;
			rigidbody.gravityScale = 0f;
			height = 0.5f;
		}
	}
}

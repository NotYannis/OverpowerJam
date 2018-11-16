using UnityEngine;

public class Fruit : MonoBehaviour
{

	private LayerMask dirtLayer;
    private LayerMask bumperBushLayer;

    [HideInInspector] public new Rigidbody2D rigidbody;
	[SerializeField] private GameObject shadow;
	[SerializeField] float height;
	public const float offset = -0.4f;
	private Vector2 acceleration;
	private float gravityVelocity;

    [SerializeField]
    private float bumperForce = 3;

	private bool falling;

    void Awake()
    {
	    rigidbody = GetComponentInChildren<Rigidbody2D>();
	    lastPosition = rigidbody.position.y;
	    shadow = Instantiate(shadow, transform.position + Vector3.down * height, Quaternion.identity);
	    dirtLayer = LayerMask.NameToLayer("Dirt");
        bumperBushLayer = LayerMask.NameToLayer("BumperBush");
    }

    private void Start()
	{
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
	private void Update ()
	{
		shadow.transform.localScale = transform.lossyScale;
	}

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

		if (other.gameObject.layer == dirtLayer)
		{
			Destroy(gameObject);
			other.gameObject.GetComponent<Collider2D>().enabled = false;
			Instantiate(GameStateController.Instance.gameConfig.tree, other.transform.position, Quaternion.identity, other.transform);
		}

        if (other.gameObject.layer == bumperBushLayer)
        {
            Vector2 direction = gameObject.transform.position - other.gameObject.transform.position;
            direction = direction.normalized;

            rigidbody.AddForce(direction * bumperForce, ForceMode2D.Impulse);
        }
	}
}

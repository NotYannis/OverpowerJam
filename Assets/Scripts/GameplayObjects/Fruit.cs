using System.Collections;
using UnityEngine;

public class Fruit : MonoBehaviour
{

    private LayerMask dirtLayer;
    private LayerMask bumperBushLayer;
    private LayerMask flowerBushLayer;

    [HideInInspector] public new Rigidbody2D rigidbody;
    [SerializeField] private GameObject shadow;
    [SerializeField] float height;
    public SpriteRenderer spriteRenderer;

    public const float offset = -0.4f;
    private Vector2 acceleration;
    private float gravityVelocity;

    [SerializeField]
    private float bumperForce = 3;

    private bool falling;

    int multiplier = 0;
    [HideInInspector]
    public TreeType treeType;

    void Awake()
    {
        rigidbody = GetComponentInChildren<Rigidbody2D>();
        lastPosition = rigidbody.position.y;
        shadow = Instantiate(shadow, transform.position + Vector3.down * height, Quaternion.identity);
        spriteRenderer = GetComponent<SpriteRenderer>();

        dirtLayer = LayerMask.NameToLayer("Dirt");
        bumperBushLayer = LayerMask.NameToLayer("BumperBush");
        flowerBushLayer = LayerMask.NameToLayer("FlowerBush");
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
        if (shadow != null)
            shadow.SetActive(false);
    }

    public void Fall()
    {
        falling = true;
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }

    private float lastPosition;
    private void Update()
    {
        shadow.transform.localScale = transform.lossyScale;

        if (rigidbody.velocity.SqrMagnitude() < 1.12f)
        {
            multiplier = 0;
        }
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

        if (rigidbody.velocity.magnitude > 30)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * 20;
        }
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
            Score.Instance.IncreaseScore(1000);
            Destroy(gameObject);
            other.gameObject.GetComponent<Collider2D>().enabled = false;


            Instantiate(treeType.prefabVariant, other.transform.position, Quaternion.identity, other.transform).GetComponent<TreeStateController>();
        }

        if (other.gameObject.layer == bumperBushLayer)
        {
            multiplier++;
            Score.Instance.IncreaseScore(100 * multiplier);
            Vector2 direction = gameObject.transform.position - other.gameObject.transform.position;
            direction = direction.normalized;
            other.gameObject.GetComponent<Animator>().SetBool("Bumped", true);


            rigidbody.AddForce(direction * bumperForce * Time.deltaTime, ForceMode2D.Impulse);
        }

        if (other.gameObject.layer == flowerBushLayer)
        {
            Score.Instance.IncreaseScore(50);
            Vector2 direction = gameObject.transform.position - other.gameObject.transform.position;
            direction = direction.normalized;

            int sign = Random.value < .5 ? 1 : -1;
            float angle = sign * 90 * Mathf.Deg2Rad;
            float cos = Mathf.Cos(angle);
            float sin = Mathf.Sin(angle);

            float x2 = direction.x * cos - direction.y * sin;
            float y2 = direction.x * sin + direction.y * cos;
            direction = new Vector2(x2, y2);


            rigidbody.AddForce(rigidbody.velocity.magnitude * direction.normalized);
            //rigidbody.AddForce(rigidbody.velocity.magnitude * 2 * direction.normalized);
        }
    }

    bool perSecond = true;

    private void OnParticleCollision(GameObject other)
    {
        if (height != 0.5f)
        {
            return;
        }

        if (perSecond)
        {
            if (other.layer == LayerMask.NameToLayer("SoftWater"))
            {
                Score.Instance.IncreaseScore(1);
                perSecond = false;
                StartCoroutine("PerSecond");
            }
            if (other.layer == LayerMask.NameToLayer("StrongWater"))
            {
                Score.Instance.IncreaseScore(5);
                perSecond = false;
                StartCoroutine("PerSecond");
            }
        }
    }

    private IEnumerator PerSecond()
    {
        yield return new WaitForSeconds(1);
        perSecond = true;
        yield return null;
    }
    
}

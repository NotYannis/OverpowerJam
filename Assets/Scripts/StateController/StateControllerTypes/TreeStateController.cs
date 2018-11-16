using StateControllerManagement;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class TreeStateController : StateController
{
    private LayerMask playerLayer;
    private LayerMask softWaterLayer;
    private LayerMask strongWaterLayer;
    [HideInInspector] public float growthPercentage;
    [HideInInspector] public bool isSoftlyWatered;
    [HideInInspector] public bool isStronglyWatered;
    [SerializeField] private Fruit[] fruits;
    [SerializeField] private TreeType treeType;
    private new SpriteRenderer renderer;
    private int lastTimeWateredFrameCount;

    [FormerlySerializedAs("growthPerWaterDrop")]
    [Range(0.1f, 10f)]
    [SerializeField] private float growthPerWaterFrame = 1f;

    Sprite[] lifeTimeSprites = new Sprite[3];
    int currentLifeTimeindex = 0;

    private void Awake()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        softWaterLayer = LayerMask.NameToLayer("SoftWater");
        strongWaterLayer = LayerMask.NameToLayer("StrongWater");
        renderer = GetComponent<SpriteRenderer>();

        lifeTimeSprites[0] = treeType.seedlingSprite.value;
        lifeTimeSprites[1] = treeType.bushSprite.value;
        lifeTimeSprites[2] = treeType.treeSprite.value;

        renderer.sprite = lifeTimeSprites[currentLifeTimeindex];

        for (int i = 0; i < fruits.Length; i++)
        {
            fruits[i].gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        for (int i = 0; i < fruits.Length; i++)
        {
            fruits[i].spriteRenderer.sprite = treeType.fruitSprite.value;
        }
    }

    protected override void Update()
    {
        base.Update();

        if (isSoftlyWatered)
        {
            lastTimeWateredFrameCount = 0;
            isSoftlyWatered = false;
        }

        if (isStronglyWatered)
        {
            lastTimeWateredFrameCount = 0;
            isStronglyWatered = false;
        }
        if (lastTimeWateredFrameCount < int.MaxValue)
            lastTimeWateredFrameCount++;

        if (growthPercentage >= 0)
        {
            transform.localScale = (Vector3.one * 0.5f) + Vector3.one * growthPercentage * 0.01f;
        }
    }

    public void Grow()
    {
        growthPercentage += growthPerWaterFrame;
    }

    public void Ungrow()
    {
        if (growthPercentage > 0 && lastTimeWateredFrameCount > 10)
        {
            growthPercentage -= GameStateController.Instance.gameConfig.ungrowRate;
        }
    }

    public void GrowFruits()
    {
        for (int i = 0; i < fruits.Length; i++)
        {
            fruits[i].gameObject.SetActive(true);
        }
    }

    public void DropFruits()
    {
        for (int i = 0; i < fruits.Length; i++)
        {
            Fruit newFruit = Instantiate(fruits[i], fruits[i].transform.position, fruits[i].transform.rotation);
            fruits[i].gameObject.SetActive(false);
            newFruit.Fall();
        }
    }

    public void UpdateSprite(Sprite sprite)
    {
        currentLifeTimeindex++;
        if (lifeTimeSprites.Length > currentLifeTimeindex)
        {
            renderer.sprite = lifeTimeSprites[currentLifeTimeindex];
        }
        else
        {
            renderer.sprite = lifeTimeSprites[lifeTimeSprites.Length - 1];
        }
        //   renderer.sprite = sprite;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.layer == softWaterLayer)
        {
            isSoftlyWatered = true;
        }

        if (other.layer == strongWaterLayer)
        {
            isStronglyWatered = true;
        }
    }
}


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

    private new SpriteRenderer renderer;
    private int lastTimeWateredFrameCount;

    [FormerlySerializedAs("growthPerWaterDrop")]
    [Range(0.1f, 10f)]
    [SerializeField] private float growthPerWaterFrame = 1f;


    private void Awake()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        softWaterLayer = LayerMask.NameToLayer("SoftWater");
        strongWaterLayer = LayerMask.NameToLayer("StrongWater");
        renderer = GetComponent<SpriteRenderer>();
        for (int i = 0; i < fruits.Length; i++)
        {
            fruits[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
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
        renderer.sprite = sprite;
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


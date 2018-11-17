using DG.Tweening;
using FMOD;
using FMOD.Studio;
using FMODUnity;
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
    [SerializeField] public TreeType treeType;
    private new SpriteRenderer renderer;
    private int lastTimeWateredFrameCount;

    [FormerlySerializedAs("growthPerWaterDrop")]
    [Range(0.1f, 10f)]
    [SerializeField] private float growthPerWaterFrame = 1f;

    [SerializeField] RuntimeAnimatorController[] bushAnimator;
	private Animator animator;

    Sprite[] lifeTimeSprites = new Sprite[3];
    int currentLifeTimeindex = 0;

	private EventInstance treeSound;

	private string[] growEvents = new[] {"event:/Trees/Seed_grow", "event:/Trees/Bush_grow", "event:/Trees/Tree_grow"};

	private bool isWateredSoundStopped
	{
		get
		{
			PLAYBACK_STATE state;
			treeSound.getPlaybackState(out state);
			return state == PLAYBACK_STATE.STOPPED;
		}
	}

    private void Awake()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        softWaterLayer = LayerMask.NameToLayer("SoftWater");
        strongWaterLayer = LayerMask.NameToLayer("StrongWater");
        renderer = GetComponent<SpriteRenderer>();
	    animator = GetComponent<Animator>();

		lifeTimeSprites[0] = treeType.seedlingSprite.value;
        lifeTimeSprites[1] = treeType.bushSprite.value;
        lifeTimeSprites[2] = treeType.treeSprite.value;

        renderer.sprite = lifeTimeSprites[currentLifeTimeindex];
        
        for (int i = 0; i < fruits.Length; i++)
        {
            fruits[i].gameObject.SetActive(false);
            fruits[i].treeType = treeType;
        }
		treeSound = RuntimeManager.CreateInstance("event:/Water_hit");
	    treeSound.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
    }

	private void Start()
    {
        for (int i = 0; i < fruits.Length; i++)
        {
            fruits[i].spriteRenderer.sprite = treeType.fruitSprite.value;
        }
        bushAnimator[0] = treeType.controller;
        GetComponent<Animator>().runtimeAnimatorController = treeType.controller;
        renderer.sprite = treeType.seedlingSprite.value;
	    PlayGrowSound();
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

    private void LateUpdate()
    {
        if (currentLifeTimeindex == 0)
        {
            GetComponent<Animator>().SetBool("isWatered", false);
        }
    }

    public void Grow()
    {
        growthPercentage += growthPerWaterFrame;
	    if (isWateredSoundStopped)
	    {
		    treeSound.start();
	    }
    }

    public void Ungrow()
    {
        if (growthPercentage > 0 && lastTimeWateredFrameCount > 10)
        {
            growthPercentage -= GameStateController.Instance.gameConfig.ungrowRate;
	        if (!isWateredSoundStopped)
	        {
		        treeSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
	        }
        }
    }

    public void GrowFruits()
    {
        for (int i = 0; i < fruits.Length; i++)
        {
            fruits[i].gameObject.SetActive(true);
            fruits[i].treeType = treeType;
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
        shadows[currentLifeTimeindex].SetActive(false);
        currentLifeTimeindex++;

        if (lifeTimeSprites.Length > currentLifeTimeindex)
        {
            GetComponent<Animator>().runtimeAnimatorController = bushAnimator[currentLifeTimeindex];
            renderer.sprite = lifeTimeSprites[currentLifeTimeindex];

        shadows[currentLifeTimeindex].SetActive(true);
            GetComponent<CircleCollider2D>().offset += Vector2.up * 0.2f;
            GetComponent<CircleCollider2D>().radius += 0.1f;

        }
        else
        {
            shadows[currentLifeTimeindex].SetActive(true);

            renderer.sprite = lifeTimeSprites[lifeTimeSprites.Length - 1];
        }
    }

	public void PlayGrowSound()
	{
		RuntimeManager.PlayOneShotAttached(growEvents[currentLifeTimeindex], gameObject);
	}

    [SerializeField]
    GameObject[] shadows = new GameObject[3];
    private void OnParticleCollision(GameObject other)
    {
        if (other.layer == softWaterLayer)
        {
            isSoftlyWatered = true;
	        if (currentLifeTimeindex == 0)
	        {
		        animator.SetBool("isWatered", true);

		        if (other.transform.position.x > transform.position.x)
			        renderer.flipX = true;
		        else
			        renderer.flipX = false;
	        }
		}

        if (other.layer == strongWaterLayer)
        {
            isStronglyWatered = true;
	        if (currentLifeTimeindex == 0)
	        {
		        animator.SetBool("isWatered", false);
	        }
		}
    }
}


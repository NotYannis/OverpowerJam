using UnityEngine;
using InControl;
using System.Collections;
using DG.Tweening;

public class PlayerPrototypeMovement : MonoBehaviour
{
    Transform spoutTransform;
    WaterSpout waterSpout;

    [SerializeField]
    PlayerLevelStats currentLevel;

    SpriteRenderer spriteRenderer;

    float currentHoldTime = 0;
    float currentKnockoutTime = 0;

    Vector2 rightStickDir;
    Vector2 leftStickDir;
    Vector2 mousePostion;

    Vector2 prevRightStickDir;
    Vector2 prevLeftStickDir;
    Vector2 prevMousePostion;

	ParticleSystem.MainModule normalMainModule;
	ParticleSystem.EmissionModule normalEmissionModule;
	ParticleSystem.VelocityOverLifetimeModule normalVelocityModule;
	ParticleSystem.MinMaxCurve normalVelocityCurve;

	[HideInInspector]
    public Vector2 spoutDirection;
	private float spoutZOffset;

    Vector3 playerVelocity;

    [SerializeField]
    ParticleSystem knockoutParticles;

    bool knockedOut;

    [SerializeField]
    float bumperBounceForce = 1.5f;

    private new Rigidbody2D rigidbody;
    private LayerMask bumperBushLayer;
	private Camera mainCamera;
    private void Awake()
    {
        waterSpout = GetComponentInChildren<WaterSpout>();
        spoutTransform = waterSpout.transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        bumperBushLayer = LayerMask.NameToLayer("BumperBush");
	    mainCamera = Camera.main;

    }

    private void Start()
    {
	    currentLevel.OnUpdate += SetupParticleSystems;
	    SetupParticleSystems();

	    normalMainModule = waterSpout.normalParticleSystem.main;
	    normalEmissionModule = waterSpout.normalParticleSystem.emission;
	    normalVelocityModule = waterSpout.normalParticleSystem.velocityOverLifetime;
		normalVelocityCurve = normalVelocityModule.y;

		prevMousePostion = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        UpgradePlayer(currentLevel);
    }

	private void OnDestroy()
	{
		currentLevel.OnUpdate -= SetupParticleSystems;
	}

	[SerializeField]
    PlayerLevelStats level2;
    [SerializeField]
    PlayerLevelStats level3;

    void Update()
    {
        if (knockedOut)
        {
            return;
        }

        rightStickDir = InputManager.ActiveDevice.RightStick.Vector.normalized;
        leftStickDir = InputManager.ActiveDevice.LeftStick.Vector.normalized;
        mousePostion = Input.mousePosition;

        playerVelocity = new Vector3(leftStickDir.x, leftStickDir.y, 0) * currentLevel.speed * Time.deltaTime;

        transform.position += playerVelocity;

        if (rightStickDir != prevRightStickDir && rightStickDir != Vector2.zero)
        {
            spoutDirection = rightStickDir;
        }
        else if (mousePostion != prevMousePostion)
        {
            Vector2 mouse2WorldPos = mainCamera.ScreenToWorldPoint(mousePostion);
            spoutDirection = -(new Vector2(transform.position.x, transform.position.y) - mouse2WorldPos).normalized;
        }

        if (InputManager.ActiveDevice.Action1)
        {
	        if (InputManager.ActiveDevice.Action1.WasPressed)
	        {
		        waterSpout.PlayDripParticles();
		        waterSpout.StopNormalParticles();
	        }
			currentHoldTime += Time.deltaTime;
            if (spoutDirection.y > 0.7)
            {
                spriteRenderer.sprite = currentLevel.spit_back;
				ToUp();
            }
            else if (spoutDirection.y < -0.7)
            {
                spriteRenderer.sprite = currentLevel.spit_front;
				ToBack();
			}
            else if (spoutDirection.x < 0)
            {
                spriteRenderer.sprite = currentLevel.spit_side;
				ToLeft();
            }
            else
            {
                spriteRenderer.sprite = currentLevel.spit_side;
                ToRight();
            }

            if (currentHoldTime > currentLevel.holdDuration)
            {
                knockedOut = true;
                currentHoldTime = 0;
                knockoutParticles.Play();
                spriteRenderer.sprite = currentLevel.stunSprite;

	            waterSpout.StopDripParticles();
	            PlayBurstParticles(currentHoldTime);

				StartCoroutine("KnockoutTimer");
                StartCoroutine("ApplyKOForce", new Vector2(spoutTransform.transform.right.x,spoutTransform.transform.right.y) * -(currentLevel.knockoutPushbackForce));

                return;
            }

            //Pushback
            transform.position -= spoutTransform.transform.right * Time.deltaTime * (currentLevel.dripPushback * currentLevel.dripForce);
        }
        else
        {
	        if (InputManager.ActiveDevice.Action1.WasReleased && !knockedOut)
	        {
		        waterSpout.StopDripParticles();
		        waterSpout.PlayNormalParticles();
		        PlayBurstParticles(currentHoldTime);
	        }
			currentHoldTime -= Time.deltaTime * currentLevel.holdingDecreaseSpeed;

            currentHoldTime = Mathf.Max(0, currentHoldTime);

            if (spoutDirection.y > 0.7) //BACK
            {
                spriteRenderer.sprite = currentLevel.walking_back;
				ToUp();
            }
            else if (spoutDirection.y < -0.7) //UP
			{
                spriteRenderer.sprite = currentLevel.walking_front;
				ToBack();
            }
            else if (spoutDirection.x < 0) //LEFT
			{
                spriteRenderer.sprite = currentLevel.walking_side;
				ToLeft();
            }
			else //RIGHT
			{
                spriteRenderer.sprite = currentLevel.walking_side;
                ToRight();
            }
            if (currentHoldTime < 0.5)
            {
                transform.position -= spoutTransform.transform.right * Time.deltaTime * (currentLevel.pushback * currentLevel.force);
            }
            else
            {
                transform.position -= spoutTransform.transform.right * Time.deltaTime * (currentLevel.burstPushback * currentLevel.force);

            }
        }
		
	    spoutTransform.localPosition = (Vector3)spoutDirection * currentLevel.spoutOriginMinimumDistance
	                                   - Vector3.up * currentLevel.spoutYOffset + Vector3.forward * spoutZOffset;
	    spoutTransform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(spoutDirection.y, spoutDirection.x) * 180 / Mathf.PI);

		if (InputManager.ActiveDevice.Action2)
        {
            UpgradePlayer(level2);
        }

        if (InputManager.ActiveDevice.Action3)
        {
            UpgradePlayer(level3);
        }

        prevRightStickDir = rightStickDir;
        prevLeftStickDir = leftStickDir;
        prevMousePostion = mousePostion;
    }

	private void ToBack()
	{
		spoutZOffset = -currentLevel.spoutZOffset;
	}

	private void ToUp()
	{
		spoutZOffset = currentLevel.spoutZOffset;
	}

	private void ToLeft()
	{
		spriteRenderer.flipX = false;
		spoutZOffset = currentLevel.spoutZOffset;
	}

	private void ToRight()
	{
		spriteRenderer.flipX = true;
		spoutZOffset = currentLevel.spoutZOffset;
	}

	private void PlayBurstParticles(float time)
	{
		float oldRate = normalEmissionModule.rateOverTimeMultiplier;
		normalEmissionModule.rateOverTimeMultiplier = oldRate * (time + 1);
		DOTween.To(SetBurstRate, normalEmissionModule.rateOverTimeMultiplier, oldRate, time);

		float oldSpeed = normalMainModule.startSpeedMultiplier;
		normalMainModule.startSpeedMultiplier = oldSpeed + (time + 1);
		DOTween.To(SetBurstSpeedMultiplier, normalMainModule.startSpeedMultiplier, currentLevel.force, time);



		normalVelocityCurve.constantMin = time + 1f * -currentLevel.burstSpray;
		normalVelocityCurve.constantMax = time + 1f * currentLevel.burstSpray;
		DOTween.To(SetBurstYMinVelocity, normalVelocityCurve.constantMin, 0f, time);
		DOTween.To(SetBurstYMaxVelocity, normalVelocityCurve.constantMax, 0f, time);

		normalVelocityModule.y = normalVelocityCurve;
	}

	private void SetupParticleSystems()
	{
		if (waterSpout.normalParticleSystem == null || waterSpout.dripParticleSystem == null) return;

		ParticleSystem.MainModule mainModule;
		ParticleSystem.EmissionModule emissionModule;
		ParticleSystem.CollisionModule collisionModule;
		//ParticleSystem.VelocityOverLifetimeModule velocityModule;
		//ParticleSystem.MinMaxCurve velocityCurve;

		//Normal spray
		mainModule = waterSpout.normalParticleSystem.main;
		emissionModule = waterSpout.normalParticleSystem.emission;
		collisionModule = waterSpout.normalParticleSystem.collision;

		mainModule.startSpeed = currentLevel.force;
		mainModule.startLifetime = currentLevel.lifetime;
		emissionModule.rateOverTimeMultiplier = currentLevel.quantity;
		collisionModule.colliderForce = currentLevel.force;

		//Drip spray
		mainModule = waterSpout.dripParticleSystem.main;
		emissionModule = waterSpout.dripParticleSystem.emission;
		collisionModule = waterSpout.normalParticleSystem.collision;

		mainModule.startSpeed = currentLevel.dripForce;
		mainModule.startLifetime = currentLevel.dripLifetime;
		emissionModule.rateOverTimeMultiplier = currentLevel.dripQuantity;
		collisionModule.colliderForce = currentLevel.dripForce;
	}

	private IEnumerator KnockoutTimer()
    {
        yield return new WaitForSeconds(currentLevel.knockoutDuration);

        knockoutParticles.Stop();
        knockedOut = false;

		yield return null;
    }

    void UpgradePlayer(PlayerLevelStats levelUp)
    {
        currentLevel = levelUp;
        spriteRenderer.sprite = currentLevel.normalSprite;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == bumperBushLayer)
        {
            Vector2 direction = gameObject.transform.position - other.gameObject.transform.position;
            direction = direction.normalized;

            StartCoroutine("ApplyForce", direction * bumperBounceForce);
            other.gameObject.GetComponent<Animator>().SetTrigger("Bumped");
        }
    }

    private IEnumerator ApplyForce(Vector2 force)
    {
        while (Vector2.SqrMagnitude(force) > 0.12f)
        {
            transform.position += new Vector3(force.x, force.y, 0) * Time.deltaTime;
            force /= 1.1f; //* Time.deltaTime;//*= Vector2.one * 50 * Time.deltaTime ;
            knockedOut = true;
            yield return new WaitForEndOfFrame();
        }
        knockedOut = false;
        yield return null;
    }


    private IEnumerator ApplyKOForce(Vector2 force)
    {
        while (Vector2.SqrMagnitude(force) > 0.12f)
        {
            transform.position += new Vector3(force.x, force.y, 0) * Time.deltaTime;
            force /= 1.1f; //* Time.deltaTime;//*= Vector2.one * 50 * Time.deltaTime ;

            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

	private void SetBurstRate(float value)
	{
		normalEmissionModule.rateOverTimeMultiplier = value;
	}

	private void SetBurstSpeedMultiplier(float value)
	{
		normalMainModule.startSpeedMultiplier = value;
	}

	private void SetBurstYMinVelocity(float value)
	{
		normalVelocityCurve.constantMin = value;
		normalVelocityModule.y = normalVelocityCurve;
	}

	private void SetBurstYMaxVelocity (float value)
	{
		normalVelocityCurve.constantMax = value;
		normalVelocityModule.y = normalVelocityCurve;
	}
}
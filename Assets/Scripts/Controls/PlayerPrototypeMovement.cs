﻿using UnityEngine;
using InControl;
using System.Collections;

public class PlayerPrototypeMovement : MonoBehaviour
{
    Transform spoutTransform;
    WaterSpout waterSpout;

    [SerializeField]
    PlayerLevelStats currentLevel;

    SpriteRenderer spriteRenderer;
    //ParticleSystem.MainModule particlesMain;
    //ParticleSystem.EmissionModule particlesEmission;
    //ParticleSystem.VelocityOverLifetimeModule particlesVelocity;
    //ParticleSystem.MinMaxCurve sprayCurve;

    float currentHoldTime = 0;
    float currentKnockoutTime = 0;

    Vector2 rightStickDir;
    Vector2 leftStickDir;
    Vector2 mousePostion;

    Vector2 prevRightStickDir;
    Vector2 prevLeftStickDir;
    Vector2 prevMousePostion;

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
        //particlesMain = waterSpout.particleSystem.main;
        //particlesEmission = waterSpout.particleSystem.emission;
        //particlesVelocity = waterSpout.particleSystem.velocityOverLifetime;
        //sprayCurve = particlesVelocity.y;

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
                StartCoroutine("KnockoutTimer");

                //particlesMain.startSpeed = 0;
                //particlesEmission.rateOverTime = 0;

                transform.position -= spoutTransform.transform.right * Time.deltaTime * (currentLevel.knockoutPushbackForce);
                return;
            }

            //particlesMain.startSpeed = currentLevel.miniForce;
            //particlesEmission.rateOverTime = currentLevel.miniQuantity;

            //sprayCurve.constantMin = -currentLevel.miniSpray;
            //sprayCurve.constantMax = currentLevel.miniSpray;

            //Pushback
            transform.position -= spoutTransform.transform.right * Time.deltaTime * (currentLevel.dripPushback * currentLevel.dripForce);
        }
        else
        {
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
			
            //particlesMain.startSpeed = currentLevel.force;
            //particlesEmission.rateOverTime = currentLevel.quantity + (currentHoldTime * currentLevel.extraForceRate);
            if (currentHoldTime < 0.5)
            {
                waterSpout.GetComponentInChildren<ParticleSystem>().gameObject.layer = LayerMask.NameToLayer("SoftWater");
                //sprayCurve.constantMin = -currentLevel.spray;
                //sprayCurve.constantMax = currentLevel.spray;
                transform.position -= spoutTransform.transform.right * Time.deltaTime * (currentLevel.pushback * currentLevel.force);
            }
            else
            {
                waterSpout.GetComponentInChildren<ParticleSystem>().gameObject.layer = LayerMask.NameToLayer("StrongWater");
                //sprayCurve.constantMin = -currentLevel.burstSpray;
                //sprayCurve.constantMax = currentLevel.burstSpray;
                transform.position -= spoutTransform.transform.right * Time.deltaTime * (currentLevel.burstPushback * currentLevel.force);

            }
        }
		
	    spoutTransform.localPosition = (Vector3)spoutDirection * currentLevel.spoutOriginMinimumDistance
	                                   - Vector3.up * currentLevel.spoutYOffset + Vector3.forward * spoutZOffset;
	    spoutTransform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(spoutDirection.y, spoutDirection.x) * 180 / Mathf.PI);

		//particlesMain.startSpeed = particlesMain.startSpeed.constant + new Vector3(InputManager.ActiveDevice.LeftStick.X, InputManager.ActiveDevice.LeftStick.Y, 0).magnitude * Time.deltaTime * playerBaseSpeed / currentLevel.weight;
		//particlesVelocity.x = InputManager.ActiveDevice.LeftStick.X * playerBaseSpeed / currentLevel.weight;
		//particlesVelocity.y =  InputManager.ActiveDevice.LeftStick.Y * playerBaseSpeed / currentLevel.weight;

		//particlesVelocity.y = sprayCurve;

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

	private void SetupParticleSystems()
	{
		if (waterSpout.normalParticleSystem == null || waterSpout.dripParticleSystem == null ||
		    waterSpout.burstParticleSystems == null) return;

		ParticleSystem.MainModule mainModule;
		ParticleSystem.EmissionModule emissionModule;
		ParticleSystem.CollisionModule collisionModule;
		ParticleSystem.VelocityOverLifetimeModule velocityModule;
		ParticleSystem.MinMaxCurve velocityCurve;

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

		//Blast spray
		mainModule = waterSpout.blastParticleSystem.main;
		emissionModule = waterSpout.blastParticleSystem.emission;
		collisionModule = waterSpout.normalParticleSystem.collision;

		mainModule.startSpeed = currentLevel.burstForce;
		mainModule.startLifetime = currentLevel.burstLifetime;
		emissionModule.rateOverTimeMultiplier = currentLevel.burstQuantity;
		collisionModule.colliderForce = currentLevel.burstForce;

		//particlesMain = waterSpout.particleSystem.main;
		//particlesEmission = waterSpout.particleSystem.emission;
		//particlesVelocity = waterSpout.particleSystem.velocityOverLifetime;
		//sprayCurve = particlesVelocity.y;
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

        //particlesEmission.rateOverTime = currentLevel.quantity;
        //particlesMain.startSpeed = currentLevel.force;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == bumperBushLayer)
        {
            Vector2 direction = gameObject.transform.position - other.gameObject.transform.position;
            direction = direction.normalized;

            StartCoroutine("ApplyForce", direction * bumperBounceForce);
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
}
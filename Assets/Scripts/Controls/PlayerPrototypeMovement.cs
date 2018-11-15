using UnityEngine;
using InControl;
using System.Collections;

public class PlayerPrototypeMovement : MonoBehaviour
{
    Transform spoutTransform;
    WaterSpout waterSpout;

    [SerializeField]
    PlayerLevelStats currentLevel;

    SpriteRenderer spriteRenderer;
    ParticleSystem.MainModule particlesMain;
    ParticleSystem.EmissionModule particlesEmission;
    ParticleSystem.VelocityOverLifetimeModule particlesVelocity;
    ParticleSystem.MinMaxCurve sprayCurve;

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

    Vector3 playerVelocity;

    [SerializeField]
    ParticleSystem knockoutParticles;

    bool knockedOut;

    private void Awake()
    {
        waterSpout = GetComponentInChildren<WaterSpout>();
        spoutTransform = waterSpout.transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        particlesMain = waterSpout.particleSystem.main;
        particlesEmission = waterSpout.particleSystem.emission;
        particlesVelocity = waterSpout.particleSystem.velocityOverLifetime;
        sprayCurve = particlesVelocity.y;

        prevMousePostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        UpgradePlayer(currentLevel);
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
            Vector2 mouse2WorldPos = Camera.main.ScreenToWorldPoint(mousePostion);
            spoutDirection = -(new Vector2(transform.position.x, transform.position.y) - mouse2WorldPos).normalized;
        }

        spoutTransform.localPosition = spoutDirection * currentLevel.spoutOriginMinimumDistance;
        spoutTransform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(spoutDirection.y, spoutDirection.x) * 180 / Mathf.PI);


        if (InputManager.ActiveDevice.Action1)
        {
            currentHoldTime += Time.deltaTime;
            if (spoutDirection.y > 0.7)
            {
                spriteRenderer.sprite = currentLevel.spit_back;
            }
            else if (spoutDirection.y < -0.7)
            {
                spriteRenderer.sprite = currentLevel.spit_front;
            }
            else if (spoutDirection.x < 0)
            {
                spriteRenderer.sprite = currentLevel.spit_side;
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.sprite = currentLevel.spit_side;
                spriteRenderer.flipX = true;
            }

            if (currentHoldTime > currentLevel.holdDuration)
            {
                knockedOut = true;
                currentHoldTime = 0;
                knockoutParticles.Play();
                spriteRenderer.sprite = currentLevel.stunSprite;
                StartCoroutine("KnockoutTimer");

                particlesMain.startSpeed = 0;
                particlesEmission.rateOverTime = 0;

                transform.position -= spoutTransform.transform.right * Time.deltaTime * (currentLevel.knockoutPushbackForce);
                return;
            }

            particlesMain.startSpeed = currentLevel.miniForce;
            particlesEmission.rateOverTime = currentLevel.miniQuantity;

            sprayCurve.constantMin = -currentLevel.miniSpray;
            sprayCurve.constantMax = currentLevel.miniSpray;

            //Pushback
            transform.position -= spoutTransform.transform.right * Time.deltaTime * (currentLevel.miniPushback * currentLevel.miniForce);
        }
        else
        {
            currentHoldTime -= Time.deltaTime * currentLevel.holdingDecreaseSpeed;

            currentHoldTime = Mathf.Max(0, currentHoldTime);

            if (spoutDirection.y > 0.7)
            {
                spriteRenderer.sprite = currentLevel.walking_back;
            }
            else if (spoutDirection.y < -0.7)
            {
                spriteRenderer.sprite = currentLevel.walking_front;
            }
            else if (spoutDirection.x < 0)
            {
                spriteRenderer.sprite = currentLevel.walking_side;
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.sprite = currentLevel.walking_side;
                spriteRenderer.flipX = true;
            }


            particlesMain.startSpeed = currentLevel.force;
            particlesEmission.rateOverTime = currentLevel.quantity + (currentHoldTime * currentLevel.extraForceRate);
            if (currentHoldTime < 0.5)
            {
                waterSpout.GetComponentInChildren<ParticleSystem>().gameObject.layer = LayerMask.NameToLayer("SoftWater");
                sprayCurve.constantMin = -currentLevel.spray;
                sprayCurve.constantMax = currentLevel.spray;
                transform.position -= spoutTransform.transform.right * Time.deltaTime * (currentLevel.pushback * currentLevel.force);
            }
            else
            {
                waterSpout.GetComponentInChildren<ParticleSystem>().gameObject.layer = LayerMask.NameToLayer("StrongWater");
                sprayCurve.constantMin = -currentLevel.burstSpray;
                sprayCurve.constantMax = currentLevel.burstSpray;
                transform.position -= spoutTransform.transform.right * Time.deltaTime * (currentLevel.burstPushback * currentLevel.force);

            }


        }

        //particlesMain.startSpeed = particlesMain.startSpeed.constant + new Vector3(InputManager.ActiveDevice.LeftStick.X, InputManager.ActiveDevice.LeftStick.Y, 0).magnitude * Time.deltaTime * playerBaseSpeed / currentLevel.weight;
        //particlesVelocity.x = InputManager.ActiveDevice.LeftStick.X * playerBaseSpeed / currentLevel.weight;
        //particlesVelocity.y =  InputManager.ActiveDevice.LeftStick.Y * playerBaseSpeed / currentLevel.weight;

        particlesVelocity.y = sprayCurve;

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

        particlesEmission.rateOverTime = currentLevel.quantity;
        particlesMain.startSpeed = currentLevel.force;
    }
}
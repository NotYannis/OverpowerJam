using UnityEngine;
using InControl;
using System.Collections;

public class PlayerPrototypeMovement : MonoBehaviour
{
    Transform spoutTransform;
    WaterSpout waterSpout;

    [SerializeField]
    private float playerBaseSpeed = 8;

    [SerializeField]
    PlayerLevelStats currentLevel;

    SpriteRenderer spriteRenderer;
    ParticleSystem.MainModule particlesMain;
    ParticleSystem.EmissionModule particlesEmission;
    ParticleSystem.VelocityOverLifetimeModule particlesVelocity;

    float currentHoldTime = 0;
    float currentKnockoutTime = 0;

    Vector2 rightStickDir;
    Vector2 leftStickDir;

    Vector3 playerVelocity;

    bool knockedOut;

    private void Awake()
    {
        waterSpout = GetComponentInChildren<WaterSpout>();
        spoutTransform =waterSpout.transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        particlesMain = waterSpout.particleSystem.main;
        particlesEmission = waterSpout.particleSystem.emission;
        particlesVelocity = waterSpout.particleSystem.velocityOverLifetime;

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

        playerVelocity = new Vector3(leftStickDir.x, leftStickDir.y, 0) * playerBaseSpeed / currentLevel.weight * Time.deltaTime;

        transform.position += playerVelocity;

        spoutTransform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(rightStickDir.y, rightStickDir.x) * 180 / Mathf.PI);

        if (InputManager.ActiveDevice.Action1)
        {
            currentHoldTime += Time.deltaTime;
            spriteRenderer.sprite = currentLevel.holdingSprite;

            if (currentHoldTime > currentLevel.holdDuration)
            {
                knockedOut = true;
                currentHoldTime = 0;
                StartCoroutine("KnockoutTimer");

                particlesMain.startSpeed = 0;
                particlesEmission.rateOverTime = 0;

                transform.position -= spoutTransform.transform.right * Time.deltaTime * (currentLevel.knockoutPushbackForce);
                return;
            }

            particlesMain.startSpeed = currentLevel.miniForce;
            particlesEmission.rateOverTime = currentLevel.miniQuantity;

            //Pushback
            transform.position -= spoutTransform.transform.right * Time.deltaTime * (currentLevel.miniPushback * currentLevel.miniForce);
        }
        else
        {
            currentHoldTime -= Time.deltaTime;
            currentHoldTime = Mathf.Max(0, currentHoldTime);
            spriteRenderer.sprite = currentLevel.normalSprite;

            particlesMain.startSpeed = currentLevel.force;
            particlesEmission.rateOverTime = currentLevel.quantity;

            //Pushback
            transform.position -= spoutTransform.transform.right * Time.deltaTime * (currentLevel.pushback * currentLevel.force);
        }

        //particlesMain.startSpeed = particlesMain.startSpeed.constant + new Vector3(InputManager.ActiveDevice.LeftStick.X, InputManager.ActiveDevice.LeftStick.Y, 0).magnitude * Time.deltaTime * playerBaseSpeed / currentLevel.weight;
        //particlesVelocity.x = InputManager.ActiveDevice.LeftStick.X * playerBaseSpeed / currentLevel.weight;
        //particlesVelocity.y =  InputManager.ActiveDevice.LeftStick.Y * playerBaseSpeed / currentLevel.weight;

        if (InputManager.ActiveDevice.Action2)
        {
            UpgradePlayer(level2);
        }

        if (InputManager.ActiveDevice.Action3)
        {
            UpgradePlayer(level3);
        }
    }

    private IEnumerator KnockoutTimer()
    {
        yield return new WaitForSeconds(currentLevel.knockoutDuration);
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

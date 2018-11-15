using UnityEngine;
using InControl;

public class PlayerPrototypeMovement : MonoBehaviour
{
    Transform playerWeaponTransform;
    WaterSpout waterSpout;

    [SerializeField]
    private float playerBaseSpeed = 8;

    [SerializeField]
    PlayerLevelStats currentLevel;

    Sprite sprite;
    ParticleSystem.MainModule particlesMain;
    ParticleSystem.EmissionModule particlesEmission;
    ParticleSystem.VelocityOverLifetimeModule particlesVelocity;

    private void Awake()
    {
        waterSpout = GetComponentInChildren<WaterSpout>();
        playerWeaponTransform = GetComponentInChildren<WaterSpout>().transform;
        sprite = GetComponent<SpriteRenderer>().sprite;
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
        transform.position += new Vector3(InputManager.ActiveDevice.LeftStick.X, InputManager.ActiveDevice.LeftStick.Y, 0) * Time.deltaTime * playerBaseSpeed / currentLevel.weight;


        playerWeaponTransform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(InputManager.ActiveDevice.RightStick.Y, InputManager.ActiveDevice.RightStick.X) * 180 / Mathf.PI);

        if (InputManager.ActiveDevice.Action1)
        {
            particlesMain.startSpeed = currentLevel.miniForce;
            particlesEmission.rateOverTime = currentLevel.miniQuantity;

            //Pushback
            transform.position -= playerWeaponTransform.transform.right * Time.deltaTime * (currentLevel.miniPushback * currentLevel.miniForce);
        }
        else
        {
            particlesMain.startSpeed = currentLevel.force;
            particlesEmission.rateOverTime = currentLevel.quantity;

            //Pushback
            transform.position -= playerWeaponTransform.transform.right * Time.deltaTime * (currentLevel.pushback * currentLevel.force);
        }

        particlesMain.startSpeed += new Vector3(InputManager.ActiveDevice.LeftStick.X, InputManager.ActiveDevice.LeftStick.Y, 0).sqrMagnitude;
        particlesVelocity.x = InputManager.ActiveDevice.LeftStick.X * playerBaseSpeed / currentLevel.weight;
        particlesVelocity.y =  InputManager.ActiveDevice.LeftStick.Y * playerBaseSpeed / currentLevel.weight;

        if (InputManager.ActiveDevice.Action2)
        {
            UpgradePlayer(level2);
        }

        if (InputManager.ActiveDevice.Action3)
        {
            UpgradePlayer(level3);
        }
    }

    void UpgradePlayer(PlayerLevelStats levelUp)
    {
        currentLevel = levelUp;
        sprite = currentLevel.sprite;


        particlesEmission.rateOverTime = currentLevel.quantity;
        particlesMain.startSpeed = currentLevel.force;
    }

}

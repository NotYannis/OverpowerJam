using UnityEngine;
using InControl;

public class PlayerPrototypeMovement : MonoBehaviour
{
    Transform playerWeaponTransform;
    PlayerWeapon waterSpout;
    private float speed  = 8;

    [SerializeField]
    PlayerLevelStats currentLevel;

    Sprite sprite;

    private void Awake()
    {
        waterSpout = GetComponentInChildren<PlayerWeapon>();
        playerWeaponTransform = GetComponentInChildren<PlayerWeapon>().transform;
        sprite = GetComponent<SpriteRenderer>().sprite;

    }

    private void Start()
    {
        UpgradePlayer(currentLevel);
    }

    [SerializeField]
    PlayerLevelStats level2;
    [SerializeField]
    PlayerLevelStats level3;

    
    void Update()
    {
        ParticleSystem.MainModule mainParticleS = waterSpout.particleSystem.main;

        mainParticleS.startSpeed = currentLevel.force;

        transform.position += Vector3.right * InputManager.ActiveDevice.LeftStick.X * Time.deltaTime * speed /currentLevel.weight;
        transform.position += Vector3.up * InputManager.ActiveDevice.LeftStick.Y * Time.deltaTime * speed / currentLevel.weight;

        
        playerWeaponTransform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(InputManager.ActiveDevice.RightStick.Y, InputManager.ActiveDevice.RightStick.X) * 180 / Mathf.PI - 90);
        transform.position -= playerWeaponTransform.transform.up * Time.deltaTime * currentLevel.pushback;

        mainParticleS.startSpeed = mainParticleS.startSpeed.constant + new Vector3(InputManager.ActiveDevice.LeftStickX, InputManager.ActiveDevice.LeftStickY, 0).magnitude * Time.deltaTime * speed / currentLevel.weight;
 var emissionModule = waterSpout.particleSystem.emission;

        if (InputManager.ActiveDevice.Action1)
        {
           
            emissionModule.rateOverTime = 0;
        }
        else
        {
            emissionModule.rateOverTime = currentLevel.quantity;
        }

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

        var emissionModule = waterSpout.particleSystem.emission;

        var minMaxCurve = emissionModule.rateOverTime;
        minMaxCurve.constant = currentLevel.quantity;
        
      //  emissionModule.rateOverTime = currentLevel.quantity;

        ParticleSystem.MainModule mainParticleS = waterSpout.particleSystem.main;
        mainParticleS.startSpeed = currentLevel.force;
    }

}

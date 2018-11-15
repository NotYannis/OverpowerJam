using UnityEngine;
using StateControllerManagement;

public class GameStateController : StateController
{
    private static GameStateController _instance;
	public GameConfig gameConfig;
    public static GameStateController Instance
    {
        get
        {
            if (!(_instance is GameStateController))
            {
                var objs = FindObjectsOfType<GameStateController>();
                if (objs.Length > 0)
                {
                    _instance = objs[0];
                }
                if (objs.Length > 1)
                {
                    Debug.LogError("There is more than one " + "GameStateController" + " in the scene.");
                }
                if (!(_instance is GameStateController))
                {
                    GameObject obj = new GameObject();
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    _instance = obj.AddComponent<GameStateController>();
                }
            }
            return _instance;
        }
    }

    public virtual void Awake()
    {
        if (_instance is GameStateController)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = Instance;
            DontDestroyOnLoad(this);
        }
    }

	private void OnDestroy()
	{
		int floata = 0;
	}
}

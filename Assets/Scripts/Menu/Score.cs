using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private static Score _instance;
    public static Score Instance
    {
        get
        {
            if (!(_instance is Score))
            {
                GameObject gO;
                _instance = FindObjectOfType<Score>();

                if (!(_instance is Score))
                {
                    gO = Instantiate(Resources.Load("Prefabs/ScoreCanvas") as GameObject);
                    _instance = gO.GetComponent<Score>();
                }
                else
                {
                    gO = _instance.gameObject;
                }
                DontDestroyOnLoad(gO);
            }
            return _instance;
        }
    }

    private Text scoreText;

    void Awake()
    {
        if (_instance is Score)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = Instance;
            scoreText = GetComponentInChildren<Text>();
        }
    }

    private void OnDestroy()
    {
        _instance = null;
    }

    private int _score;
    public int score
    {
        get { return _score; }
        private set
        {
            _score = value;
            scoreText.text = "Score : " + _score;
        }
    }

    public void IncreaseScore(int points)
    {
        score += points;
    }
}

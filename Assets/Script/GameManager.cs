using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject Player, StairsObj, AllSound;
    [HideInInspector] public int Score = 0, HighScore;
    public bool stairs = false;
    public bool isStarted = false;
    public bool Movement = false;
    public byte stockColor;

    public Camera camera;
    public bool shake = false;

    Transform firstPos;
    private void Awake()
    {

        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;

        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
      //  StartCoroutine(spawner.iwaitSpawner());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartedController()
    {
        isStarted = true;

        UICont.Instance.startPanel.SetActive(false);
    }

    public void GameOver()
    {

        UICont.Instance.startPanel.SetActive(true);

        isStarted = false;
        HighPoint();
        Score = 0;
    }
    public void GetDatas()
    {

        // GEM
        if (PlayerPrefs.HasKey("highscore"))
        {
            HighScore = PlayerPrefs.GetInt("highscore");
        }
        else
        {
            PlayerPrefs.SetInt("highscore", 0);
        }

        // SOUND
        if (!PlayerPrefs.HasKey("sound"))
        {
            PlayerPrefs.SetInt("sound", 1);
        }
    }

    public void Point()
    {
        Score += 1;
        UICont.Instance.score.text = Score.ToString();

    }

    public void HighPoint()
    {
        if (Score > HighScore)
        {
            HighScore = Score;
            UICont.Instance.high.text = HighScore.ToString();

            PlayerPrefs.GetInt("highscore", Score);
        }
        else
        {
            // prefabScoru çekiyor.
            UICont.Instance.high.text = PlayerPrefs.GetInt("highscore").ToString();

        }
    }
    bool Up;
    float timer;
    public void Shake()
    {

        if (timer <= 10)
        {
            if (Up)
            {
                camera.transform.Translate(0, 0.1f, 0);
                Up = false;
            }
            else
            {
                camera.transform.Translate(0, -0.1f, 0);
                Up = true;
            }
            timer += Time.deltaTime;
        }

    }
}

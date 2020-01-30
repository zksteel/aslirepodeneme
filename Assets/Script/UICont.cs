using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICont : MonoBehaviour
{
    public static UICont Instance { get; private set; }
    public Text high, score;
    public Button buy, startButton;

    public GameObject startPanel;
    // Start is called before the first frame update

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

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartButton()
    {


        GameManager.Instance.StartedController();

        startPanel.gameObject.SetActive(false);

    }
}
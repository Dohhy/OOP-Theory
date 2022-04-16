using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }

    public List<GameObject> carList = new List<GameObject>();

    private KeyNames keyData = new KeyNames();

    private Transform m_player;

    [Header("UI Objects")]
    public Text inGameScoreText;
    public Text gameOverScoreText;
    public GameObject gameOverMenu;
    public GameObject gameUI;
    public Slider powerBarSlider;


    private void Awake()
    {
        gameManager = this;
        carList[PlayerPrefs.GetInt(keyData.selectedKey)].SetActive(true);
    }
    private void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Time.timeScale = 1;
    }
    private void Update()
    {
        inGameScoreText.text = $"{(int)m_player.position.z / 10}";
    }

    public void GameOver()
    {
        gameOverScoreText.text = $"Score: {(int)m_player.position.z / 10}";
        gameOverMenu.SetActive(true);
        gameUI.SetActive(false);
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(1);
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
    public void SetPowerBarValue(int nextTime)
    {
        powerBarSlider.maxValue = nextTime;
    }
    public void FillPowerBar(int time)
    {
        powerBarSlider.value = time;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuUIHandler : MonoBehaviour
{
    private KeyNames keyData = new KeyNames();

    public List<GameObject> carList = new List<GameObject>();

    private int m_index;

    public GameObject optionsMenu;
    public GameObject mainMenu;

    public Slider volumeSlider;

    public GameObject playButton;
    public GameObject selectButton;


    private void Start()
    {
        if (!PlayerPrefs.HasKey(keyData.selectedKey)) {
            PlayerPrefs.SetInt(keyData.selectedKey, 0);
        }
        m_index = PlayerPrefs.GetInt(keyData.selectedKey);

        carList[m_index].SetActive(true);
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }
    public void OptionsButton()
    {
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void ExitOptionsButton()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
        Singleton.Instance.Volume = volumeSlider.value;
    }
    public void SelectButton()
    {
        playButton.SetActive(true);
        selectButton.SetActive(false);
        PlayerPrefs.SetInt(keyData.selectedKey, m_index);
    }
    public void NextButton()
    {
        carList[m_index].SetActive(false);
        m_index++;
        if (m_index > carList.Count - 1) {
            m_index = 0;
        }
        SelectButtonCheck();
        carList[m_index].SetActive(true);
    }
    public void PreviousButton()
    {
        carList[m_index].SetActive(false);
        m_index--;
        if (m_index < 0) {
            m_index = carList.Count - 1;
        }
        SelectButtonCheck();
        carList[m_index].SetActive(true);
    }
    private void SelectButtonCheck()
    {
        int selectedCarIndex = PlayerPrefs.GetInt(keyData.selectedKey);
        if (m_index == selectedCarIndex) {
            playButton.SetActive(true);
            selectButton.SetActive(false);
        }
        else if (m_index != selectedCarIndex) {
            playButton.SetActive(false);
            selectButton.SetActive(true);
        }
    }
}
[System.Serializable]
public class KeyNames
{
    public string selectedKey = "SelectedIndex";
}

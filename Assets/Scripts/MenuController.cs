using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering;

public class MenuController : MonoBehaviour
{
    //Sound Settings Menu Control
    [Header("Volume Settings")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private GameObject ComfirmationPrompt = null;
    [SerializeField] private float defaultVolume = 1.0f;


    //GamePlay Settings Menu Control
    [Header("GamePlay Settings")]
    [SerializeField] private Text controllerSenTextValue = null;
    [SerializeField] private Slider controllerSenSlider = null;
    [SerializeField] private int defaultSen = 4;

    //--------no need-----------
    [Header("Levels To Load")]
    public string _newGameLevel;
    private string levelToLoad;
    [SerializeField] private GameObject noSavedDialog = null;
    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(_newGameLevel);
    }


    public void LoadGameDialogYes() {
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            levelToLoad = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(levelToLoad);
        }
        else 
        {
            noSavedDialog.SetActive(true);
        }
    }


    //----------------Main Menu - Quit Button(Exit from the Game)-----------------------------------------------------------------------------
    public void ExitButton()
    {
        Application.Quit();
    }

    //Scene Transitions
    //-------------------Scene Transition - Meditation---------------------------------------------------------------------------------------
    public float fadeDuration = 1f; // ����������������� ���������
    //public int sceneIndex = 3; // ������ ������� ����� � build settings

    public void LoadThirdScene()
    {
        StartCoroutine(Transition());
    }

    IEnumerator Transition()
    {
        // �������� ��������� CanvasGroup ��� ���������� ������������� �������
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        // ������ ��������� �������� ������������ (1 - ��������� ������������)
        canvasGroup.alpha = 1;

        // ������ ��������� ������������ �� 0 �� fadeDuration ������
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / fadeDuration;
            yield return null;
        }

        // ����� ����, ��� ������������ �������� 0, ��������� ������ �����
        SceneManager.LoadScene(2);
    }

    //-----------------------Transition - Logic Game---------------------------------------------


    public float fadeDuration2 = 1f; // ����������������� ���������
    public int sceneIndex2 = 1; // ������ ������ ����� � build settings

    public void LoadFirstScene()
    {
        StartCoroutine(Transition2());
    }

    IEnumerator Transition2()
    {
        // �������� ��������� CanvasGroup ��� ���������� ������������� �������
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        // ������ ��������� �������� ������������ (1 - ��������� ������������)
        canvasGroup.alpha = 1;

        // ������ ��������� ������������ �� 0 �� fadeDuration ������
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / fadeDuration;
            yield return null;
        }

        // ����� ����, ��� ������������ �������� 0, ��������� ������ �����
        SceneManager.LoadScene(sceneIndex2);
    }

    //---------------Volume Settings--------------------------------
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text =volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        //Show Prompt
        StartCoroutine(ComfirmationBox());
    }

    public void ResetButton(string MenuType)
    {
        if (MenuType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");
            VolumeApply();
        }
    }

    public IEnumerator ComfirmationBox()
    {
        ComfirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        ComfirmationPrompt.SetActive(false);
    }


    //--------------GamePLay Settings----------------------




}

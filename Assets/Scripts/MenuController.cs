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
    [SerializeField] private TMP_Text controllerSenTextValue = null;
    [SerializeField] private Slider controllerSenSlider = null;
    [SerializeField] private int defaultSen = 4;
    public int mainControllerSen = 4;

    //Toggle Settings (PlayGame Container)
    [Header("Toggle Settings")]
    [SerializeField] private Toggle invertYToggle = null;

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
    public float fadeDuration = 1f; // Продолжительность затухания
    //public int sceneIndex = 3; // Индекс третьей сцены в build settings

    public void LoadThirdScene()
    {
        StartCoroutine(Transition());
    }

    IEnumerator Transition()
    {
        // Получаем компонент CanvasGroup для управления прозрачностью объекта
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        // Задаем начальное значение прозрачности (1 - полностью непрозрачный)
        canvasGroup.alpha = 1;

        // Плавно уменьшаем прозрачность до 0 за fadeDuration секунд
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / fadeDuration;
            yield return null;
        }

        // После того, как прозрачность достигла 0, загружаем третью сцену
        SceneManager.LoadScene(2);
    }

    //-----------------------Transition - Logic Game---------------------------------------------


    public float fadeDuration2 = 1f; // Продолжительность затухания
    //public int sceneIndex2 = 1; // Индекс первой сцены в build settings

    public void LoadFirstScene()
    {
        StartCoroutine(Transition2());
    }

    IEnumerator Transition2()
    {
        // Получаем компонент CanvasGroup для управления прозрачностью объекта
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        // Задаем начальное значение прозрачности (1 - полностью непрозрачный)
        canvasGroup.alpha = 1;

        // Плавно уменьшаем прозрачность до 0 за fadeDuration секунд
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / fadeDuration;
            yield return null;
        }

        // После того, как прозрачность достигла 0, загружаем первую сцену
        SceneManager.LoadScene(1);
    }


    //-----------------------Transition - QUIT BUTTON MEDITATION---------------------------------------------


    public float fadeDuration3 = 1f; // Продолжительность затухания
    //public int sceneIndex2 = 1; // Индекс первой сцены в build settings

    public void LoadZeroScene()
    {
        StartCoroutine(Transition3());
    }

    IEnumerator Transition3()
    {
        // Получаем компонент CanvasGroup для управления прозрачностью объекта
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        // Задаем начальное значение прозрачности (1 - полностью непрозрачный)
        canvasGroup.alpha = 1;

        // Плавно уменьшаем прозрачность до 0 за fadeDuration секунд
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / fadeDuration;
            yield return null;
        }

        // После того, как прозрачность достигла 0, загружаем первую сцену
        SceneManager.LoadScene(0);
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
        //reset for gameplay
        if (MenuType == "Gameplay")
        {
            controllerSenTextValue.text = defaultSen.ToString("0");
            controllerSenSlider.value = defaultSen;
            mainControllerSen = defaultSen;
            invertYToggle.isOn = false;
            GameplayApply();
        }
    }

    public IEnumerator ComfirmationBox()
    {
        ComfirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        ComfirmationPrompt.SetActive(false);
    }


    //--------------GamePLay Settings----------------------

    //Controller Sensitivity
    public void SetControllerSen(float sensitivity)
    {
        mainControllerSen = Mathf.RoundToInt(sensitivity);
        controllerSenTextValue.text = sensitivity.ToString("0");
    }

    //GameplayApply
    public void GameplayApply()
    {
        if (invertYToggle.isOn)
        {
            PlayerPrefs.SetInt("masterInvertY", 1);
            //invert Y
        }
        else
        {
            PlayerPrefs.SetInt("masterInvertY", 0);
            //not invert Y
        }
        PlayerPrefs.SetFloat("masterSen", mainControllerSen);
        StartCoroutine (ComfirmationBox());
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// --------------------Scene Transition Quit_To_The_Start Menu Game --------------------------------------

public class SceneSwitcher : MonoBehaviour
{
    // Название сцены, на которую нужно перейти
    public string sceneToLoad;

    void Start()
    {
        // Убедитесь, что у TextMeshPro есть компонент Collider
        if (GetComponent<Collider>() == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }
    }

    void OnMouseDown()
    {
        // Проверяем, установлен ли sceneToLoad
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            // Переход на указанную сцену
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("Scene name to load is not set.");
        }
    }
}
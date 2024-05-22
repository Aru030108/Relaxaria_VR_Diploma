using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// --------------------Scene Transition Quit_To_The_Start Menu Game --------------------------------------

public class SceneSwitcher : MonoBehaviour
{
    // �������� �����, �� ������� ����� �������
    public string sceneToLoad;

    void Start()
    {
        // ���������, ��� � TextMeshPro ���� ��������� Collider
        if (GetComponent<Collider>() == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }
    }

    void OnMouseDown()
    {
        // ���������, ���������� �� sceneToLoad
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            // ������� �� ��������� �����
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("Scene name to load is not set.");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPlacer : MonoBehaviour
{
    public GameObject[] largeObjects; // ������ ��� ������� �������� (����)
    public GameObject[] mediumObjects; // ������ ��� ������� �������� (������)
    public GameObject[] smallObjects; // ������ ��� ��������� �������� (������)

    public Transform[] positions; // ������ ��� �������, ���� ����� ��������� �������
    public Text feedbackText; // UI ������� ��� ����������� ���������
    public Text congratsText; // UI ������� ��� ����������� ������������

    private int correctPlacements = 0; // ������� ���������� ����������

    void Start()
    {
        PlaceObjects();
        congratsText.gameObject.SetActive(false);
    }

    void PlaceObjects()
    {
        int totalObjects = largeObjects.Length + mediumObjects.Length + smallObjects.Length;
        if (positions.Length < totalObjects)
        {
            Debug.LogError("������������ ������� ��� ���������� ���� ��������!");
            return;
        }

        int index = 0;

        // ��������� ������� ������� (����)
        foreach (GameObject obj in largeObjects)
        {
            Instantiate(obj, positions[index].position, Quaternion.identity);
            index++;
        }

        // ��������� ������� ������� (������)
        foreach (GameObject obj in mediumObjects)
        {
            Instantiate(obj, positions[index].position, Quaternion.identity);
            index++;
        }

        // ��������� ��������� ������� (������)
        foreach (GameObject obj in smallObjects)
        {
            Instantiate(obj, positions[index].position, Quaternion.identity);
            index++;
        }
    }

    public void CheckPlacement(GameObject placedObject, Transform targetPosition)
    {
        // �������� ������������ �������
        if (Vector3.Distance(placedObject.transform.position, targetPosition.position) < 0.5f)
        {
            correctPlacements++;
            feedbackText.text = "Right!";
            Destroy(placedObject); // ������� ������ ����� ����������� ����������
        }
        else
        {
            feedbackText.text = "That`s wrong!";
        }

        // �������� ���������� ����
        if (correctPlacements == largeObjects.Length + mediumObjects.Length + smallObjects.Length)
        {
            congratsText.gameObject.SetActive(true);
            feedbackText.text = ""; // ������� ���������� ���� ��� ���������
        }
    }
}

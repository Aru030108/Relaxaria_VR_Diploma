using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPlacer : MonoBehaviour
{
    public GameObject[] largeObjects; // ������ ��� ������� �������� (����)
    public GameObject[] mediumObjects; // ������ ��� ������� �������� (������)
    public GameObject[] smallObjects; // ������ ��� ��������� �������� (������)

    public Transform[] positions; // ������ ��� �������, ���� ����� ��������� �������

    void Start()
    {
        PlaceObjects();
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPlacer : MonoBehaviour
{
    public GameObject[] largeObjects; // Массив для больших объектов (кубы)
    public GameObject[] mediumObjects; // Массив для средних объектов (конусы)
    public GameObject[] smallObjects; // Массив для маленьких объектов (кольца)

    public Transform[] positions; // Массив для позиций, куда будут размещены объекты

    void Start()
    {
        PlaceObjects();
    }

    void PlaceObjects()
    {
        int totalObjects = largeObjects.Length + mediumObjects.Length + smallObjects.Length;
        if (positions.Length < totalObjects)
        {
            Debug.LogError("Недостаточно позиций для размещения всех объектов!");
            return;
        }

        int index = 0;

        // Размещаем большие объекты (кубы)
        foreach (GameObject obj in largeObjects)
        {
            Instantiate(obj, positions[index].position, Quaternion.identity);
            index++;
        }

        // Размещаем средние объекты (конусы)
        foreach (GameObject obj in mediumObjects)
        {
            Instantiate(obj, positions[index].position, Quaternion.identity);
            index++;
        }

        // Размещаем маленькие объекты (кольца)
        foreach (GameObject obj in smallObjects)
        {
            Instantiate(obj, positions[index].position, Quaternion.identity);
            index++;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPlacer : MonoBehaviour
{
    public GameObject[] largeObjects; // Массив для больших объектов (кубы)
    public GameObject[] mediumObjects; // Массив для средних объектов (конусы)
    public GameObject[] smallObjects; // Массив для маленьких объектов (кольца)

    public Transform[] positions; // Массив для позиций, куда будут размещены объекты
    public Text feedbackText; // UI элемент для отображения сообщений
    public Text congratsText; // UI элемент для отображения поздравлений

    private int correctPlacements = 0; // Счетчик правильных размещений

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

    public void CheckPlacement(GameObject placedObject, Transform targetPosition)
    {
        // Проверка правильности позиции
        if (Vector3.Distance(placedObject.transform.position, targetPosition.position) < 0.5f)
        {
            correctPlacements++;
            feedbackText.text = "Right!";
            Destroy(placedObject); // Убираем объект после правильного размещения
        }
        else
        {
            feedbackText.text = "That`s wrong!";
        }

        // Проверка завершения игры
        if (correctPlacements == largeObjects.Length + mediumObjects.Length + smallObjects.Length)
        {
            congratsText.gameObject.SetActive(true);
            feedbackText.text = ""; // Очистка текстового поля для сообщений
        }
    }
}

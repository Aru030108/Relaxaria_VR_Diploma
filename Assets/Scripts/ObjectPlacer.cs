using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPlacer : MonoBehaviour
{
    public GameObject[] largeObjects; // Array for large objects (cubes)
    public GameObject[] mediumObjects; // Array for medium objects (cones)
    public GameObject[] smallObjects; // Array for small objects (rings)

    public Transform[] positions; // Array for the positions where the objects will be placed
    public Text feedbackText; // UI element for displaying messages
    public Text congratsText; 

    private int correctPlacements = 0; // The counter of correct placements

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
            Debug.LogError("There are not enough positions to place all the objects!");
            return;
        }

        int index = 0;

        // Placing large objects (cubes)
        foreach (GameObject obj in largeObjects)
        {
            Instantiate(obj, positions[index].position, Quaternion.identity);
            index++;
        }

        // Placing medium objects (cones)
        foreach (GameObject obj in mediumObjects)
        {
            Instantiate(obj, positions[index].position, Quaternion.identity);
            index++;
        }

        // Placing small objects (rings)
        foreach (GameObject obj in smallObjects)
        {
            Instantiate(obj, positions[index].position, Quaternion.identity);
            index++;
        }
    }

    public void CheckPlacement(GameObject placedObject, Transform targetPosition)
    {
        // --------------------------Checking the correctness of the position------------------------------------
        if (Vector3.Distance(placedObject.transform.position, targetPosition.position) < 0.5f)
        {
            correctPlacements++;
            feedbackText.text = "Right!";
            Destroy(placedObject); // We remove the object after proper placement
        }
        else
        {
            feedbackText.text = "That`s wrong!";
        }

        // ---------------Checking the completion of the game-----------------------------------------------
        if (correctPlacements == largeObjects.Length + mediumObjects.Length + smallObjects.Length)
        {
            congratsText.gameObject.SetActive(true);
            feedbackText.text = ""; // Clearing the text field for messages
        }
    }
}

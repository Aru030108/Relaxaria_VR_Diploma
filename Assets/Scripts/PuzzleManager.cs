using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleManager : MonoBehaviour
{
    public GameObject puzzlePiecePrefab; // Prefab cube
    public int puzzleSize = 4; 
    public float pieceSize = 1.0f; // The size of one piece of the puzzle
    public TextMeshPro congratsText; 

    private List<GameObject> puzzlePieces = new List<GameObject>();
    private Vector3[] correctPositions;

    void Start()
    {
        congratsText.gameObject.SetActive(false); // Hide the text of the greeting at the beginning
        CreatePuzzle();
    }

    void CreatePuzzle()
    {
        correctPositions = new Vector3[puzzleSize * puzzleSize];
        int index = 0;
        for (int y = 0; y < puzzleSize; y++)
        {
            for (int x = 0; x < puzzleSize; x++)
            {
                // Create a new piece of the puzzle
                GameObject piece = Instantiate(puzzlePiecePrefab, new Vector3(x * pieceSize, y * pieceSize, 0), Quaternion.identity);
                piece.name = $"PuzzlePiece_{x}_{y}";
                puzzlePieces.Add(piece);

                // Save the correct position
                correctPositions[index] = piece.transform.position;
                index++;

                // Apply texture to a piece of the puzzle
                Material pieceMaterial = new Material(Shader.Find("Unlit/Texture"));
                pieceMaterial.mainTexture = Resources.Load<Texture2D>($"PuzzleImages/part_{y * puzzleSize + x}");

                // Apply the material to the cube
                Renderer renderer = piece.GetComponent<Renderer>();
                renderer.material = pieceMaterial;

                // Add a drag and drop component
                piece.AddComponent<PuzzlePiece>().manager = this;
            }
        }

        
        ShufflePuzzle();
    }

    void ShufflePuzzle()
    {
        foreach (var piece in puzzlePieces)
        {
            piece.transform.position = new Vector3(Random.Range(0, puzzleSize) * pieceSize, Random.Range(0, puzzleSize) * pieceSize, 0);
        }
    }

    public void CheckPuzzle()
    {
        bool isComplete = true;
        for (int i = 0; i < puzzlePieces.Count; i++)
        {
            if (Vector3.Distance(puzzlePieces[i].transform.position, correctPositions[i]) > 0.1f)
            {
                isComplete = false;
                break;
            }
        }

        if (isComplete)
        {
            congratsText.gameObject.SetActive(true); 
        }
    }
}

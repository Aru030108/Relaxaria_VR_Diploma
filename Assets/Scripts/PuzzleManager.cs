using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    public GameObject puzzlePiecePrefab; // ������ ����
    public int puzzleSize = 4; // ������ ����� (��������, 4x4)
    public float pieceSize = 1.0f; // ������ ����� ����� �����
    public Text congratsText; // ����� ������������

    private List<GameObject> puzzlePieces = new List<GameObject>();
    private Vector3[] correctPositions;

    void Start()
    {
        congratsText.gameObject.SetActive(false); // ������ ����� ������������ � ������
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
                // �������� ����� ����� �����
                GameObject piece = Instantiate(puzzlePiecePrefab, new Vector3(x * pieceSize, y * pieceSize, 0), Quaternion.identity);
                piece.name = $"PuzzlePiece_{x}_{y}";
                puzzlePieces.Add(piece);

                // ��������� ���������� �������
                correctPositions[index] = piece.transform.position;
                index++;

                // ��������� �������� � ����� �����
                Material pieceMaterial = new Material(Shader.Find("Unlit/Texture"));
                pieceMaterial.mainTexture = Resources.Load<Texture2D>($"PuzzleImages/part_{y * puzzleSize + x}");

                // ��������� �������� � ����
                Renderer renderer = piece.GetComponent<Renderer>();
                renderer.material = pieceMaterial;

                // �������� ��������� ��� ��������������
                piece.AddComponent<PuzzlePiece>().manager = this;
            }
        }

        // ����������� ����
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
            congratsText.gameObject.SetActive(true); // �������� ����� ������������
        }
    }
}

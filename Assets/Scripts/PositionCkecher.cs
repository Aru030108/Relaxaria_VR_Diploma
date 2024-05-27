    using UnityEngine;

public class PositionChecker : MonoBehaviour
{
    // Целевая позиция для проверки
    public Vector3 targetPosition = new Vector3(-12.08f, 0f, -12.44f);
    // Допустимое отклонение от целевой позиции
    public float tolerance = 0.1f;

    void Update()
    {
        // Проверка позиции объекта
        if (IsAtTargetPosition())
        {
            Debug.Log("Correct");
        }
        else
        {
            Debug.Log("Wrong");
        }
    }

    // Функция для проверки позиции объекта
    private bool IsAtTargetPosition()
    {
        return Vector3.Distance(transform.position, targetPosition) <= tolerance;
    }
}

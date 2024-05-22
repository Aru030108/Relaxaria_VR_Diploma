using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class BreathingExercise : MonoBehaviour
{
    public float inhaleDuration = 4.0f;
    public float holdDuration = 4.0f;
    public float exhaleDuration = 4.0f;
    public float minScale = 0.5f;
    public float maxScale = 2.0f;

    public Button startStopButton;
    public Renderer sphereRenderer;
    public Transform sphereTransform;
    public TextMesh sphereText;
    public Slider progressBar; // Прогресс-бар
    public AudioSource backgroundMusic; // Фоновая музыка
    public AudioSource[] phaseSounds; // Звуки для каждой фазы

    private float timer = 0.0f;
    private enum BreathingState { Inhale, Hold, Exhale }
    private BreathingState currentState;

    private bool isBreathing = false;

    void Start()
    {
        currentState = BreathingState.Inhale;
        timer = 0.0f;
        startStopButton.onClick.AddListener(ToggleBreathing);
        UpdateSphereColor();
        UpdateSphereText("Press the button to start");

        // Настройка прогресс-бара
        if (progressBar != null)
        {
            progressBar.maxValue = inhaleDuration + holdDuration + exhaleDuration;
        }
    }

    void Update()
    {
        if (isBreathing)
        {
            timer += Time.deltaTime;

            // Обновление прогресс-бара
            if (progressBar != null)
            {
                progressBar.value = timer % progressBar.maxValue;
            }

            switch (currentState)
            {
                case BreathingState.Inhale:
                    ScaleSphere(minScale, maxScale, inhaleDuration);
                    if (timer >= inhaleDuration)
                    {
                        currentState = BreathingState.Hold;
                        timer = 0.0f;
                        UpdateSphereColor();
                        UpdateSphereText("Hold your breath");
                        PlayPhaseSound(1); // Звук для задержки дыхания
                    }
                    break;
                case BreathingState.Hold:
                    if (timer >= holdDuration)
                    {
                        currentState = BreathingState.Exhale;
                        timer = 0.0f;
                        UpdateSphereColor();
                        UpdateSphereText("Exhale");
                        PlayPhaseSound(2); // Звук для выдоха
                    }
                    break;
                case BreathingState.Exhale:
                    ScaleSphere(maxScale, minScale, exhaleDuration);
                    if (timer >= exhaleDuration)
                    {
                        currentState = BreathingState.Inhale;
                        timer = 0.0f;
                        UpdateSphereColor();
                        UpdateSphereText("Inhale");
                        PlayPhaseSound(0); // Звук для вдоха
                    }
                    break;
            }
        }
    }

    void ScaleSphere(float startScale, float endScale, float duration)
    {
        float t = timer / duration;
        float scale = Mathf.Lerp(startScale, endScale, t);
        sphereTransform.localScale = new Vector3(scale, scale, scale);
    }

    void UpdateSphereColor()
    {
        Color color = Color.white;
        switch (currentState)
        {
            case BreathingState.Inhale:
                color = Color.green;
                break;
            case BreathingState.Hold:
                color = Color.yellow;
                break;
            case BreathingState.Exhale:
                color = Color.blue;
                break;
        }
        sphereRenderer.material.color = color;
    }

    void UpdateSphereText(string text)
    {
        if (sphereText != null)
        {
            sphereText.text = text;
        }
    }

    void PlayPhaseSound(int index)
    {
        if (phaseSounds != null && index < phaseSounds.Length)
        {
            phaseSounds[index].Play();
        }
    }

    public void StartBreathing()
    {
        isBreathing = true;
        timer = 0.0f;
        currentState = BreathingState.Inhale;
        UpdateSphereColor();
        UpdateSphereText("Inhale");

        // Воспроизведение фоновой музыки
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }

        PlayPhaseSound(0); // Звук для вдоха
    }

    public void StopBreathing()
    {
        isBreathing = false;
        sphereTransform.localScale = new Vector3(minScale, minScale, minScale);
        UpdateSphereText("Press the button to start");

        // Остановка фоновой музыки
        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
        }
    }

    public void ToggleBreathing()
    {
        if (isBreathing)
        {
            StopBreathing();
        }
        else
        {
            StartBreathing();
        }
    }
}
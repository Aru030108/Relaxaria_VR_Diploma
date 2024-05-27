using UnityEngine;
using UnityEngine.UI;

public class StartMenuGamesTransitions : MonoBehaviour
{
    [Header("Main Menu Buttons")]
    public Button meditationButton;
    public Button logicGameButton;
    public Button puzzleGameButton;
    public Button quitButton;

    // Start is called before the first frame update
    void Start()
    {
        // Hook events
        meditationButton.onClick.AddListener(GoToMeditation);
        logicGameButton.onClick.AddListener(GoToLogicGame);
        puzzleGameButton.onClick.AddListener(GoToPuzzleGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    public void GoToMeditation()
    {
        SceneNavigator.singleton.GoToSceneAsync(5);
    }

    public void GoToLogicGame()
    {
        SceneNavigator.singleton.GoToSceneAsync(1);
    }

    public void GoToPuzzleGame()
    {
        SceneNavigator.singleton.GoToSceneAsync(4);
    }

    public void QuitGame()
    {
        SceneNavigator.singleton.GoToSceneAsync(0);
    }
}

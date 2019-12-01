using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button startGameButton;
    [SerializeField] Button endGameButton;

    public string levelName;

    private void Start()
    {
        startGameButton.onClick.AddListener(() => {
            StartGame(levelName);
        });

        endGameButton.onClick.AddListener(QuitGame);
    }

    void StartGame(string name)
    {
        SceneManager.LoadScene(name);
    }

    void QuitGame()
    {
        Application.Quit();
    }
}

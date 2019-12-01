using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour
{
    [SerializeField] GameObject winMenuPanel;
    [SerializeField] Button backButton;

    private void Start()
    {
        winMenuPanel.SetActive(false);

        GameManager.instance.EventBus.AddListener("OnAllEnemyKilled", () =>
        {
            GameManager.instance.Timer.Add(() =>
            {
                GameManager.instance.IsPaused = true;
                winMenuPanel.SetActive(true);
            }, 4);
            
        });

        backButton.onClick.AddListener(OnBackButtonClick);
    }

    void OnBackButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

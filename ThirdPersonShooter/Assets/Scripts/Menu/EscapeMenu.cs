using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscapeMenu : MonoBehaviour
{
    [SerializeField] GameObject escapeMenuPanel;
    [SerializeField] Button yesButton;
    [SerializeField] Button noButton;

    private void Awake()
    {
        escapeMenuPanel.SetActive(false);
        yesButton.onClick.AddListener(OnYesButtonClick);
        noButton.onClick.AddListener(OnNoButtonClick);
    }

    void OnYesButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void OnNoButtonClick()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        GameManager.instance.IsPaused = false;
        escapeMenuPanel.SetActive(false);
    }

    private void Update()
    {
        if (escapeMenuPanel.activeSelf)
            return;

        if (GameManager.instance.GetInputController.escape)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;

            GameManager.instance.IsPaused = true;
            escapeMenuPanel.SetActive(true);
        }
    }
}

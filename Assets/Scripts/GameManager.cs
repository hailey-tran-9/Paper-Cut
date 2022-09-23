using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public GameObject tutorialGO;
    static GameObject tutorial;
    bool tutorialOpen;

    #region Unity_functions
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);

        Debug.Log(SceneManager.GetActiveScene().name);

        if (SceneManager.GetActiveScene().name == "InGame") {
            tutorial = Instantiate(tutorialGO);
            tutorialOpen = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("escape") && tutorialOpen == true) {
            tutorial.SetActive(false);
            tutorialOpen = false;
        } else if (Input.GetKeyDown("escape") && tutorialOpen == false) {
            tutorial.SetActive(true);
            tutorialOpen = true;
        }
    }
    #endregion

    #region Scene_transitions
    public void StartGame() {
        SceneManager.LoadScene("StartScene");
    }
    public void LoseGame() {
        SceneManager.LoadScene("LoseScene");
    }
    public void WinGame() {
        SceneManager.LoadScene("WinScene");
    }
    public void MainMenu() {
        SceneManager.LoadScene("InGame");
    }
    public void QuitGame() {
        Application.Quit();
    }
    #endregion
}

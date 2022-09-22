using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    #region GameManager_variables
    public GameObject gc;
    GameManager gm;
    #endregion

    #region Button_functions
    public void CallStartGame() {
        gm.StartGame();
    }
    public void CallLoseGame() {
        gm.LoseGame();
    }
    public void CallWinGame() {
        gm.WinGame();
    }
    public void CallMainMenu() {
        gm.MainMenu();
    }
    public void CallQuitGame() {
        gm.QuitGame();
    }
    #endregion

    #region Unity_functions
    void Awake()
    {
        gm = gc.GetComponent<GameManager>();
    }
    #endregion
}

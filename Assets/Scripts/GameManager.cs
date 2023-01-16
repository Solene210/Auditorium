using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Expose

    [SerializeField]
    GameObject _victoryUI;

    #endregion

    #region Unity Lyfecycle
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    #endregion

    #region Methods
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void MainMenue()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Quit()
    {
        Application.Quit();
    }
        #endregion

        #region Private & Protected

        #endregion
    }

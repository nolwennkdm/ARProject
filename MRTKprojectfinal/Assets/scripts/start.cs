using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public GameObject locked;
    
    // Start is called before the first frame update
    public void Niveau1() {
        if (win.Instance != null)
        {
            win.Instance.panel.SetActive(false);
            win.Instance.isCompleted = false;
            win.Instance.scoreMax = 100000;

        }
        SceneManager.LoadScene("Niveau1");

    }
    public void Niveau2()

    {
        List<int> hightScores = scoresMan.Instance.GetHighScores();
        if (hightScores[0] !=0)
            {
                SceneManager.LoadScene("level2");
            }
        
        else
        {

            locked.SetActive(true);

        }

    }
    public void Niveau1Bike()
    {
    
        SceneManager.LoadScene("Niveau1bike");

    }
    public void Niveau2Bike()
    {
       

    }
    public void Quit()
    {

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif


    }
    public void Menu()
    {
        if (win.Instance != null)
        {
            win.Instance.panel.SetActive(false);
            win.Instance.isCompleted=false;
            win.Instance.scoreMax = 100000;

        }
        SceneManager.LoadScene("start");

    }

}

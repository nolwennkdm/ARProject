using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Search;
using static Microsoft.MixedReality.GraphicsTools.MeshInstancer;

public class winb : MonoBehaviour
{
    public static winb Instance;
    public GameObject panel;
    public float startTime;
    public bool isCompleted = false;
    
    public TMP_Text tscore;
    public TMP_Text bravo;
    private int score;
    public int scoreMax = 100000;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance!=this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        scoreMax = 100000;
        isCompleted = false;
        panel.SetActive(false);
        startLevel();
    }
    public void startLevel()
    {
        isCompleted = false;
        startTime = Time.time;
    }
    public void CompleteGame()
    {
        if (isCompleted) return;
        isCompleted = true;
       
        float timeTaken = Time.time - startTime;
        score = Mathf.RoundToInt(scoreMax/ timeTaken);
        scoresManb.Instance.SaveScore(score);
        winDialog(score);
    }

    public void winDialog(int score)
    {
        panel.SetActive(true);
        tscore.text = "Score: " + score.ToString();
        bravo.text = "Bravo! Vous etes vivant.";
      
    }
    public void modifyMax(int amount)
    {
        scoreMax += amount;
    }

}

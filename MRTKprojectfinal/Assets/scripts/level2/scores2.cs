using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class scoresMan2 : MonoBehaviour
{   
    public static scoresMan2 Instance;
    private List<int> highScores = new List<int>();   
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadScores();
       }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    public void SaveScore(int newScore)
    {
        highScores.Add(newScore);
        highScores = highScores.OrderByDescending(s=>s).Take(3).ToList();
        for (int i = 0; i < highScores.Count; i++)
        {
            PlayerPrefs.SetInt("HighScore" + i, highScores[i]);

        }
        PlayerPrefs.Save();
    }
    private void LoadScores()
    {
        highScores.Clear();
        for(int i = 0; i < 3; i++)
        {
            if (PlayerPrefs.HasKey("HighScore" + i ))
            {
                highScores.Add(PlayerPrefs.GetInt("HighScore:"+i));
            }
        }
    }
    public List<int> GetHighScores()
    {
        return highScores;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class disp2 : MonoBehaviour
{
    public TextMeshProUGUI scoredisp;

    // Start is called before the first frame update
    void Start()
    {
        List<int> hightScores = scoresMan2.Instance.GetHighScores();
        scoredisp.text = "Meilleurs Scores:\n";
        for (int i =0; i< hightScores.Count; i++)
        {
            scoredisp.text += (i + 1) + "." + hightScores[i] + "\n";
        }
    }

  
}

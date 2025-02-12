using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class button : MonoBehaviour
{
    public GameObject player;
    public move playerMovement;
    public win win;
    // Start is called before the first frame update
    public void onClickForward()
    {
        if (playerMovement.dead || win.isCompleted)
        {
            return; // Ne rien faire si le personnage est mort
        }
        move moveScript = player.GetComponent<move>();
        StartCoroutine(moveScript.Moveforward());
    }
    public void onClickBackwards()
    {
        if (playerMovement.dead || win.isCompleted)
        {
            return; // Ne rien faire si le personnage est mort
        }
        move moveScript = player.GetComponent<move>();
        StartCoroutine(moveScript.MoveBackwards());
    }
    public void onClickRight()
    {
        if (playerMovement.dead || win.isCompleted)
        {
            return; // Ne rien faire si le personnage est mort
        }
        move moveScript = player.GetComponent<move>();
        StartCoroutine(moveScript.MoveRight());
    }
    public void onClickLeft()
    {
        if (playerMovement.dead || win.isCompleted)
        {
            return; // Ne rien faire si le personnage est mort
        }
        move moveScript = player.GetComponent<move>();
        StartCoroutine(moveScript.MoveLeft());
    }
}

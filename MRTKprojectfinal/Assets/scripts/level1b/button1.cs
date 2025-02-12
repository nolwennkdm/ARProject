using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonbike : MonoBehaviour
{
    public movebike playerMovement;
    public GameObject player;
    public winb win;
    // Start is called before the first frame update
    public void onClickForward()
    {
        if (playerMovement.dead || win.isCompleted)
        {
            return; // Ne rien faire si le personnage est mort
        }
        movebike moveScript = player.GetComponent<movebike>();
        StartCoroutine(moveScript.Moveforward());
    }
    public void onClickBackwards()
    {
        if (playerMovement.dead || win.isCompleted)
        {
            return; // Ne rien faire si le personnage est mort
        }
        movebike moveScript = player.GetComponent<movebike>();
        StartCoroutine(moveScript.MoveBackwards());
    }
    public void onClickRight()
    {
        if (playerMovement.dead || win.isCompleted)
        {
            return; // Ne rien faire si le personnage est mort
        }
        movebike moveScript = player.GetComponent<movebike>();
        StartCoroutine(moveScript.MoveRight());
    }
    public void onClickLeft()
    {
        if (playerMovement.dead || win.isCompleted)
        {
            return; // Ne rien faire si le personnage est mort
        }
        movebike moveScript = player.GetComponent<movebike>();
        StartCoroutine(moveScript.MoveLeft());
    }
}

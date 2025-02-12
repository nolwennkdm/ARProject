using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class button2 : MonoBehaviour
{
    public GameObject player;
    public GridMove playerMovement2;
    public win2 win2;
    // Start is called before the first frame update
    public void onClickForward()
    {
        if (playerMovement2.dead || win2.isCompleted)
        {
            return; // Ne rien faire si le personnage est mort
        }
        GridMove GridMoveScript = player.GetComponent<GridMove>();
        StartCoroutine(GridMoveScript.Moveforward());
    }
    public void onClickBackwards()
    {
        if (playerMovement2.dead || win2.isCompleted)
        {
            return; // Ne rien faire si le personnage est mort
        }
        GridMove GridMoveScript = player.GetComponent<GridMove>();
        StartCoroutine(GridMoveScript.MoveBackwards());
    }
    public void onClickRight()
    {
        if (playerMovement2.dead || win2.isCompleted)
        {
            return; // Ne rien faire si le personnage est mort
        }
        GridMove GridMoveScript = player.GetComponent<GridMove>();
        StartCoroutine(GridMoveScript.MoveRight());
    }
    public void onClickLeft()
    {
        if (playerMovement2.dead || win2.isCompleted)
        {
            return; // Ne rien faire si le personnage est mort
        }
        GridMove GridMoveScript = player.GetComponent<GridMove>();
        StartCoroutine(GridMoveScript.MoveLeft());
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using TMPro;

public class GridMove : MonoBehaviour
{
    public GameObject panel;
    public int gridSizeX = 13;
    public int gridSizeY = 21;
    private Vector2Int playerPositionInit = new Vector2Int(6, 2);
    public GameObject Lamp1;
    public GameObject Lamp2;
    public GameObject Car;
    public GameObject Factory;
    public GameObject Pallet1;
    public GameObject Stop;
    public GameObject Brique1;
    public GameObject Brique2;
    public GameObject Trash;
    public GameObject Bus;
    public GameObject Road;
    public GameObject Barriere;
    public GameObject Antenna;
    public GameObject Signal;
    public GameObject Pallet2;
    public TMP_Text textelose;
    public TMP_Text losetexte;
    public bool dead = false;

    private int[,] grid =
    {
        { 1,1,1,1,1,1,1,1,1,1,1,1,1 },
        { 1,1,0,0,0,0,0,0,0,3,3,1,1 },
        { 1,1,0,0,2,2,2,0,0,3,3,1,1 },
        { 1,1,0,1,2,2,2,4,0,3,3,1,1 },
        { 1,1,0,0,2,2,2,0,0,3,3,1,1 },
        { 1,1,0,0,2,2,2,0,0,3,3,1,1 },
        { 1,1,0,0,0,0,0,0,0,3,3,1,1 },
        { 1,1,6,0,0,8,8,8,1,1,0,1,1 },
        { 1,1,6,0,0,0,16,0,0,0,0,1,1 },
        { 1,1,6,0,0,0,0,0,0,0,0,1,1 },
        { 1,10,10,1,1,9,9,9,0,0,0,1,1 },
        { 1,1,10,0,0,0,0,0,0,0,0,0,1 },
        { 1,10,10,0,0,0,11,11,11,12,12,12,1 },
        { 1,0,0,0,0,0,11,1,11,12,12,12,1 },
        { 1,0,0,0,0,0,11,1,11,12,12,12,1 },
        { 1,13,0,1,0,0,11,1,11,12,12,1,1 },
        { 1,13,0,0,0,0,11,11,11,12,12,1,1 },
        { 1,0,0,0,1,0,0,0,0,0,0,1,1 },
        { 1,14,14,0,0,0,15,15,15,1,0,0,1 },
        { 1,1,14,0,0,0,0,20,0,0,0,0,1 },
        { 1,1,1,1,1,1,1,1,1,1,1,1,1 }

    };
    /*2: Car || 3: Factory || 4: Pallet1 || 5:stop  ||6: Scaffolding 
    7: Sewer ||8:  Lampe1 || 9: Lampe2 || 10: Trash || 11: Bus 
    12: Road || 13: Barrer || 14 : Antenna || 15: signal || 20: win2
    16: Pallet2
     */
    private bool isValidMove(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < gridSizeX &&
               pos.y >= 0 && pos.y < gridSizeY &&
               grid[pos.y, pos.x] != 1;
    }

    private bool isDead(Vector2Int pos)
    {
        return grid[pos.y, pos.x] != 0 && grid[pos.y, pos.x] != 1
            && grid[pos.y, pos.x] != 20;
    }

    private void LogPosition(Vector2Int pos)
    {
        Debug.Log($"Position: {pos.x}, {pos.y} | Grid Value: {grid[pos.y, pos.x]}");
    }

    public IEnumerator Moveforward()
    {
        Vector3 forwardDirection = transform.forward;
        Vector2Int up = new Vector2Int(0, 1); // Vers le haut dans la grille
        Vector2Int newpos = playerPositionInit + up;

        if (isValidMove(newpos))
        {
            transform.position += forwardDirection * 0.18f;
            playerPositionInit = newpos;
            LogPosition(playerPositionInit); // Affiche la nouvelle position
            if (isDead(playerPositionInit))
            {
                dead = true;
                if (grid[playerPositionInit.y, playerPositionInit.x] == 2)
                {
                    Car.SetActive(true);
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 8)
                {
                    Lamp1.transform.Rotate(0f, 0f, -90f, Space.Self);
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 9)
                {
                    Lamp2.transform.Rotate(0f, 0f, -90f, Space.Self);
                    loseDialog();
                }



                if (grid[playerPositionInit.y, playerPositionInit.x] == 3)
                {

                    Factory.SetActive(true);
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 5)
                {
                    Stop.transform.Rotate(90f, 0f, 0f, Space.Self);
                    loseDialog();
                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 4)
                {
                    Pallet1.SetActive(false);
                    loseDialog();
                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 16)
                {
                    Pallet2.SetActive(false);
                    loseDialog();
                }


                if (grid[playerPositionInit.y, playerPositionInit.x] == 6)
                {
                    Brique1.transform.Translate(new Vector3(0f, -0.671f, 0f));
                    Brique2.transform.Translate(new Vector3(0f, -0.671f, 0f));
                    loseDialog();

                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 10)
                {
                    Trash.SetActive(true);
                    loseDialog();
                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 11)
                {
                    Bus.SetActive(true);
                    loseDialog();
                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 12)
                {
                    Road.transform.Translate(new Vector3(-0.805f, 0f, 0F));
                    loseDialog();
                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 13)
                {
                    Barriere.transform.Rotate(90f, 0f, 0f);
                    loseDialog();
                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 14)
                {
                    Antenna.SetActive(true);
                    loseDialog();
                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 15)
                {
                    Signal.transform.Rotate(0f, 0f, 90f);
                    loseDialog();
                }
            }
            if (grid[playerPositionInit.y, playerPositionInit.x] == 20)
                {
                if (win2.Instance != null)
                    {
                        win2.Instance.CompleteGame();
                    }

                else
                    {
                        Debug.LogError("pasdewin2");
                    }
                }

            
        }
        yield return null;
    }

    public IEnumerator MoveBackwards()
    {
        Vector3 backwardsDirection = -transform.forward;
        Vector2Int down = new Vector2Int(0, -1); // Vers le bas dans la grille
        Vector2Int newpos = playerPositionInit + down;

        if (isValidMove(newpos))
        {
            transform.position += backwardsDirection * 0.18f;
            playerPositionInit = newpos;
            LogPosition(playerPositionInit); // Affiche la nouvelle position
            if (isDead(playerPositionInit))
            {
                dead = true;
                if (grid[playerPositionInit.y, playerPositionInit.x] == 2)
                {
                    Car.SetActive(true);
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 8)
                {
                    Lamp1.transform.Rotate(90f, 0f, 0f, Space.Self);
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 9)
                {
                    Lamp2.transform.Rotate(90f, 0f, 0f, Space.Self);
                    loseDialog();
                }



                if (grid[playerPositionInit.y, playerPositionInit.x] == 3)
                {

                    Factory.SetActive(true);
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 5)
                {
                    Stop.transform.Rotate(90f, 0f, 0f, Space.Self);
                    loseDialog();
                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 4)
                {
                    Pallet1.SetActive(false);
                    loseDialog();
                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 16)
                {
                    Pallet2.SetActive(false);
                    loseDialog();
                }


                if (grid[playerPositionInit.y, playerPositionInit.x] == 6)
                {
                    Brique1.transform.Translate(new Vector3(0f, -0.671f, 0f));
                    Brique2.transform.Translate(new Vector3(0f, -0.671f, 0f));
                    loseDialog();

                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 10)
                {
                    Trash.SetActive(true);
                    loseDialog();
                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 11)
                {
                    Bus.SetActive(true);
                    loseDialog();
                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 12)
                {
                    Road.transform.Translate(new Vector3(-0.805f, 0f, 0F));
                    loseDialog();
                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 13)
                {
                    Barriere.transform.Rotate(90f, 90f, 0f);
                    loseDialog();
                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 14)
                {
                    Antenna.SetActive(true);
                    loseDialog();
                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 15)
                {
                    Signal.transform.Rotate(0f, 0f, 90f);
                    loseDialog();
                }
            }
            if (grid[playerPositionInit.y, playerPositionInit.x] == 20)
                {
                if (win2.Instance != null)
                    {
                        win2.Instance.CompleteGame();
                    }

                else
                    {
                        Debug.LogError("pasdewin2");
                    }
                }
            
        }
        yield return null;

    }
    public IEnumerator MoveRight()
    {
        Vector3 rightDirection = transform.right;
        Vector2Int right = new Vector2Int(1, 0); // Vers la droite dans la grille
        Vector2Int newpos = playerPositionInit + right;

        if (isValidMove(newpos))
        {

            transform.position += rightDirection * 0.2f;
            playerPositionInit = newpos;
            LogPosition(playerPositionInit); // Affiche la nouvelle position
            if (isDead(playerPositionInit))
            {
                dead = true;
                if (grid[playerPositionInit.y, playerPositionInit.x] == 2)
                {
                    Car.SetActive(true);
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 8)
                {
                    Lamp1.transform.Rotate(90f, 0f, 0f, Space.Self);
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 9)
                {
                    Lamp2.transform.Rotate(90f, 0f, 0f, Space.Self);
                    loseDialog();
                }



                if (grid[playerPositionInit.y, playerPositionInit.x] == 3)
                {

                    Factory.SetActive(true);
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 5)
                {
                    Stop.transform.Rotate(90f, 0f, 0f, Space.Self);
                    loseDialog();
                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 4)
                {
                    Pallet1.SetActive(false);
                    loseDialog();
                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 16)
                {
                    Pallet2.SetActive(false);
                    loseDialog();
                }


                if (grid[playerPositionInit.y, playerPositionInit.x] == 6)
                {
                    Brique1.transform.Translate(new Vector3(0f, -0.671f, 0f));
                    Brique2.transform.Translate(new Vector3(0f, -0.671f, 0f));
                    loseDialog();

                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 10)
                {
                    Trash.SetActive(true);
                    loseDialog();
                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 11)
                {
                    Bus.SetActive(true);
                    loseDialog();
                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 12)
                {
                    Road.transform.Translate(new Vector3(-0.805f, 0f, 0F));
                    loseDialog();
                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 13)
                {
                    Barriere.transform.Rotate(90f, 90f, 0f);
                    loseDialog();
                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 14)
                {
                    Antenna.SetActive(true);
                    loseDialog();
                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 15)
                {
                    Signal.transform.Rotate(0f, 0f, 90f);
                    loseDialog();
                }
            }
            if (grid[playerPositionInit.y, playerPositionInit.x] == 20)
                {
                if (win2.Instance != null)
                    {
                        win2.Instance.CompleteGame();
                    }

                else
                    {
                        Debug.LogError("pasdewin2");
                    }
                }
            
        }
        yield return null;
    }
    public IEnumerator MoveLeft()
    {
        Vector3 leftDirection = -transform.right;
        Vector2Int left = new Vector2Int(-1, 0); // Vers la gauche dans la grille
        Vector2Int newpos = playerPositionInit + left;

        if (isValidMove(newpos))
        {
            transform.position += leftDirection * 0.2f;
            playerPositionInit = newpos;
            LogPosition(playerPositionInit); // Affiche la nouvelle position
            if (isDead(playerPositionInit))
            {
                dead = true;
                if (grid[playerPositionInit.y, playerPositionInit.x] == 2)
                {
                    Car.SetActive(true);
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 8)
                {
                    Lamp1.transform.Rotate(90f, 0f, 0f, Space.Self);
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 9)
                {
                    Lamp2.transform.Rotate(90f, 0f, 0f, Space.Self);
                    loseDialog();
                }



                if (grid[playerPositionInit.y, playerPositionInit.x] == 3)
                {

                    Factory.SetActive(true);
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 5)
                {
                    Stop.transform.Rotate(90f, 0f, 0f, Space.Self);
                    loseDialog();
                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 4)
                {
                    Pallet1.SetActive(false);
                    loseDialog();
                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 16)
                {
                    Pallet2.SetActive(false);
                    loseDialog();
                }


                if (grid[playerPositionInit.y, playerPositionInit.x] == 6)
                {
                    Brique1.transform.Translate(new Vector3(0f, -0.671f, 0f));
                    Brique2.transform.Translate(new Vector3(0f, -0.671f, 0f));
                    loseDialog();

                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 10)
                {
                    Trash.SetActive(true);
                    loseDialog();
                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 11)
                {
                    Bus.SetActive(true);
                    loseDialog();
                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 12)
                {
                    Road.transform.Translate(new Vector3(-0.805f, 0f, 0F));
                    loseDialog();
                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 13)
                {
                    Barriere.transform.Rotate(90f, 90f, 0f);
                    loseDialog();
                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 14)
                {
                    Antenna.SetActive(true);
                    loseDialog();
                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 15)
                {
                    Signal.transform.Rotate(0f, 0f, 90f);
                    loseDialog();
                }
            }
            if (grid[playerPositionInit.y, playerPositionInit.x] == 20)
                {
                if (win2.Instance != null)
                    {
                        win2.Instance.CompleteGame();
                    }

                else
                    {
                        Debug.LogError("pasdewin2");
                    }
                }

            
        }
        yield return null;
    }
    public void loseDialog()
    {
        panel.SetActive(true);
        textelose.text = "Vous êtes morts. ";
        losetexte.text = "Réessayer ?";

    }
}
//Ajouter bool is moving, ajouter props bonus et malus, ajouter chiono et scorz final genre 10000/emps et faire vélo demain 
//Iln y aura pas de vtesse dsl
//Debugger les scores et l'echange des scenes (passage entre scene, persistance, etc) ok je crois 
//ajouter un ovject manipulatir dans el menu et bounds control a faire aec niveau velo 
//ajouter un slider qui gere la skybox tout le temps a faire avec velo 
//Debuger les ombres qui empeche de voir le niveau a faier avec velo 
//Dimuner taille phere
//Ajouter le gameover 
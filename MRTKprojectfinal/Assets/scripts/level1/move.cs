using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;
using TMPro;

public class move : MonoBehaviour
{
    public GameObject panel;
    public int gridSizeX = 13;
    public int gridSizeY = 21;
    private Vector2Int playerPositionInit = new Vector2Int(6, 2);
    public GameObject Lamp1;
    public GameObject Lamp2;
    public GameObject Lamp3;
    public GameObject Hydro1;
    public GameObject Hydro2;
    public GameObject Trash1;
    public GameObject Car1;
    public GameObject Truck1;
    public GameObject Animal1;
    public GameObject Jardiniere1;
    public GameObject Pipe1;
    public GameObject Lid1;
    public ParticleSystem particle;
    public ParticleSystem water;
    public ParticleSystem gas;
    public GameObject sphereM;
    public GameObject sphereB;
    public TMP_Text textelose;
    public TMP_Text losetexte;
    public bool dead = false;

    private int[,] grid =
    {
        { 1,1,1,1,1,1,1,1,1,1,1,1,1 },
        { 1,1,10,0,0,0,0,0,0,0,0,1,1 },
        { 1,1,0,0,0,0,0,0,0,0,0,1,1 },
        { 1,1,0,0,2,2,2,2,1,1,0,1,1 },
        { 1,1,0,0,0,0,0,0,0,0,19,1,1 },
        { 1,1,0,0,0,0,0,0,0,0,13,1,1 },
        { 1,1,0,1,1,3,3,3,3,0,0,1,1 },
        { 1,1,0,11,0,0,0,0,0,0,0,1,1 },
        { 1,1,11,1,11,0,0,0,0,1,0,1,1 },
        { 1,1,0,11,0,0,0,0,0,0,0,1,1 },
        { 1,0,0,0,12,0,0,1,0,0,0,0,1 },
        { 1,0,0,12,1,12,0,0,0,0,0,5,1 },
        { 1,0,0,0,12,0,0,0,0,0,5,5,1 },
        { 1,0,0,0,0,0,0,18,4,0,5,5,1 },
        { 1,0,0,0,0,0,0,0,4,0,5,5,1 },
        { 1,0,6,6,6,6,8,0,4,0,5,5,1 },
        { 1,0,1,1,1,6,0,0,4,0,5,5,1 },
        { 1,0,1,1,1,6,0,9,1,0,5,5,1 },
        { 1,0,6,6,6,6,0,0,1,0,0,5,1 },
        { 1,0,0,0,7,0,20,0,0,0,0,0,1 },
        { 1,1,1,1,1,1,1,1,1,1,1,1,1 }
    };
    //2:Lamp1 ; 8: lid: 9:pipe: 10: hydro1 11:animal;12:hydro2
    //3:Lamp2; 5:truck ; 6:Car : 7: jardinier
    //4Lampe3: 13:trash

    private bool isValidMove(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < gridSizeX &&
               pos.y >= 0 && pos.y < gridSizeY &&
               grid[pos.y, pos.x] != 1;
    }

    private bool isDead(Vector2Int pos)
    {
        return grid[pos.y, pos.x] != 0 && grid[pos.y, pos.x] != 1
            && grid[pos.y, pos.x] != 20 && grid[pos.y, pos.x] != 19 && grid[pos.y, pos.x] != 18;
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
                    Lamp1.transform.Rotate(90f, 0f, 0f, Space.Self);
                   loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 3)
                {
                    Lamp2.transform.Rotate(90f, 0f, 0f, Space.Self);
                   loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 4)
                {
                    Lamp3.transform.Rotate(90f, 0f, 0f, Space.Self);
                   loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 5)
                {
                    ParticleSystem exp = Instantiate(particle, Truck1.transform.position, Quaternion.identity);
                    exp?.Play();
                    while (exp.isPlaying)
                    {
                        yield return null;
                    }
                   loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 6)
                {
                    ParticleSystem exp = Instantiate(particle, Car1.transform.position, Quaternion.identity);
                    exp?.Play();
                    while (exp.isPlaying)
                    {
                        yield return null;
                    }
                   loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 7)
                {
                   
                    Jardiniere1.transform.position = transform.position;
                   loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 8)
                {
                    Vector3 endPos = new Vector3(transform.position.x, -0.1f, transform.position.z);
                    Destroy(Lid1);
                    transform.position = endPos;
                   loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 9)
                {
                    ParticleSystem leak = Instantiate(gas, Pipe1.transform.position, Quaternion.identity);
                    leak?.Play();
                    while (leak.isPlaying)
                    {
                        yield return null;
                    }
                   loseDialog();
                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 10)
                {
                    ParticleSystem splash = Instantiate(water, Hydro1.transform.position, Quaternion.identity);
                    splash?.Play();
                    while (splash.isPlaying)
                    {
                        yield return null;
                    }
                   loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 11)
                {
                    Animal1.transform.position = transform.position;
                   loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 12)
                {
                    ParticleSystem splash = Instantiate(water, Hydro2.transform.position, Quaternion.identity);
                    splash?.Play();
                    while (splash.isPlaying)
                    {
                        yield return null;
                    }
                   loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 13)
                {
                    ParticleSystem exp = Instantiate(particle, Trash1.transform.position, Quaternion.identity);
                    exp?.Play();
                    while (exp.isPlaying)
                    {
                        yield return null;
                    }
                   loseDialog();
                }

            }
            if (grid[playerPositionInit.y, playerPositionInit.x] == 18)
            {
                win.Instance.modifyMax(-10000);
                Destroy(sphereM);
                grid[playerPositionInit.y, playerPositionInit.x] = 0;
            }
            if (grid[playerPositionInit.y, playerPositionInit.x] == 19)
            {
                win.Instance.modifyMax(10000);
                Destroy(sphereB);
                grid[playerPositionInit.y, playerPositionInit.x] = 0;
            }
            if (grid[playerPositionInit.y, playerPositionInit.x] == 20)
            {
                if (win.Instance != null)
                {
                    win.Instance.CompleteGame();
                }

                else
                {
                    Debug.LogError("pasdewin");
                }
            }

        }
    }
    //2:Lamp1 ; 8: lid: 9:pipe: 10: hydro1 11:animal;12:hydro2
    //3:Lamp2; 5:truck ; 6:Car : 7: jardinier
    //4Lampe3: 13:trash
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
                    Lamp1.transform.Rotate(90f, 0f, 0f, Space.Self);
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 3)
                {
                    Lamp2.transform.Rotate(90f, 0f, 0f, Space.Self);
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 4)
                {
                    Lamp3.transform.Rotate(90f, 0f, 0f, Space.Self);
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 5)
                {
                    ParticleSystem exp = Instantiate(particle, Truck1.transform.position, Quaternion.identity);
                    exp?.Play();
                    while (exp.isPlaying)
                    {
                        yield return null;
                    }
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 6)
                {
                    ParticleSystem exp = Instantiate(particle, Car1.transform.position, Quaternion.identity);
                    exp?.Play();
                    while (exp.isPlaying)
                    {
                        yield return null;
                    }
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 7)
                {

                    Jardiniere1.transform.position = transform.position;
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 8)
                {
                    Vector3 endPos = new Vector3(transform.position.x, -0.1f, transform.position.z);
                    Destroy(Lid1);
                    transform.position = endPos;
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 9)
                {
                    ParticleSystem leak = Instantiate(gas, Pipe1.transform.position, Quaternion.identity);
                    leak?.Play();
                    while (leak.isPlaying)
                    {
                        yield return null;
                    }
                    loseDialog();
                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 10)
                {
                    ParticleSystem splash = Instantiate(water, Hydro1.transform.position, Quaternion.identity);
                    splash?.Play();
                    while (splash.isPlaying)
                    {
                        yield return null;
                    }
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 11)
                {
                    Animal1.transform.position = transform.position;
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 12)
                {
                    ParticleSystem splash = Instantiate(water, Hydro2.transform.position, Quaternion.identity);
                    splash?.Play();
                    while (splash.isPlaying)
                    {
                        yield return null;
                    }
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 13)
                {
                    ParticleSystem exp = Instantiate(particle, Trash1.transform.position, Quaternion.identity);
                    exp?.Play();
                    while (exp.isPlaying)
                    {
                        yield return null;
                    }
                    loseDialog();
                }
            }
            if (grid[playerPositionInit.y, playerPositionInit.x] == 18)
            {
                win.Instance.modifyMax(-10000);
                Destroy(sphereM);
                grid[playerPositionInit.y, playerPositionInit.x] = 0;
            }
            if (grid[playerPositionInit.y, playerPositionInit.x] == 19)
            {
                win.Instance.modifyMax(10000);
                Destroy(sphereB);
                grid[playerPositionInit.y, playerPositionInit.x] = 0;
            }
            if (grid[playerPositionInit.y, playerPositionInit.x] == 20)
            {

                if (win.Instance != null)
                {
                    win.Instance.CompleteGame();

                }

                else
                {
                    Debug.LogError("pasdewin");
                }
            }
        }
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
                    Lamp1.transform.Rotate(90f, 0f, 0f, Space.Self);
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 3)
                {
                    Lamp2.transform.Rotate(90f, 0f, 0f, Space.Self);
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 4)
                {
                    Lamp3.transform.Rotate(90f, 0f, 0f, Space.Self);
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 5)
                {
                    ParticleSystem exp = Instantiate(particle, Truck1.transform.position, Quaternion.identity);
                    exp?.Play();
                    while (exp.isPlaying)
                    {
                        yield return null;
                    }
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 6)
                {
                    ParticleSystem exp = Instantiate(particle, Car1.transform.position, Quaternion.identity);
                    exp?.Play();
                    while (exp.isPlaying)
                    {
                        yield return null;
                    }
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 7)
                {

                    Jardiniere1.transform.position = transform.position;
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 8)
                {
                    Vector3 endPos = new Vector3(transform.position.x, -0.1f, transform.position.z);
                    Destroy(Lid1);
                    transform.position = endPos;
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 9)
                {
                    ParticleSystem leak = Instantiate(gas, Pipe1.transform.position, Quaternion.identity);
                    leak?.Play();
                    while (leak.isPlaying)
                    {
                        yield return null;
                    }
                    loseDialog();
                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 10)
                {
                    ParticleSystem splash = Instantiate(water, Hydro1.transform.position, Quaternion.identity);
                    splash?.Play();
                    while (splash.isPlaying)
                    {
                        yield return null;
                    }
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 11)
                {
                    Animal1.transform.position = transform.position;
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 12)
                {
                    ParticleSystem splash = Instantiate(water, Hydro2.transform.position, Quaternion.identity);
                    splash?.Play();
                    while (splash.isPlaying)
                    {
                        yield return null;
                    }
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 13)
                {
                    ParticleSystem exp = Instantiate(particle, Trash1.transform.position, Quaternion.identity);
                    exp?.Play();
                    while (exp.isPlaying)
                    {
                        yield return null;
                    }
                    loseDialog();
                }
            }
            if (grid[playerPositionInit.y, playerPositionInit.x] == 18)
            {
                win.Instance.modifyMax(-10000);
                Destroy(sphereM);
                grid[playerPositionInit.y, playerPositionInit.x] = 0;
            }
            if (grid[playerPositionInit.y, playerPositionInit.x] == 19)
            {
                win.Instance.modifyMax(10000);
                Destroy(sphereB);
                grid[playerPositionInit.y, playerPositionInit.x] = 0;
            }
            if (grid[playerPositionInit.y, playerPositionInit.x] == 20)
            {
                if (win.Instance != null)
                {
                    win.Instance.CompleteGame();
                }

                else
                {
                    Debug.LogError("pasdewin");
                }
            }
        }
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
                    Lamp1.transform.Rotate(90f, 0f, 0f, Space.Self);
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 3)
                {
                    Lamp2.transform.Rotate(90f, 0f, 0f, Space.Self);
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 4)
                {
                    Lamp3.transform.Rotate(90f, 0f, 0f, Space.Self);
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 5)
                {
                    ParticleSystem exp = Instantiate(particle, Truck1.transform.position, Quaternion.identity);
                    exp?.Play();
                    while (exp.isPlaying)
                    {
                        yield return null;
                    }
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 6)
                {
                    ParticleSystem exp = Instantiate(particle, Car1.transform.position, Quaternion.identity);
                    exp?.Play();
                    while (exp.isPlaying)
                    {
                        yield return null;
                    }
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 7)
                {

                    Jardiniere1.transform.position = transform.position;
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 8)
                {
                    Vector3 endPos = new Vector3(transform.position.x, -0.1f, transform.position.z);
                    Destroy(Lid1);
                    transform.position = endPos;
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 9)
                {
                    ParticleSystem leak = Instantiate(gas, Pipe1.transform.position, Quaternion.identity);
                    leak?.Play();
                    while (leak.isPlaying)
                    {
                        yield return null;
                    }
                    loseDialog();
                }

                if (grid[playerPositionInit.y, playerPositionInit.x] == 10)
                {
                    ParticleSystem splash = Instantiate(water, Hydro1.transform.position, Quaternion.identity);
                    splash?.Play();
                    while (splash.isPlaying)
                    {
                        yield return null;
                    }
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 11)
                {
                    Animal1.transform.position = transform.position;
                   loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 12)
                {
                    ParticleSystem splash = Instantiate(water, Hydro2.transform.position, Quaternion.identity);
                    splash?.Play();
                    while (splash.isPlaying)
                    {
                        yield return null;
                    }
                    loseDialog();
                }
                if (grid[playerPositionInit.y, playerPositionInit.x] == 13)
                {
                    ParticleSystem exp = Instantiate(particle, Trash1.transform.position, Quaternion.identity);
                    exp?.Play();
                    while (exp.isPlaying)
                    {
                        yield return null;
                    }
                    loseDialog();
                }
            }
            if (grid[playerPositionInit.y, playerPositionInit.x] == 18)
            {
                win.Instance.modifyMax(-10000);
                Destroy(sphereM);
                grid[playerPositionInit.y, playerPositionInit.x] = 0;
            }
            if (grid[playerPositionInit.y, playerPositionInit.x] == 19)
            {
                win.Instance.modifyMax(10000);
                Destroy(sphereB);
                grid[playerPositionInit.y, playerPositionInit.x] = 0;
            }
            if (grid[playerPositionInit.y, playerPositionInit.x] == 20)
            {
                if (win.Instance != null)
                {
                    win.Instance.CompleteGame();
                }

                else
                {
                    Debug.LogError("pasdewin");
                }
            }
        }
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
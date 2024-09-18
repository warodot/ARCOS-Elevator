using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
using UnityEngine.UIElements;
using System;

namespace LucasRojo
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        //public int gameTime = 0;
        //public int gameGoal = 0;
        //public int gameScore = 0;
        //public int gameLevel = 0;
        //public int gameDamage = 0;
        //public string gameState = "neutral";
        //public bool isScoreGaming = false;
        //public TextMeshProUGUI debugState;
        //public TextMeshProUGUI Objetive;
        //public TextMeshProUGUI debugLevel;
        ////public TextMeshProUGUI debugDamage;
        //public GameObject perfectParent;
        //public GameObject portalParent;
        //[Space]
        //[Header("Debug")]
        //public bool debug = true;
        //[Space]
        //[Header("Games")]
        //public GameObject trompoGame1;
        //public GameObject trompoGame2;
        //public GameObject trompoGame3;
        //public GameObject blockGame1;
        //public GameObject blockGame2;
        //public GameObject batsGame1;
        //public GameObject batsGame2;
        //public GameObject currentActiveGame;
        //[Space]
        //[Header("Cameras")]
        //public CinemachineVirtualCamera neutralCamera;
        //public CinemachineVirtualCamera upperCamera;
        //public CinemachineVirtualCamera lowerCamera;
        //[Space]
        //[Header("Positions")]
        //public Transform neutralPos;
        //public Transform arenaPos;
        //public Transform stillGamePos;
        //[Space]
        //[Header("Player")]
        //public GameObject player;
        public int level = 1;
        public int round = 1;


        private void Awake()
        {
            instance = this;
        }



        private void Update()
        {
            //    if (debug)
            //    {
            //        debugState.text = gameState;
            //        debugLevel.text = "LV: " + gameLevel.ToString();
            //    }



            //    //si el score es igual a nuestro goal, acabar ataque
            //    if (gameScore >= gameGoal && isScoreGaming)
            //    {
            //        NeutralState(1);
            //    }
            //    switch (gameState)
            //    {
            //        case "Neutral":
            //            {
            //                Objetive.text = "";
            //                break;
            //            }
            //        case "Trompo":
            //            {
            //                Objetive.text = "Survive " + (gameTime) + " more seconds!";
            //                break;
            //            }
            //        case "Block":
            //            {
            //                Objetive.text = "Break " + (gameGoal - gameScore).ToString() + " more blocks!";
            //                break;
            //            }
            //        case "Bats":
            //            {
            //                Objetive.text = "Survive " + (gameTime) + " more seconds!";
            //                break;
            //            }
            //        case "Laser":
            //            {
            //                Objetive.text = "Hope you used <color=blue>[Defend]</color> before, HAHA!";
            //                break;
            //            }
            //        default:
            //            {
            //                Objetive.text = "";
            //                break;
            //            }
            //    }

            //}
            ////Debug
            //#region Game states

            //public void NeutralState(int delay)
            //{
            //    StartCoroutine(NeutralCoroutine(delay));
            //    gameLevel = 0;

            //    isScoreGaming = false;
            //    ActivateCamera(neutralCamera);

            //    blockGame1.SetActive(false);
            //    blockGame2.SetActive(false);
            //    batsGame1.SetActive(false);
            //    batsGame2.SetActive(false);

            //    gameTime = 0;
            //    gameGoal = 0;
            //    gameScore = 0;
            //    Destroy(currentActiveGame);
            //}
            //IEnumerator NeutralCoroutine(int delay)
            //{
            //    //si el delay es 0, todo ocurre de inmediato
            //    yield return new WaitForSeconds(delay);
            //    gameState = "Neutral";
            //    GameController.instance.turnCount += 1;
            //    GameController.instance.isPlayerTurn = true;
            //    MovePlayerToNeutral();

            //    portalParent.SetActive(false);

            //    if (gameDamage == 0 && GameController.instance.turnCount != 1)
            //    {
            //        perfectParent.SetActive(true);
            //        Player.instance.IncreaseFP(2);
            //    }
            //    gameDamage = 0;
            //}
            //public void BackToNeutral()
            //{

            //}

            //public void TrompoState(int level)
            //{
            //    gameState = "Trompo";
            //    ActivateCamera(upperCamera);
            //    MovePlayerToArena();
            //    Destroy(currentActiveGame);

            //    if (level == 1)
            //    {
            //        gameLevel = 1;
            //        currentActiveGame = Instantiate(trompoGame1, Vector3.zero, Quaternion.identity);
            //        gameTime = 20;
            //        StartCoroutine(CountDown());

            //    }
            //    else if (level == 2)
            //    {
            //        gameLevel = 2;
            //        currentActiveGame = Instantiate(trompoGame2, Vector3.zero, Quaternion.identity);
            //        gameTime = 25;
            //        StartCoroutine(CountDown());
            //    }
            //    else if (level == 3)
            //    {
            //        gameLevel = 3;
            //        currentActiveGame = Instantiate(trompoGame3, Vector3.zero, Quaternion.identity);
            //        gameTime = 25;
            //        StartCoroutine(CountDown());
            //    }
            //    else
            //    {
            //        Debug.Log("Trompo level fuera de rango");
            //    }

            //}
            //public void BlockState(int level)
            //{
            //    gameState = "Block";
            //    ActivateCamera(lowerCamera);
            //    MovePlayerToJumping();
            //    isScoreGaming = true;

            //    portalParent.SetActive(true);

            //    if (level == 1)
            //    {
            //        gameLevel = 1;
            //        blockGame1.SetActive(true);
            //        //currentActiveGame = Instantiate(blockGame1, Vector3.zero, Quaternion.identity);

            //        gameGoal = 15;
            //        gameScore = 0;
            //    }
            //    else if (level == 2)
            //    {
            //        gameLevel = 2;
            //        blockGame2.SetActive(true);

            //        gameGoal = 15;
            //        gameScore = 0;
            //    }
            //    else
            //    {
            //        Debug.Log("block level fuera de rango");
            //    }



            //}
            //public void BatsState(int level)
            //{
            //    gameState = "Bats";
            //    ActivateCamera(neutralCamera);
            //    MovePlayerToJumping();

            //    portalParent.SetActive(true);

            //    if (level == 1)
            //    {
            //        gameLevel = 1;
            //        batsGame1.SetActive(true);

            //        gameTime = 15;
            //        StartCoroutine(CountDown());
            //    }
            //    else if (level == 2)
            //    {
            //        gameLevel = 2;
            //        batsGame2.SetActive(true);

            //        gameTime = 20;
            //        StartCoroutine(CountDown());
            //    }
            //    else
            //    {
            //        Debug.Log("bats level fuera de rango");
            //    }


            //}
            ////Aun sin implementar, planeado.
            //public void TrompoBlockState(int level)
            //{
            //    gameState = "TrompoBlock";
            //    Objetive.text = "Objetivo: ¡Rompe " + (gameGoal - gameScore) + " bloques más!";
            //    gameGoal = 15;
            //    gameScore = 0;
            //}
            //public void LaserState(int level)
            //{
            //    gameState = "Laser";

            //    if (level == 1)
            //    {
            //        gameLevel = 1;
            //        // lanzar 1 proyectil
            //        BorosBoss.instance.EnergyBallAttack(1);
            //        NeutralState(3);
            //    }
            //    else if (level == 2)
            //    {
            //        gameLevel = 2;
            //        // lanzar 2 proyecyiles
            //        BorosBoss.instance.EnergyBallAttack(2);
            //        NeutralState(4);
            //    }
            //    else
            //    {
            //        Debug.Log("Laser level fuera de rango");
            //    }
            //}
            //public void BubbleState()
            //{
            //    Objetive.text = "Objetivo: ¡Sobrevive!";
            //    gameTime = 20;
            //}
            //#endregion

            //#region Camera manag
            //public void ActivateCamera(CinemachineVirtualCamera activeCamera)
            //{
            //    neutralCamera.gameObject.SetActive(false);
            //    upperCamera.gameObject.SetActive(false);
            //    lowerCamera.gameObject.SetActive(false);

            //    activeCamera.gameObject.SetActive(true);
            //}
            //#endregion

            //#region Game time manag
            //IEnumerator CountDown()
            //{
            //    while (gameTime > 0)
            //    {
            //        yield return new WaitForSeconds(1f);
            //        gameTime--;
            //    }
            //    // Cuando el tiempo llegue a cero, puedes hacer algo aquí
            //    NeutralState(0);
            //}
            //#endregion

            //#region Player position & Movement enable
            //public void MovePlayerToNeutral()
            //{
            //    player.transform.position = neutralPos.position;
            //    player.GetComponent<Player>().canMove = false;
            //    player.GetComponent<Player>().canJump = false;
            //}
            //public void MovePlayerToArena()
            //{
            //    player.transform.position = arenaPos.position;
            //    player.GetComponent<Player>().canMove = true;
            //    player.GetComponent<Player>().canJump = false;
            //}
            //public void MovePlayerToJumping()
            //{
            //    player.transform.position = stillGamePos.position;
            //    player.GetComponent<Player>().canMove = false;
            //    player.GetComponent<Player>().canJump = true;
            //}
            //#endregion

            //public void PlayerState()
            //{
            //    //turno del player
            //}
            //public void EnemyState()
            //{
            //    //turno del enemigo
            //}



        }
    }
}


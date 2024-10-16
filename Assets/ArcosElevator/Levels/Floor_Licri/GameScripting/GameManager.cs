using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
using UnityEngine.UIElements;
using System;
using UnityEngine.Android;
using UnityEngine.Events;

namespace LucasRojo
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        [Header("Game Stats")]
        public int gameTime = 0;
        public int level = 1;
        public int round = 1;
        public int strikes = 3;
        public bool roundIsActive = false;
        public bool alreadyLost = false;
        [Header("Player Stats")]
        public int maxGranade = 3;
        public int currentGranade = 3;
        public bool canUseGranade = false;
        [Header("Scoreboard References")]
        public TextMeshPro roundText;
        public TextMeshPro gameTimeText;
        public GameObject defeatText;
        public GameObject strike1;
        public GameObject strike2;
        public GameObject strike3;
        [Header("Unity events")]
        public UnityEvent OnDefeat;
        [Header("Round Dialogue")]
        public GameObject RoundOneEnd;
        public GameObject RoundTwoEnd;
        public GameObject RoundThreeEnd;
        public GameObject RoundFourEnd;
        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            
        }

        private void Update()
        {

            roundText.text = "ROUND: " + round;
            gameTimeText.text = "TIME: " + gameTime;

            if (strikes == 1)
            {
                strike1.SetActive(true);
                strike2.SetActive(false);
                strike3.SetActive(false);
            }
            else if (strikes == 2)
            {
                strike1.SetActive(true);
                strike2.SetActive(true);
                strike3.SetActive(false);
            }
            else if (strikes == 3)
            {
                strike1.SetActive(true);
                strike2.SetActive(true);
                strike3.SetActive(true);
            }
            else
            {
                strike1.SetActive(false);
                strike2.SetActive(false);
                strike3.SetActive(false);
            }


        }
        public void ToggleRound(bool state)
        {
            roundIsActive = state;

            if (roundIsActive)
            {
                StartCoroutine(CountDown());
            }
        }

        public void ReduceStrike(int value)
        {
            strikes -= value;

            if (strikes <= 0)
            {
                
                if (round == 1) { gameTime = 20; }
                else if (round == 2) { gameTime = 40; }
                else if (round == 3) { gameTime = 60; }
                else if (round == 4) { gameTime = 80; }
                roundIsActive = false;
                defeatText.SetActive(true);
                alreadyLost = true;
                //El evento final del dialogo deberia devolver los 3 strikes y permitir jugar en la misma ronda
                OnDefeat.Invoke();
            }
        }
        public void SetStrikes(int value)
        {
            strikes = value;
        }
        public void SetRound(int value)
        {
            round = value;
        }
        public void SetCanUseGranade(bool value)
        {
            canUseGranade = value;
        }
            
        #region GameTime manag
        IEnumerator CountDown()
        {
            while (gameTime > 0 && roundIsActive)
            {
                yield return new WaitForSeconds(1f);
                gameTime--;
            }
            // Cuando el tiempo llegue a cero
            Debug.Log("El tiempo llego a 0, desactivar enemigos");
            roundIsActive = false;
            EnemyPool.instance.DisableAll();
            // EVENTOS AL TERMINAR UNA RONDA
            if (round == 1 && strikes > 0)
            {
                gameTime = 40;
                RoundOneEnd.SetActive(true);
                round += 1;
                strikes = 3;
            }
            else if (round == 2 && strikes > 0)
            {
                gameTime = 60;
                RoundTwoEnd.SetActive(true);
                round += 1;
                strikes = 3;
                currentGranade = 3;
            }
            else if (round == 3 && strikes > 0)
            {
                gameTime = 80;
                RoundThreeEnd.SetActive(true);
                round += 1;
                strikes = 3;
                currentGranade = 3;
            }
            else if (round == 4 && strikes > 0)
            {
                gameTime = 100;
                RoundFourEnd.SetActive(true);
                round += 1;
                strikes = 3;
                currentGranade = 3;
            }
        }
        #endregion

            



        
    }
}


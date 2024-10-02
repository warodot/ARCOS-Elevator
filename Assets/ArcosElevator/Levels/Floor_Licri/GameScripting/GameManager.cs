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
        [Header("Game Stats")]
        public int gameTime = 0;
        public int level = 1;
        public int round = 1;
        public bool roundIsActive = false;
        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            
        }

        private void Update()
        {



            
                

        }
            
        #region GameTime manag
        IEnumerator CountDown()
        {
            while (gameTime > 0)
            {
                yield return new WaitForSeconds(1f);
                gameTime--;
            }
            // Cuando el tiempo llegue a cero, puedes hacer algo aquí
            //NeutralState(0);
        }
        #endregion

            



        
    }
}


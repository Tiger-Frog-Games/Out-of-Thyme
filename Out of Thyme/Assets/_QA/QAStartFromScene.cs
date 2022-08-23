using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class QAStartFromScene : MonoBehaviour
    {
        #region Variables

        [SerializeField] private EventChannelGameStart OnGameStart;
        [SerializeField] private StartGameData TestData;
        
        #endregion

        #region Unity Methods

        private void Start()
        {
            OnGameStart.RaiseEvent(TestData);
        }

        #endregion

        #region Methods

        #endregion
    }
}
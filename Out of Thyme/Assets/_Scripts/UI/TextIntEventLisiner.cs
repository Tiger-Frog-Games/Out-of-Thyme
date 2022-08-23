using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TigerFrogGames
{
    public class TextIntEventLisiner : MonoBehaviour
    {
        #region Variables

        [SerializeField] private EventChannelInt OnIntChannel;
        [SerializeField] private TMP_Text text;
        [SerializeField] private String beforeText;
        [SerializeField] private String afterText;
        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            OnIntChannel.OnEvent += OnIntChannelOnOnEvent;
        }
        
        private void OnDisable()
        {
            OnIntChannel.OnEvent -= OnIntChannelOnOnEvent;
        }

        #endregion

        #region Methods

        private void OnIntChannelOnOnEvent(int obj)
        {
            text.text = $"{beforeText}{obj}{afterText}";
        }
        
        #endregion
    }
}
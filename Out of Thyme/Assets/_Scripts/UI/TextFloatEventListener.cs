using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TigerFrogGames
{
    public class TextFloatEventListener : MonoBehaviour
    {
        #region Variables

        [SerializeField] private EventChannelFloat OnFloatChannel;
        [SerializeField] private TMP_Text text;
        [SerializeField] private String beforeText;
        [SerializeField] private String afterText;
        
        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            OnFloatChannel.OnEvent += OnFloatEventOnEvent;
        }
        
        private void OnDisable()
        {
            OnFloatChannel.OnEvent -= OnFloatEventOnEvent;
        }

        #endregion

        #region Methods

        private void OnFloatEventOnEvent(float obj)
        {
            text.text = $"{beforeText}{obj}{afterText}";
        }
        
        #endregion
    }
}
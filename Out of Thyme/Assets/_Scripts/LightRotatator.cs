using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class LightRotatator : MonoBehaviour
    {
        #region Variables

        [SerializeField] private EventChannelGameStart OnGameStart;
        [SerializeField] private EventChannelInt OnSecChange;

        [SerializeField] private Vector3 StartRotation;
        [SerializeField] private Vector3 endRotataion;

        [SerializeField] private int slerpDampener = 1;
        
        private Quaternion _startQuat;
        private Quaternion _endQuat;

        private Quaternion targetRotation;
        
        private float _maxTime;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            OnGameStart.OnEvent += OnGameStartOnOnEvent;
            OnSecChange.OnEvent += OnSecChangeOnOnEvent;

            _startQuat = Quaternion.Euler(StartRotation);
            _endQuat = Quaternion.Euler(endRotataion);

        }

        private void OnDestroy()
        {
            OnGameStart.OnEvent -= OnGameStartOnOnEvent;
            OnSecChange.OnEvent -= OnSecChangeOnOnEvent;
        }

        #endregion

        #region Methods

        private void OnGameStartOnOnEvent(StartGameData obj)
        {
            _maxTime = TimeManager.CalculateTime(obj.StartMin, obj.StartSec);

            if (_maxTime == 0) _maxTime = -1;

            _maxTime /= 60;

        }

        private void OnSecChangeOnOnEvent(int obj)
        {
            targetRotation = Quaternion.Slerp(_endQuat,_startQuat,obj/_maxTime);
        }

        private void Update()
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, slerpDampener * Time.deltaTime);
        }

        #endregion
    }
}
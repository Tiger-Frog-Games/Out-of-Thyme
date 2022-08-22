using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    [Serializable]
    public class Request 
    {
        [field: SerializeField] public RequestData RequestData { private set; get; }
        public float TimeLeft { private set; get; }
        
        public Request(RequestData requestData)
        {
            RequestData = requestData;
            TimeLeft = RequestData.TimeToComplete;
        }
    }
}
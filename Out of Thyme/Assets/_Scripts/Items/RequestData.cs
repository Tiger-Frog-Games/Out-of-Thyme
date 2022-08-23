using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    [CreateAssetMenu(menuName = "Request/Basic")]
    public class RequestData : ScriptableObject
    {
       [field: SerializeField] public float TimeToComplete { private set; get; }
        [field: SerializeField] public ItemData RequiredItem { private set; get; }

        public float getTimeLeftToComplete()
        {
            if (TimeToComplete == 0)
            {
                return -1;
            }

            return TimeToComplete;
        }
    }
}
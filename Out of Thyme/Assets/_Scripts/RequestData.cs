using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    [CreateAssetMenu(menuName = "Request/Basic")]
    public class RequestData : ScriptableObject
    {
        [field: SerializeField] public String Name { private set; get; }
        [field: SerializeField] public float TimeToComplete { private set; get; }
        [field: SerializeField] public ItemData RequiredItem { private set; get; }
    }
}
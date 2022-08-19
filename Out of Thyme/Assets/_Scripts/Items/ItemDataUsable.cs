using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class ItemDataUsable : ItemData
    {
        //These are hooks for a nice to have feature. 
        //What type of tool would be determined by the animation to play. 
        //Values are added for the future ie a bigger value will cut vegtables faster. 
        [field: SerializeField] public string usableAnimation { private set; get; }
        [field: SerializeField] public float usableValue { private set; get; }
    }
}
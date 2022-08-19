using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    [CreateAssetMenu(menuName = "Item/Vegetable")]
    public class ItemData : ScriptableObject
    {
        [field: SerializeField] public string Name { private set; get; }
        [field: SerializeField] public GameObject VisualPrefab { private set; get; }
        [field: SerializeField] public Vector3 HeldOffset { private set; get; }
    }
}
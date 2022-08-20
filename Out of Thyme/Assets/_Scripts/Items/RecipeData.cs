using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    [Serializable]
    public struct RecipeData
    {
        [field: SerializeField] public ItemData CreatedItem { private set; get; }
        [field: SerializeField] public ItemTransformationType TypeOfTransformation { private set; get; }
        [field: SerializeField] public float TimeToTransform { private set; get; }
        [field: SerializeField] public List<ItemData> RequiredItems { private set; get; }
        
        public RecipeData(ItemTransformationType typeOfTransformation, float timeToTransform, ItemData createdItem, List<ItemData> requiredItems)
        {
            TypeOfTransformation = typeOfTransformation;
            TimeToTransform = timeToTransform;
            CreatedItem = createdItem;
            RequiredItems = requiredItems;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace TigerFrogGames
{
    [CreateAssetMenu(menuName = "Item/Vegetable")]
    public class ItemData : ScriptableObject
    {
        [field: SerializeField] public string Name { private set; get; }
        [field: SerializeField] public int ScoreValue { private set; get; }
        [field: SerializeField] public GameObject VisualPrefab { private set; get; }
        [field: SerializeField] public Vector3 OffSetHeldOnPlayerHead { private set; get; }
        [field: SerializeField] public Vector3 OffsetHeldOnInteractableItemHolder { private set; get; }
        
        [field: SerializeField] public ItemTransformations[] ItemTransformations;

        /// <summary>
        /// Returns the prefab based on what it transforms into. 
        /// </summary>
        /// <param name="transformationType"></param>
        /// <returns></returns>
        public ItemData getTransformedItemData(ItemTransformationType transformationType)
        {
            foreach (var v in ItemTransformations)
            {
                if (v.TypeOfTransformation == transformationType)
                {
                    return v.ItemData;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the time to transform.
        /// 
        /// </summary>
        /// <param name="transformationType"></param>
        /// <returns>Time to transform or -1 if transformation is not found</returns>
        public float getTransformationTime(ItemTransformationType transformationType)
        {
            foreach (var v in ItemTransformations)
            {
                if (v.TypeOfTransformation == transformationType)
                {
                    return v.TimeToTransform;
                }
            }

            return -1;
        }
    }

    [Serializable]
    public struct ItemTransformations
    {
        [field: SerializeField] public ItemTransformationType TypeOfTransformation { private set; get; }
        [field: SerializeField] public float TimeToTransform { private set; get; }
        [field: SerializeField] public ItemData ItemData { private set; get; }
        
        public ItemTransformations(ItemTransformationType typeOfTransformation, ItemData itemData, float timeToTransform)
        {
            TypeOfTransformation = typeOfTransformation;
            this.ItemData = itemData;
            TimeToTransform = timeToTransform;
        }
    }
}
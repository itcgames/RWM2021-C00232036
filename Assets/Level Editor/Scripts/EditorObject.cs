using UnityEngine;
using System;

public class EditorObject : MonoBehaviour
{
    public enum ObjectType { Item_1, Item_2, Item_3, Item_4, Item_5, Item_6, Item_7, Item_8, Player, Collectable, Background };

    /// <summary>
    /// This is all the info that the editor needs to be
    /// able to save the level items.
    /// </summary>
    [Serializable]
    public struct Data
    {
        public Vector3 obPosition;
        public Quaternion obRotation;
        public ObjectType obType;
        public int bckGrndIndex;
    }

    public Data data;
}

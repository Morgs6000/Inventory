using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Item", fileName = "New Item")]
public class Item : ScriptableObject {
    public EnumVoxels voxelID;
    //public EnumItems itemID;

    [Space(20)]
    public Sprite sprite;
    public string itemName;
    public int maxStack = 1;
}

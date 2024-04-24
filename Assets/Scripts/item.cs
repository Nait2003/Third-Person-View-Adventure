using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class item : ScriptableObject
{
    [Header("Only ui")]
    public Sprite image;
    public bool stackable = true;
}

public enum ItemType {
Buildingblock,
Tool
}

public enum ActionType{
    Dig,
    Mine
}
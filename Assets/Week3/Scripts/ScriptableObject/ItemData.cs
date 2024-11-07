using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipable,
    Consumable,
    Resource
}

public enum ConsumableType
{
    Health,
    MagicPoint
}

[Serializable]
public class ItemDataConsumable
{
    public ConsumableType _type;
    public float _value;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string _displayName;
    public string _description;
    public string _itemCode;
    public ItemType _type;
    public Sprite _icon;

    [Header("Stacking")]
    public bool _canStack;
    public int _maxStackAmount;

    [Header("Consumable")]
    public ItemDataConsumable[] _consumables;

    [Header("Equip")]
    public GameObject _equipPrefab;
}

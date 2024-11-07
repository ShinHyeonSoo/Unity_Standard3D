using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    private PlayerController _controller;
    private PlayerCondition _condition;
    public Equipment _equipment;

    public ItemData _itemData;
    public Action AddItem;

    public Transform _dropPosition;

    public PlayerController Controller { get { return _controller; } }
    public PlayerCondition Condition { get { return _condition; } }

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        _controller = GetComponent<PlayerController>();
        _condition = GetComponent<PlayerCondition>();
        _equipment = GetComponent<Equipment>();
    }
}

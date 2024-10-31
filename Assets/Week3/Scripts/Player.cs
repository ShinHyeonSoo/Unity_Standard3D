using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerController _controller;

    public PlayerController Controller { get { return _controller; } }

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        _controller = GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

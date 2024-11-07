using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject(nameof(GameManager)).AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    private GameObject _uiCanvas;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance == this)
            {
                Destroy(gameObject);
            }
        }

        InitUI();
    }

    private void InitUI()
    {
        if(_uiCanvas == null)
        {
            var ui = Resources.Load<GameObject>("Prefabs/UI");
            _uiCanvas = Instantiate(ui);
        }
    }
}

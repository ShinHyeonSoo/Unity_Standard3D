using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISetting : MonoBehaviour
{
    public GameObject _settingWindow;

    // Start is called before the first frame update
    void Start()
    {
        CharacterManager.Instance.Player.Controller.Setting += Toggle;

        _settingWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Toggle()
    {
        if (IsOpen())
        {
            _settingWindow.SetActive(false);
        }
        else
        {
            _settingWindow.SetActive(true);
        }
    }

    public bool IsOpen()
    {
        return _settingWindow.activeInHierarchy;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPromptText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CharacterManager.Instance.Player.gameObject.GetComponent<Interaction>()._promptText = gameObject.GetComponent<TextMeshProUGUI>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

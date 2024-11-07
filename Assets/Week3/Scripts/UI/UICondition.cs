using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition _health;
    public Condition _stamina;

    // Start is called before the first frame update
    void Start()
    {
        CharacterManager.Instance.Player.Condition._uiCondition = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

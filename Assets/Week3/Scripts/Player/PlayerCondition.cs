using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public interface IDamageable
{
    void TakePhysicalDamage(int damage);
}

public class PlayerCondition : MonoBehaviour, IDamageable
{
    public UICondition _uiCondition;

    Condition Health { get { return _uiCondition._health; } }
    Condition MagicPoint { get { return _uiCondition._magicPoint; } }

    public event Action OnTakeDamage;

    void Update()
    {
        //MagicPoint.Add(MagicPoint._passiveValue * Time.deltaTime);

        if (Health._curValue == 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        Health.Add(amount);
    }

    public void RecoveryMP(float amount)
    {
        MagicPoint.Add(amount);
    }

    public void Die()
    {
        Debug.Log("ав╬З╢ы.");
    }

    public void TakePhysicalDamage(int damage)
    {
        Health.Subtract(damage);
        OnTakeDamage?.Invoke();
    }

    public bool UseMP(float amount)
    {
        if (MagicPoint._curValue - amount < 0f)
        {
            return false;
        }

        MagicPoint.Subtract(amount);
        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class ArrowBehaviour : MonoBehaviour
{
    [SerializeField] private int damageRate;
    public void SetDamage(int skillLevel) 
    { 
        damageRate = skillLevel;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<HealthComponent>(out var healthComponent))
        {
            healthComponent.GetDamage(damageRate);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    public UnityAction<GameObject> PlayerDied;
    public UnityAction PlayerResurrected;

    public int MaxHealth;
    public int AutoHealPerSec;

    private bool _issAutoHealing;
    private float _currentHealth;

    private Animator characterAnimator;

    private bool isLoaded;

    private void Start()
    {
        if (GetComponent<Animator>())
        {
            characterAnimator = GetComponent<Animator>();
            isLoaded = true;
        }
    }

    public void Init()
    {
        _currentHealth = MaxHealth;
        PlayerResurrected?.Invoke();
    }

    public void GetDamage(int value)
    {
        ChangeCurrentHealth(-value);
    }

    public void Heal(int value)
    {
        ChangeCurrentHealth(value);
    }

    private void ChangeCurrentHealth(float value)
    {
        _currentHealth += value;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            PlayerDied?.Invoke(gameObject);

            if (isLoaded)
            {
                characterAnimator.SetBool("isDeath", true);
            }
        }
        else if (_currentHealth > MaxHealth)
        {
            _currentHealth = MaxHealth;
        }
    }

    private void CheckAutoHealing()
    {
        _issAutoHealing = _currentHealth < MaxHealth;
    }

    private void Update()
    {
        CheckAutoHealing();

        if(_issAutoHealing)
        {
            ChangeCurrentHealth(AutoHealPerSec * Time.deltaTime);
        }
    }
}

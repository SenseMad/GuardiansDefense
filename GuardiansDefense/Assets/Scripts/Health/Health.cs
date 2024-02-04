using System;
using UnityEngine;

namespace GuardiansDefense.HealthManager
{
  public class Health : MonoBehaviour
  {
    [SerializeField] private int _maxHealth = 100;

    //======================================

    public int MaxHealth => _maxHealth;

    public int CurrentHealth { get; private set; }

    //======================================

    public event Action<int> OnAddHealth;

    public event Action<int> OnTakeHealth;

    public event Action OnInstantlyKill;

    //======================================

    private void Start()
    {
      CurrentHealth = _maxHealth;
    }

    //======================================

    public void AddHealth(int parHealth)
    {
      if (parHealth < 0)
        return;

      int healthBefore = CurrentHealth;
      CurrentHealth += parHealth;

      if (CurrentHealth > _maxHealth)
        CurrentHealth = _maxHealth;

      int healthAmount = CurrentHealth - healthBefore;
      if (healthAmount > 0)
        OnAddHealth?.Invoke(healthAmount);
    }

    public void TakeDamage(int parHealth)
    {
      if (parHealth < 0)
        return;

      int healthBefore = CurrentHealth;
      CurrentHealth -= parHealth;

      if (CurrentHealth < 0)
        CurrentHealth = 0;

      int damageAmount = healthBefore - CurrentHealth;
      if (damageAmount > 0)
        OnTakeHealth?.Invoke(damageAmount);

      if (CurrentHealth <= 0)
        OnInstantlyKill?.Invoke();
    }

    public void InstantlyKill()
    {
      CurrentHealth = 0;

      OnTakeHealth?.Invoke(_maxHealth);

      OnInstantlyKill?.Invoke();
    }

    //======================================
  }
}
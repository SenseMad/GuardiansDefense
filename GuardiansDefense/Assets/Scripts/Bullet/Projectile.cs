using UnityEngine;

using GuardiansDefense.Enemy;
using GuardiansDefense.HealthManager;

namespace GuardiansDefense.Towers
{
  public class Projectile : MonoBehaviour
  {
    [SerializeField, Min(0)] private float _speed;

    [SerializeField] private Transform _hitEffect;

    //--------------------------------------

    private EnemyAgent target;

    private int damage;

    //======================================

    private void Update()
    {
      Move();
    }

    //======================================

    private void Move()
    {
      if (target == null)
      {
        Destroy(gameObject);
        return;
      }

      Vector3 direction = target.transform.position - transform.position;
      float distanceToTarget = _speed * Time.deltaTime;

      if (direction.magnitude <= distanceToTarget)
      {
        if (target.TryGetComponent(out Health parHealth))
        {
          parHealth.TakeDamage(damage);
        }

        HitEffect();

        Destroy(gameObject);
        return;
      }

      transform.Translate(direction.normalized * distanceToTarget, Space.World);
    }

    private void HitEffect()
    {
      if (_hitEffect != null)
      {
        Transform hitEffect = Instantiate(_hitEffect, transform.position, transform.rotation);
        Destroy(hitEffect.gameObject, 2.0f);
      }
    }

    //======================================

    public void Init(EnemyAgent parTarget, int parDamage)
    {
      target = parTarget;
      damage = parDamage;
    }

    //======================================
  }
}
using UnityEngine;

namespace GuardiansDefense.Enemy
{
  [System.Serializable]
  public class EnemyConfig
  {
    [SerializeField, Min(0)] private float _moveSpeed = 9;

    [SerializeField, Min(0)] private int _damage;

    //======================================

    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }

    public int Damage { get => _damage; set => _damage = value; }

    //======================================
  }
}
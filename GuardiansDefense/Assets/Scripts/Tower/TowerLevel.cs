using UnityEngine;

using GuardiansDefense.Towers.Data;

namespace GuardiansDefense.Towers
{
  public class TowerLevel : MonoBehaviour
  {
    [SerializeField] private TowerLevelData _levelData;

    [Space(10)]
    [SerializeField] private Transform _turretTransform;

    [SerializeField] private Transform[] _pointsShot;

    [SerializeField] private Projectile _projectilePrefab;

    //======================================

    public TowerLevelData LevelData => _levelData;

    public Transform TurretTransform => _turretTransform;

    public Transform[] PointsShot => _pointsShot;

    public Projectile ProjectilePrefab => _projectilePrefab;

    //======================================
  }
}
using UnityEngine;

using GuardiansDefense.Enemy;

namespace GuardiansDefense.Towers
{
  [RequireComponent(typeof(TowerUpgrade))]
  public class Tower : MonoBehaviour
  {
    [Space(10)]
    [SerializeField, Min(0)] private float _turnSpeed;

    [SerializeField] private LayerMask _isShootingLayer;

    [Space(10)]
    [SerializeField] private TowerGhost _towerGhost;

    //--------------------------------------

    private EnemyAgent currentTarget;

    private float lastShotTime;

    private Collider[] targets;

    //======================================

    [field: SerializeField] public TowerUpgrade TowerUpgrade { get; private set; }

    public TowerPlacement TowerPlacement { get; set; }

    public TowerGhost TowerGhost => _towerGhost;

    //======================================

    private void Awake()
    {
      TowerUpgrade = GetComponent<TowerUpgrade>();
    }

    private void Start()
    {
      targets = new Collider[25];
    }

    private void Update()
    {
      currentTarget = DetectTarget();

      LookOnTarget();

      PerformAttack();
    }

    //======================================

    public void Init()
    {
      TowerUpgrade.SetLevel(0);
    }

    private EnemyAgent DetectTarget()
    {
      int hitCount = Physics.OverlapSphereNonAlloc(transform.position, TowerUpgrade.CurrentTowerLevel.LevelData.Distance, targets, _isShootingLayer);

      if (hitCount > 0)
      {
        EnemyAgent nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        for (int i = 0; i < hitCount; i++)
        {
          if (targets[i] == null)
            continue;

          EnemyAgent enemy = targets[i].GetComponentInParent<EnemyAgent>();

          if (enemy != null)
          {
            if (enemy.GetDistanceLastPoint() < nearestDistance)
            {
              nearestDistance = enemy.GetDistanceLastPoint();
              nearestEnemy = enemy;
            }
          }
        }

        return nearestEnemy;
      }

      return null;
    }

    private void LookOnTarget()
    {
      if (currentTarget == null)
        return;

      Transform turret = TowerUpgrade.CurrentTowerLevel.TurretTransform;

      Vector3 direction = currentTarget.transform.position - turret.position;
      Quaternion lookRotation = Quaternion.LookRotation(direction);

      Vector3 rotation = Quaternion.Lerp(turret.rotation, lookRotation, _turnSpeed * Time.deltaTime).eulerAngles;
      turret.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);
    }

    private void PerformAttack()
    {
      if (currentTarget == null)
        return;

      if (!(Time.time - lastShotTime > 60.0f / TowerUpgrade.CurrentTowerLevel.LevelData.ShotsPerMinutes))
        return;

      TowerLevel turret = TowerUpgrade.CurrentTowerLevel;

      for (int i = 0; i < turret.PointsShot.Length; i++)
      {
        Projectile projectile = Instantiate(turret.ProjectilePrefab, turret.PointsShot[i].position, turret.PointsShot[i].rotation);
        projectile.Init(currentTarget, TowerUpgrade.CurrentTowerLevel.LevelData.Damage);
      }

      lastShotTime = Time.time;
    }

    //======================================

    private void OnDrawGizmos()
    {
      if (TowerUpgrade == null)
        return;

      if (TowerUpgrade.CurrentTowerLevel == null)
        return;

      Gizmos.color = Color.yellow;
      Gizmos.DrawWireSphere(transform.position, TowerUpgrade.CurrentTowerLevel.LevelData.Distance);
    }

    //======================================
  }
}
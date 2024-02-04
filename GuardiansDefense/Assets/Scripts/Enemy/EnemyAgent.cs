using UnityEngine;

using GuardiansDefense.HealthManager;
using GuardiansDefense.Level;

namespace GuardiansDefense.Enemy
{
  public class EnemyAgent : MonoBehaviour
  {
    [SerializeField] private EnemyConfig _config;

    //--------------------------------------

    private Vector3[] waypoints;

    private Vector3 target;

    private int currentWaypointIndex;

    private Health health;

    private LevelManager levelManager;

    //======================================

    private void Awake()
    {
      health = GetComponent<Health>();

      levelManager = LevelManager.Instance;
    }

    private void Start()
    {
      target = waypoints[0];
    }

    private void OnEnable()
    {
      health.OnInstantlyKill += Die;
    }

    private void OnDisable()
    {
      health.OnInstantlyKill -= Die;
    }

    private void Update()
    {
      Move();
    }

    //======================================

    private void Move()
    {
      transform.position = Vector3.MoveTowards(transform.position, target, _config.MoveSpeed * Time.deltaTime);

      RotateTowards();

      if (Vector3.Distance(transform.position, target) <= 0.1f)
      {
        GetNextWaypoint();
      }
    }

    private void RotateTowards()
    {
      Vector3 direction = target - transform.position;
      direction.y = 0;

      if (direction != Vector3.zero)
      {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f * Time.deltaTime);
      }
    }

    private void GetNextWaypoint()
    {
      if (currentWaypointIndex >= waypoints.Length - 1)
      {
        EndPointReached();
        return;
      }

      currentWaypointIndex++;
      target = waypoints[currentWaypointIndex];
    }

    private void EndPointReached()
    {
      levelManager.PlayerHomeBase.Health.TakeDamage(_config.Damage);

      Die();
    }

    //======================================

    private void Die()
    {
      levelManager.ReduceNumberEnemies();

      Destroy(gameObject);
    }

    //======================================

    public float GetDistanceLastPoint()
    {
      float totalDistance = 0f;
      for (int i = currentWaypointIndex; i < waypoints.Length; i++)
      {
        totalDistance += Vector3.Distance(transform.position, waypoints[i]);
      }

      return totalDistance;
    }

    public void InitWaypoints(Vector3[] parWaypoints)
    {
      waypoints = parWaypoints;
    }

    //======================================
  }
}
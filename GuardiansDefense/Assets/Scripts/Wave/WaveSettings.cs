using UnityEngine;

using GuardiansDefense.Enemy;

namespace GuardiansDefense.Wave
{
  [System.Serializable]
  public class WaveSettings
  {
    [SerializeField] private EnemyAgent _agent;

    [SerializeField, Min(0)] private int _pathIndex;

    [SerializeField, Min(1)] private int _numberAgents;

    [SerializeField, Min(0)] private float _delaySpawn;

    //======================================

    public EnemyAgent Agent { get => _agent; private set => _agent = value; }

    public int PathIndex { get => _pathIndex; private set => _pathIndex = value; }

    public int NumberAgents { get => _numberAgents; private set => _numberAgents = value; }

    public float DelaySpawn { get => _delaySpawn; private set => _delaySpawn = value; }

    //======================================
  }
}
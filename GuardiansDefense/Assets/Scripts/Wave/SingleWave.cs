using System;
using System.Collections.Generic;
using UnityEngine;

using GuardiansDefense.Enemy;
using GuardiansDefense.Level;
using Zenject;

namespace GuardiansDefense.Wave
{
  public class SingleWave : MonoBehaviour
  {
    [SerializeField] private List<WaveSettings> _listWaveSettings;

    //--------------------------------------

    private bool isWaveActive = true;

    private float timerDelaySpawn;

    private int currentWaveSettingIndex = 0;

    private int agentsSpawned = 0;

    private Waypoints waypoints;

    private LevelManager levelManager;

    //======================================

    public event Action OnSingleWaveBegun;

    public event Action OnSingleWaveOver;

    //======================================

    private void Update()
    {
      TimerSpawnAgent();
    }

    //======================================

    [Inject]
    private void Construct(LevelManager parLevelManager, Waypoints parWaypoints)
    {
      levelManager = parLevelManager;
      waypoints = parWaypoints;
    }

    private void TimerSpawnAgent()
    {
      if (!isWaveActive)
        return;

      timerDelaySpawn += Time.deltaTime;
      WaveSettings waveSetting = _listWaveSettings[currentWaveSettingIndex];
      if (timerDelaySpawn >= waveSetting.DelaySpawn)
      {
        timerDelaySpawn = 0;

        SpawnAgent(waveSetting);
      }
    }        

    private void SpawnAgent(WaveSettings parWaveSetting)
    {
      if (agentsSpawned < parWaveSetting.NumberAgents)
      {
        CreateAgent(parWaveSetting);
        return;
      }

      FollowingEnemies();
    }

    private void FollowingEnemies()
    {
      if (currentWaveSettingIndex + 1 > _listWaveSettings.Count - 1)
        return;

      currentWaveSettingIndex++;
      agentsSpawned = 0;
      timerDelaySpawn = 0;
    }

    private void CreateAgent(WaveSettings parWaveSetting)
    {
      var wave = _listWaveSettings[currentWaveSettingIndex];

      Vector3 direction = waypoints.GetPointPosition(wave.PathIndex, 1) - waypoints.GetPointPosition(wave.PathIndex, 0);
      direction.y = 0;

      EnemyAgent agent = Instantiate(parWaveSetting.Agent, waypoints.GetPointPosition(wave.PathIndex, 0), Quaternion.LookRotation(direction));
      agent.InitWaypoints(waypoints.GetWaypoints(wave.PathIndex));
      agent.InitLevelMagager(levelManager);

      agentsSpawned++;

      EnemiesOver(parWaveSetting);
    }

    private void EnemiesOver(WaveSettings parWaveSetting)
    {
      if (currentWaveSettingIndex + 1 <= _listWaveSettings.Count - 1)
        return;

      if (agentsSpawned >= parWaveSetting.NumberAgents)
      {
        isWaveActive = false;
        OnSingleWaveOver?.Invoke();
        enabled = false;
      }
    }

    //======================================

    public int GetNumberAgents()
    {
      int count = 0;
      for (int i = 0; i < _listWaveSettings.Count; i++)
      {
        count += _listWaveSettings[i].NumberAgents;
      }

      return count;
    }

    //======================================
  }
}
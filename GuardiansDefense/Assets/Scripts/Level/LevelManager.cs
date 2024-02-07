using System;
using UnityEngine;
using Zenject;

using GuardiansDefense.Currencies;
using GuardiansDefense.Wave;

namespace GuardiansDefense.Level
{
  public class LevelManager : MonoBehaviour
  {
    [SerializeField] private int _startingCurrence = 100;

    //--------------------------------------

    private int totalNumberAgents;

    //======================================

    public WaveManager WaveManager {  get; private set; }

    public PlayerHomeBase PlayerHomeBase { get; private set; }

    public LevelState LevelState { get; private set; }

    public Ñurrency Ñurrency { get; private set; }

    //======================================

    public event Action OnLevelFailed;
    
    public event Action OnLevelComplete;

    //======================================

    private void Awake()
    {
      Ñurrency = new Ñurrency(_startingCurrence);
    }

    private void Start()
    {
      totalNumberAgents = WaveManager.GetNumberAgentsWaves();

      BuildingCompleted();
    }

    private void OnEnable()
    {
      WaveManager.OnWavesOver += AllSpawnCompleted;

      PlayerHomeBase.Health.OnInstantlyKill += LevelFailed;
    }

    private void OnDisable()
    {
      WaveManager.OnWavesOver -= AllSpawnCompleted;

      PlayerHomeBase.Health.OnInstantlyKill -= LevelFailed;
    }

    //======================================

    [Inject]
    private void Construct(WaveManager parWaveManager, PlayerHomeBase parPlayerHomeBase)
    {
      WaveManager = parWaveManager;
      PlayerHomeBase = parPlayerHomeBase;
    }

    public void BuildingCompleted()
    {
      ChangeLevelState(LevelState.SpawningEnemies);
    }

    public void AllSpawnCompleted()
    {
      ChangeLevelState(LevelState.AllEnemiesSpawned);
    }

    private void LevelFailed()
    {
      ChangeLevelState(LevelState.Failed);
    }

    private void LevelCompleted()
    {
      ChangeLevelState(LevelState.Completed);
    }

    public void ReduceNumberEnemies()
    {
      totalNumberAgents--;

      if (totalNumberAgents < 0)
        totalNumberAgents = 0;

      if (totalNumberAgents == 0 && LevelState == LevelState.AllEnemiesSpawned)
      {
        LevelCompleted();
      }
    }

    //======================================

    private void ChangeLevelState(LevelState parNewLevelState)
    {
      if (LevelState == parNewLevelState)
        return;

      LevelState = parNewLevelState;

      switch (parNewLevelState)
      {
        case LevelState.SpawningEnemies:
          WaveManager.StartWave();
          break;
        case LevelState.AllEnemiesSpawned:
          if (totalNumberAgents == 0)
          {
            LevelCompleted();
          }
          break;
        case LevelState.Failed:
          OnLevelFailed?.Invoke();
          break;
        case LevelState.Completed:
          OnLevelComplete?.Invoke();
          break;
      }
    }

    //======================================
  }
}
using System;
using UnityEngine;

namespace GuardiansDefense.Wave
{
  public class WaveManager : MonoBehaviour
  {
    [SerializeField] private MultiWave[] _listMultiWaves;

    //======================================

    public int CurrentWaveIndex { get; private set; } = -1;

    public int NumberWaves { get; private set; }

    //======================================

    public event Action<int> OnWaveBegun;

    public event Action<int> OnWaveCompleted;

    public event Action OnWavesOver;

    //======================================

    private void Awake()
    {
      InitMultiWaves();
    }

    private void OnEnable()
    {
      foreach (var multiWave in _listMultiWaves)
      {
        multiWave.OnWaveOver += NextWave;
      }
    }

    private void OnDisable()
    {
      foreach (var multiWave in _listMultiWaves)
      {
        multiWave.OnWaveOver -= NextWave;
      }
    }

    //======================================

    private void InitMultiWaves()
    {
      _listMultiWaves = GetComponentsInChildren<MultiWave>(true);

      foreach (var wave in _listMultiWaves)
      {
        wave.gameObject.SetActive(false);
      }

      NumberWaves = _listMultiWaves.Length;
    }

    private void NextWave()
    {
      if (CurrentWaveIndex < _listMultiWaves.Length - 1)
      {
        OnWaveCompleted?.Invoke(CurrentWaveIndex + 1);
        return;
      }
      
      OnWavesOver?.Invoke();
    }

    public void StartWave()
    {
      CurrentWaveIndex++;

      MultiWave wave = _listMultiWaves[CurrentWaveIndex];

      wave.gameObject.SetActive(true);

      OnWaveBegun?.Invoke(CurrentWaveIndex + 1);
    }

    public int GetNumberAgentsWaves()
    {
      int numberAgents = 0;
      for (int i = 0; i < _listMultiWaves.Length; i++)
      {
        numberAgents += _listMultiWaves[i].NumberAgents;
      }

      return numberAgents;
    }

    //======================================
  }
}
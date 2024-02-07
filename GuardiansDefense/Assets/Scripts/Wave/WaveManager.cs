using System;
using UnityEngine;

using GuardiansDefense.Level;

namespace GuardiansDefense.Wave
{
  public class WaveManager : MonoBehaviour
  {
    [SerializeField] private MultiWave[] _listMultiWaves;

    //======================================

    public int CurrentWaveIndex { get; private set; } = -1;

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
        multiWave.OnWaveOver += WaveOver;
      }
    }

    private void OnDisable()
    {
      foreach (var multiWave in _listMultiWaves)
      {
        multiWave.OnWaveOver -= WaveOver;
      }
    }

    //======================================

    private void InitMultiWaves()
    {
      _listMultiWaves = GetComponentsInChildren<MultiWave>(true);

      for (int i = 0; i < _listMultiWaves.Length; i++)
      {
        _listMultiWaves[i].gameObject.SetActive(false);
      }

      _listMultiWaves[0].gameObject.SetActive(true);
    }

    private void WaveOver()
    {
      OnWaveCompleted?.Invoke(CurrentWaveIndex + 1);

      NextWave();
    }

    private void NextWave()
    {
      _listMultiWaves[CurrentWaveIndex].gameObject.SetActive(false);

      if (CurrentWaveIndex + 1 > _listMultiWaves.Length - 1)
        OnWavesOver?.Invoke();
    }

    public void StartWave()
    {
      InitWave();
    }

    private void InitWave()
    {
      CurrentWaveIndex++;

      Debug.Log($"Инициализация волны: {CurrentWaveIndex + 1}");

      MultiWave wave = _listMultiWaves[CurrentWaveIndex];

      wave.gameObject.SetActive(true);

      OnWaveBegun?.Invoke(CurrentWaveIndex + 1);
    }

    public int GetNumberAgentsWaves()
    {
      int numberAgents = 0;
      for (int i = 0; i < _listMultiWaves.Length; i++)
      {
        numberAgents += _listMultiWaves[i].GetNumberAgents();
      }

      return numberAgents;
    }

    //======================================
  }
}
using System;
using System.Collections.Generic;
using UnityEngine;

using GuardiansDefense.Enemy;

namespace GuardiansDefense.Wave
{
  public class MultiWave : MonoBehaviour
  {
    public List<EnemyAgent> agentsWave = new List<EnemyAgent>();

    //--------------------------------------
    
    private bool[] completedSingleWaves;

    //======================================

    public SingleWave[] SingleWaves { get; private set; }

    public int NumberAgents { get; private set; }

    //======================================

    public event Action OnWaveOver;

    //======================================

    private void Awake()
    {
      SingleWaves = GetComponents<SingleWave>();

      completedSingleWaves = new bool[SingleWaves.Length];
    }

    private void OnEnable()
    {
      foreach (var singleWave in SingleWaves)
      {
        singleWave.OnSingleWaveOver += WaveOver;
      }
    }

    private void OnDisable()
    {
      foreach (var singleWave in SingleWaves)
      {
        singleWave.OnSingleWaveOver -= WaveOver;
      }

      NumberAgents = GetNumberAgents();
    }

    //======================================

    private void WaveOver()
    {
      for (int i = 0; i < completedSingleWaves.Length; i++)
      {
        if (completedSingleWaves[i])
          continue;

        completedSingleWaves[i] = true;
        break;
      }

      foreach (var completedSubWave in completedSingleWaves)
      {
        if (!completedSubWave)
          return;
      }

      OnWaveOver?.Invoke();
      gameObject.SetActive(false);
    }

    //======================================
    
    public int GetNumberAgents()
    {
      int count = 0;
      for (int i = 0; i < SingleWaves.Length; i++)
      {
        count += SingleWaves[i].GetNumberAgents();
      }

      return count;
    }

    //======================================
  }
}
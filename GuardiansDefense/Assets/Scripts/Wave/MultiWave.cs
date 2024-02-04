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

    private SingleWave[] singleWaves;
    
    private bool[] completedSingleWaves;

    //======================================

    public event Action OnWaveOver;

    //======================================

    private void Awake()
    {
      singleWaves = GetComponents<SingleWave>();

      completedSingleWaves = new bool[singleWaves.Length];
    }

    private void OnEnable()
    {
      foreach (var singleWave in singleWaves)
      {
        singleWave.OnSingleWaveOver += WaveOver;
      }
    }

    private void OnDisable()
    {
      foreach (var singleWave in singleWaves)
      {
        singleWave.OnSingleWaveOver -= WaveOver;
      }
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
      enabled = false;
    }

    //======================================
    
    public int GetNumberAgents()
    {
      int count = 0;
      for (int i = 0; i < singleWaves.Length; i++)
      {
        count += singleWaves[i].GetNumberAgents();
      }

      return count;
    }

    //======================================
  }
}
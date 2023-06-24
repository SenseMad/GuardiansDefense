using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GuardianDefence.Wave
{
  public class MultiWave : Wave
  {
    public override int GetEnemiesCount()
    {
      return 10;
    }

    private List<SingleWave> listSingleWave = new List<SingleWave>();

    //======================================

    private void Awake()
    {
      AddSingleWave();
    }

    //======================================

    private void AddSingleWave()
    {
      var singleWave = GetComponents<SingleWave>();
      for (int i = 0; i < singleWave.Length; i++)
      {
        listSingleWave.Add(singleWave[i]);
      }
    }

    //======================================

    public override void SpawnEnemies()
    {
      for (int i = 0; i < listSingleWave.Count; i++)
      {
        StartCoroutine(listSingleWave[i].SpawnEnemies());
      }
    }

    public override void StopSpawnEnemies()
    {
      /*for (int i = 0; i < listSingleWave.Count; i++)
      {
        StopCoroutine(listSingleWave[i].SpawnEnemies());
      }*/
    }

    //======================================
  }
}
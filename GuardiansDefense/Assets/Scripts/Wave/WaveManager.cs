using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GuardianDefence.Wave
{
  public class WaveManager : MonoBehaviour
  {
    [SerializeField, Tooltip("Список волн")]
    private List<Wave> _listWaves;

    [SerializeField, Tooltip("Список точек спавна")]
    private List<Transform> _listSpawnPoints;

    //--------------------------------------



    //======================================

    /// <summary>
    /// Текущая волна
    /// </summary>
    public int CurrentWave { get; private set; }

    //======================================

    private void Start()
    {
      _listWaves[0].SpawnEnemies();
    }

    //======================================

    /// <summary>
    /// Добавить волну
    /// </summary>
    public void AddWave()
    {
      CurrentWave++;
    }

    //======================================
  }
}
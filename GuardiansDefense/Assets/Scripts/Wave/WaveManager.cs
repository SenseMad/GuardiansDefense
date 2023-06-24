using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GuardianDefence.Wave
{
  public class WaveManager : MonoBehaviour
  {
    [SerializeField, Tooltip("������ ����")]
    private List<Wave> _listWaves;

    [SerializeField, Tooltip("������ ����� ������")]
    private List<Transform> _listSpawnPoints;

    //--------------------------------------



    //======================================

    /// <summary>
    /// ������� �����
    /// </summary>
    public int CurrentWave { get; private set; }

    //======================================

    private void Start()
    {
      _listWaves[0].SpawnEnemies();
    }

    //======================================

    /// <summary>
    /// �������� �����
    /// </summary>
    public void AddWave()
    {
      CurrentWave++;
    }

    //======================================
  }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GuardianDefence.Wave
{
  public abstract class Wave : MonoBehaviour
  {
    /// <summary>
    /// ����� ������
    /// </summary>
    public abstract void SpawnEnemies();

    /// <summary>
    /// ���������� ����� ������
    /// </summary>
    public abstract void StopSpawnEnemies();

    /// <summary>
    /// �������� ���������� ������
    /// </summary>
    public abstract int GetEnemiesCount();

    //======================================



    //======================================



    //======================================
  }
}
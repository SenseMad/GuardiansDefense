using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GuardianDefence.Wave
{
  public abstract class Wave : MonoBehaviour
  {
    /// <summary>
    /// Спавн врагов
    /// </summary>
    public abstract void SpawnEnemies();

    /// <summary>
    /// Остановить спавн врагов
    /// </summary>
    public abstract void StopSpawnEnemies();

    /// <summary>
    /// Получить количество врагов
    /// </summary>
    public abstract int GetEnemiesCount();

    //======================================



    //======================================



    //======================================
  }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GuardianDefence.Wave
{
  public class SingleWave : MonoBehaviour
  {
    /*[Header("СПАВН")]
    [SerializeField, Tooltip("Индекс точки спавна")]
    private int _indexSpawnPoint;*/

    [Header("ВРАГИ")]
    [SerializeField, Tooltip("Список врагов")]
    private List<SpawnEnemy> _listEnemies;

    //======================================



    //======================================



    //======================================

    public IEnumerator SpawnEnemies()
    {
      for (int i = 0; i < _listEnemies.Count; i++)
      {
        for (int j = 0; j < _listEnemies[i].numberEnemies; j++)
        {
          Debug.Log($"{_listEnemies[i].indexSpawnPoint} - {j}");
          yield return new WaitForSeconds(_listEnemies[i].delaySpawn);
        }

        yield return new WaitForSeconds(_listEnemies[i].delayBetween);
      }
    }

    //======================================

    [System.Serializable]
    public class SpawnEnemy
    {
      public Enemy enemy;

      /// <summary>
      /// Индекс точки спавна
      /// </summary>
      public int indexSpawnPoint;

      /// <summary>
      /// Количество врагов
      /// </summary>
      public int numberEnemies;

      /// <summary>
      /// Задержка спавна врагов
      /// </summary>
      public float delaySpawn;

      /// <summary>
      /// Задержка между появлением следующих врагов
      /// </summary>
      public float delayBetween;
    }

    //======================================
  }
}
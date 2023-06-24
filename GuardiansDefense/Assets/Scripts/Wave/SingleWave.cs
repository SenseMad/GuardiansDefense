using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GuardianDefence.Wave
{
  public class SingleWave : MonoBehaviour
  {
    /*[Header("�����")]
    [SerializeField, Tooltip("������ ����� ������")]
    private int _indexSpawnPoint;*/

    [Header("�����")]
    [SerializeField, Tooltip("������ ������")]
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
      /// ������ ����� ������
      /// </summary>
      public int indexSpawnPoint;

      /// <summary>
      /// ���������� ������
      /// </summary>
      public int numberEnemies;

      /// <summary>
      /// �������� ������ ������
      /// </summary>
      public float delaySpawn;

      /// <summary>
      /// �������� ����� ���������� ��������� ������
      /// </summary>
      public float delayBetween;
    }

    //======================================
  }
}
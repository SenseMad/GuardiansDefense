using System;
using UnityEngine;
using Zenject;

using GuardiansDefense.Level;
using GuardiansDefense.Wave;

namespace GuardiansDefense.Towers
{
  public class TowerUpgrade : MonoBehaviour
  {
    [SerializeField] private TowerLevel[] _levels;

    //--------------------------------------

    private int currentLevel;

    public TowerLevel currentTowerLevel;

    private LevelManager levelManager;

    //======================================

    public TowerLevel CurrentTowerLevel => currentTowerLevel;

    //======================================

    public event Action<TowerLevel> OnUpgrade;

    public event Action OnSell;

    //======================================

    private void Awake()
    {
      CreateTowerLevels();
    }

    //======================================

    /*[Inject]
    private void Construct(LevelManager parLevelManager)
    {
      levelManager = parLevelManager;
    }*/

    private void CreateTowerLevels()
    {
      _levels = GetComponentsInChildren<TowerLevel>();

      for (int i = 0; i < _levels.Length; i++)
      {
        _levels[i].gameObject.SetActive(false);
      }

      _levels[0].gameObject.SetActive(true);
      currentTowerLevel = _levels[0];
    }

    //======================================

    public void SetLevel(int parLevel)
    {
      if (parLevel >= _levels.Length || parLevel < 0)
        return;

      currentLevel = parLevel;

      if (currentTowerLevel != null)
      {
        currentTowerLevel.gameObject.SetActive(false);
      }

      currentTowerLevel = _levels[currentLevel];

      currentTowerLevel.gameObject.SetActive(true);

      OnUpgrade?.Invoke(currentTowerLevel);
    }

    public void Upgrade()
    {
      SetLevel(currentLevel + 1);
    }

    public void Sell()
    {
      OnSell?.Invoke();

      Destroy(gameObject);
    }

    //======================================

    public bool IsNextUpgrade()
    {
      return currentLevel < _levels.Length - 1;
    }

    public TowerLevel GetNextLevel()
    {
      if (currentLevel + 1 >= _levels.Length)
        return null;

      return _levels[currentLevel + 1];
    }

    public int GetSellLevel()
    {
      return GetSellLevel(currentLevel);
    }

    public int GetSellLevel(int parLevel)
    {
      //Debug.Log($"{levelManager}");

      /*if (levelManager.LevelState == LevelState.Building)
      {
        int cost = 0;
        for (int i = 0; i <= parLevel; i++)
        {
          cost += _levels[i].LevelData.Price;
        }

        return cost;
      }*/

      return _levels[parLevel].LevelData.GetSellPrice();
    }

    //======================================
  }
}
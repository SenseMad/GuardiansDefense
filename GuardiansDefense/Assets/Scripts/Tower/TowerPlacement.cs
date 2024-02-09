using UnityEngine;
using Zenject;

using GuardiansDefense.Level;

namespace GuardiansDefense.Towers
{
  public class TowerPlacement : MonoBehaviour
  {
    [SerializeField] private Tower _currentTower;

    //--------------------------------------

    private LevelManager levelManager;

    //======================================

    public Tower CurrentTower => _currentTower;

    //======================================

    [Inject]
    private void Construct(LevelManager parLevelManager)
    {
      levelManager = parLevelManager;
    }

    //======================================

    public void CreateTower(Tower parTower, Vector3 parPosition)
    {
      if (_currentTower != null)
        return;

      TowerUpgrade towerUpgrade = parTower.TowerUpgrade;

      if (!levelManager.—urrency.CanAfford(towerUpgrade.CurrentTowerLevel.LevelData.Price))
        return;

      levelManager.—urrency.Take—urrency(towerUpgrade.CurrentTowerLevel.LevelData.Price);

      _currentTower = Instantiate(parTower, parPosition, transform.rotation);
      _currentTower.TowerPlacement = this;
      _currentTower.TowerUpgrade.SetLevelManager(levelManager);
    }

    //======================================
  }
}
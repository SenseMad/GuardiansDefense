using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

using GuardiansDefense.Towers;
using GuardiansDefense.Level;

namespace GuardiansDefense.UI
{
  public class TowerUI : MenuUI
  {
    [SerializeField] private Panel _panel;

    [Space(10)]
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private TextMeshProUGUI _rangeText;

    [Space(10)]
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Button _removeButton;

    [Space(10)]
    [SerializeField] private TextMeshProUGUI _upgradeTowerPriceText;
    [SerializeField] private TextMeshProUGUI _removeTowerPriceText;

    //--------------------------------------

    private Tower selectedTower;

    private LevelManager levelManager;

    //======================================

    public event Action OnRemove;

    //======================================

    private void OnEnable()
    {
      _upgradeButton.onClick.AddListener(UpgradeTower);

      _removeButton.onClick.AddListener(RemoveTower);
    }

    private void OnDisable()
    {
      _upgradeButton.onClick.RemoveListener(UpgradeTower);

      _removeButton.onClick.RemoveListener(RemoveTower);
    }

    //======================================

    [Inject]
    private void Construct(LevelManager parLevelManager)
    {
      levelManager = parLevelManager;
    }

    //======================================

    public void Show(TowerPlacement parTowerPlacement)
    {
      if (!_panel.IsShow)
        panelController.Show(_panel);

      selectedTower = parTowerPlacement.CurrentTower;

      UpdateText();

      UpdateUpgradeButton();
    }

    public void Hide()
    {
      panelController.Hide(_panel);

      selectedTower = null;
    }

    //======================================

    public void UpgradeTower()
    {
      if (!selectedTower)
        return;

      TowerUpgrade towerUpgrade = selectedTower.TowerUpgrade;

      if (!levelManager.Ñurrency.CanAfford(towerUpgrade.GetNextLevel().LevelData.Price))
        return;

      towerUpgrade.Upgrade();

      levelManager.Ñurrency.TakeÑurrency(towerUpgrade.GetNextLevel().LevelData.Price);

      UpdateText();

      UpdateUpgradeButton();
    }

    public void RemoveTower()
    {
      if (!selectedTower)
        return;

      levelManager.Ñurrency.AddÑurrency(selectedTower.TowerUpgrade.GetSellLevel());

      selectedTower.TowerUpgrade.Sell();

      OnRemove?.Invoke();
    }

    //======================================

    private void UpdateUpgradeButton()
    {
      if (!selectedTower)
        return;

      if (!selectedTower.TowerUpgrade.IsNextUpgrade())
        _upgradeButton.gameObject.SetActive(false);
      else
        _upgradeButton.gameObject.SetActive(true);
    }

    private void UpdateText()
    {
      if (!selectedTower)
        return;

      UpdateText(selectedTower.TowerUpgrade.CurrentTowerLevel);
    }

    private void UpdateText(TowerLevel parTowerLevel)
    {
      _damageText.text = $"Damage: {parTowerLevel.LevelData.Damage}";
      _rangeText.text = $"Range: {parTowerLevel.LevelData.Distance}";
      // Ñêîðîñòü ñòðåëüáû

      TowerLevel towerLevel = selectedTower.TowerUpgrade.GetNextLevel();

      _upgradeTowerPriceText.text = $"{towerLevel.LevelData.Price}";
      _removeTowerPriceText.text = $"{selectedTower.TowerUpgrade.GetSellLevel()}";
    }

    //======================================
  }
}
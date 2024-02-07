using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using GuardiansDefense.Towers;

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
      //Debug.Log($"{panelController}");

      panelController.Hide(_panel);

      selectedTower = null;
    }

    //======================================

    public void UpgradeTower()
    {
      if (!selectedTower)
        return;

      selectedTower.TowerUpgrade.Upgrade();

      UpdateText();

      UpdateUpgradeButton();
    }

    public void RemoveTower()
    {
      if (!selectedTower)
        return;

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
      // Скорость стрельбы

      _upgradeTowerPriceText.text = $"{parTowerLevel.LevelData.Price}";
      _removeTowerPriceText.text = $"{selectedTower.TowerUpgrade.GetSellLevel()}";
    }

    //======================================
  }
}
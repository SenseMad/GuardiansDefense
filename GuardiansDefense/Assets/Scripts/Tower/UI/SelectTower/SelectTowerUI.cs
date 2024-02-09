using System;
using UnityEngine;

using GuardiansDefense.Towers;

namespace GuardiansDefense.UI.SelectTower
{
  public class SelectTowerUI : MonoBehaviour
  {
    [SerializeField] private Tower[] _towers;

    [SerializeField] private SelectTowerButton _prefabButton;

    [SerializeField] private Transform _container;

    //--------------------------------------

    private SelectTowerButton[] selectTowerButtons;

    private SelectTowerButton currentSelectedTowerButton;

    //======================================

    public event Action OnButtonSelected;

    //======================================

    private void Start()
    {
      Initialize();
    }

    private void OnDisable()
    {
      foreach (var button in selectTowerButtons)
      {
        button.OnButtonSelected -= SelectTowerButton_OnButtonSelected;
      }
    }

    //======================================

    private void SelectTowerButton_OnButtonSelected(SelectTowerButton parSelectTowerButton)
    {
      if (currentSelectedTowerButton != null)
      {
        if (currentSelectedTowerButton == parSelectTowerButton)
        {
          currentSelectedTowerButton.UnSelection();
          currentSelectedTowerButton = null;
          return;
        }

        currentSelectedTowerButton.UnSelection();
      }

      currentSelectedTowerButton = parSelectTowerButton;

      currentSelectedTowerButton.Selection();
    }

    private void Initialize()
    {
      selectTowerButtons = new SelectTowerButton[_towers.Length];

      for (int i = 0; i < _towers.Length; i++)
      {
        SelectTowerButton selectTowerButton = Instantiate(_prefabButton, _container);
        selectTowerButton.OnButtonSelected += SelectTowerButton_OnButtonSelected;
        selectTowerButton.Initialize(_towers[i]);

        selectTowerButtons[i] = selectTowerButton;
      }
    }

    //======================================



    //======================================
  }
}
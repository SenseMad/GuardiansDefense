using System;
using UnityEngine;
using UnityEngine.InputSystem;

using GuardiansDefense.Towers;
using GuardiansDefense.InputManager;
using GuardiansDefense.UI;

namespace GuardiansDefense.BuildSystem
{
  public class PlacementSystem : SingletonInSceneNoInstance<PlacementSystem>
  {
    [SerializeField] private Tower tower;

    [SerializeField] private TowerUI _towerUI;

    //--------------------------------------

    private BuildInputManager buildInputManager;

    private InputHandler inputHandler;

    private Tower towerToBuild;

    private TowerPlacement selectedTowerPlacement;

    //======================================

    public event Action<Tower> OnSelectedTower;

    public event Action<TowerPlacement> OnSelectedTowerPlacement;

    //======================================

    protected override void Awake()
    {
      base.Awake();

      inputHandler = InputHandler.Instance;

      SelectTower(tower);
    }

    private void Start()
    {
      buildInputManager = BuildInputManager.Instance;
    }

    private void OnEnable()
    {
      inputHandler.IA_Player.Player.LeftMouse.performed += OnTryCreateTower;

      _towerUI.OnRemove += Deselect;
    }

    private void OnDisable()
    {
      inputHandler.IA_Player.Player.LeftMouse.performed -= OnTryCreateTower;

      _towerUI.OnRemove -= Deselect;
    }

    //======================================

    private void OnTryCreateTower(InputAction.CallbackContext obj)
    {
      Tower tower = buildInputManager.GetSelectedTower();

      if (tower != null)
      {
        SelectedTowerPlacement(tower.TowerPlacement);
        return;
      }

      Vector3 mousePositionTowerPlacement = buildInputManager.GetSelectedMapPositionTowerPlacement(out TowerPlacement parTowerPlacement);

      if (parTowerPlacement == null)
        return;

      /*if (parTowerPlacement.CurrentTower != null)
      {
        SelectedTowerPlacement(parTowerPlacement);
        return;
      }*/

      parTowerPlacement.CreateTower(towerToBuild, mousePositionTowerPlacement);
    }

    public void SelectTower(Tower parTower)
    {
      towerToBuild = parTower;

      Deselect();

      OnSelectedTower?.Invoke(parTower);
    }

    public void SelectedTowerPlacement(TowerPlacement parTowerPlacement)
    {
      /*if (towerToBuild != null)
        return;*/

      if (selectedTowerPlacement == parTowerPlacement)
      {
        Deselect();
        return;
      }

      selectedTowerPlacement = parTowerPlacement;

      _towerUI.Show(parTowerPlacement);

      OnSelectedTowerPlacement?.Invoke(parTowerPlacement);
    }

    private void Deselect()
    {
      _towerUI.Hide();

      selectedTowerPlacement = null;
    }

    //======================================
  }
}
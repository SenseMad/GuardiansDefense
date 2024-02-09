using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

using GuardiansDefense.Towers;
using GuardiansDefense.InputManager;
using GuardiansDefense.UI;
using GuardiansDefense.Level;

namespace GuardiansDefense.BuildSystem
{
  public class PlacementSystem : MonoBehaviour
  {
    [SerializeField] private Tower tower;

    [SerializeField] private TowerUI _towerUI;

    //--------------------------------------

    private BuildInputManager buildInputManager;

    private InputHandler inputHandler;

    private Tower towerToBuild;

    private TowerPlacement selectedTowerPlacement;

    private LevelManager levelManager;

    //======================================

    public event Action<Tower> OnSelectedTower;

    public event Action<TowerPlacement> OnSelectedTowerPlacement;

    //======================================

    private void Start()
    {
      SelectTower(tower);
    }

    private void OnEnable()
    {
      inputHandler.IA_Player.Player.LeftMouse.performed += OnTryCreateTower;

      _towerUI.OnRemove += Deselect;

      levelManager.WaveManager.OnWaveBegun += Deselect;
    }

    private void OnDisable()
    {
      inputHandler.IA_Player.Player.LeftMouse.performed -= OnTryCreateTower;

      _towerUI.OnRemove -= Deselect;

      levelManager.WaveManager.OnWaveBegun -= Deselect;
    }

    //======================================

    [Inject]
    private void Construct(InputHandler parInputHandler, BuildInputManager parBuildInputManager, LevelManager parLevelManager)
    {
      inputHandler = parInputHandler;
      buildInputManager = parBuildInputManager;
      levelManager = parLevelManager;
    }

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

      Deselect();
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

    private void Deselect(int parWave)
    {
      Deselect();
    }

    //======================================
  }
}
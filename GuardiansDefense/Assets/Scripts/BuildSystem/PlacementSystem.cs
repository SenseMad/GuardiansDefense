using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

using GuardiansDefense.Towers;
using GuardiansDefense.InputManager;
using GuardiansDefense.UI;
using GuardiansDefense.Level;
using GuardiansDefense.UI.SelectTower;

namespace GuardiansDefense.BuildSystem
{
  public class PlacementSystem : MonoBehaviour
  {
    [SerializeField] private TowerGhost _towerGhost;

    [SerializeField] private TowerUI _towerUI;

    [SerializeField] private SelectTowerUI _selectTowerUI;

    //--------------------------------------

    private BuildInputManager buildInputManager;

    private InputHandler inputHandler;

    private Tower towerToBuild;

    private TowerPlacement selectedTowerPlacement;

    private LevelManager levelManager;

    //======================================

    public event Action<TowerPlacement> OnSelectedTowerPlacement;

    //======================================

    private void Start()
    {
      DeselectUI();
    }

    private void OnEnable()
    {
      inputHandler.IA_Player.Player.LeftMouse.performed += OnTryCreateTower;

      inputHandler.IA_Player.Player.RightMouse.performed += RightMouse_performed;

      _towerUI.OnRemove += DeselectUI;

      levelManager.WaveManager.OnWaveBegun += DeselectUI;

      _selectTowerUI.OnButtonSelected += SelectTower;
      _selectTowerUI.OnButtonUnSelected += UnSelectTower;
    }

    private void OnDisable()
    {
      inputHandler.IA_Player.Player.LeftMouse.performed -= OnTryCreateTower;

      inputHandler.IA_Player.Player.RightMouse.performed -= RightMouse_performed;

      _towerUI.OnRemove -= DeselectUI;

      levelManager.WaveManager.OnWaveBegun -= DeselectUI;

      _selectTowerUI.OnButtonSelected -= SelectTower;
      _selectTowerUI.OnButtonUnSelected -= UnSelectTower;
    }

    private void Update()
    {
      if (_towerGhost == null)
        return;

      Vector3 mousePositionTowerPlacement = buildInputManager.GetPositionInstallTower(out TowerPlacement parTowerPlacement);

      _towerGhost.gameObject.SetActive(buildInputManager.YouCanInstallTower());

      _towerGhost.transform.position = mousePositionTowerPlacement;
    }

    //======================================

    [Inject]
    private void Construct(InputHandler parInputHandler, BuildInputManager parBuildInputManager, LevelManager parLevelManager)
    {
      inputHandler = parInputHandler;
      buildInputManager = parBuildInputManager;
      levelManager = parLevelManager;
    }

    //======================================

    private void RightMouse_performed(InputAction.CallbackContext obj)
    {
      if (towerToBuild != null)
      {
        UnSelectTower();

        _selectTowerUI.ButtonUnSelected();
      }

      if (selectedTowerPlacement != null)
      {
        DeselectUI();
      }
    }

    private void OnTryCreateTower(InputAction.CallbackContext obj)
    {
      Tower tower = buildInputManager.GetSelectedTower();

      if (tower != null)
      {
        SelectedTowerPlacementUI(tower.TowerPlacement);
        return;
      }
      
      if (towerToBuild == null)
        return;

      Vector3 mousePositionTowerPlacement = buildInputManager.GetPositionInstallTower(out TowerPlacement parTowerPlacement);

      if (parTowerPlacement == null)
        return;

      parTowerPlacement.CreateTower(towerToBuild, mousePositionTowerPlacement);

      DeselectUI();

      UnSelectTower();

      _selectTowerUI.ButtonUnSelected();
    }

    private void SelectTower(Tower parTower)
    {
      UnSelectTower();

      towerToBuild = parTower;

      _towerGhost = Instantiate(parTower.TowerGhost, Vector3.zero, Quaternion.identity);

      DeselectUI();
    }

    private void UnSelectTower()
    {
      towerToBuild = null;

      if (_towerGhost != null)
        Destroy(_towerGhost.gameObject);
    }

    private void SelectedTowerPlacementUI(TowerPlacement parTowerPlacement)
    {
      if (towerToBuild != null)
        return;

      if (selectedTowerPlacement == parTowerPlacement)
      {
        DeselectUI();
        return;
      }

      selectedTowerPlacement = parTowerPlacement;

      _towerUI.Show(parTowerPlacement);

      OnSelectedTowerPlacement?.Invoke(parTowerPlacement);
    }

    private void DeselectUI()
    {
      _towerUI.Hide();

      selectedTowerPlacement = null;
    }

    private void DeselectUI(int parWave)
    {
      DeselectUI();
    }

    //======================================
  }
}
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

    [SerializeField] private TowerInstallUI _towerInstallUI;

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

      _selectTowerUI.OnButtonSelected += SelectCreateTower;
      _selectTowerUI.OnButtonUnSelected += UnSelectTower;
    }

    private void OnDisable()
    {
      inputHandler.IA_Player.Player.LeftMouse.performed -= OnTryCreateTower;

      inputHandler.IA_Player.Player.RightMouse.performed -= RightMouse_performed;

      _towerUI.OnRemove -= DeselectUI;

      levelManager.WaveManager.OnWaveBegun -= DeselectUI;

      _selectTowerUI.OnButtonSelected -= SelectCreateTower;
      _selectTowerUI.OnButtonUnSelected -= UnSelectTower;
    }

    private void Update()
    {


      SelectCreateTowerGhost();
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

    private void SelectCreateTowerGhost()
    {
      if (_towerGhost == null)
        return;

      _towerGhost.gameObject.SetActive(buildInputManager.YouCanInstallTower());

      if (!_towerGhost.gameObject.activeSelf)
        return;

      TowerPlacement towerPlacement = buildInputManager.GetPositionInstallTower();

      if (towerPlacement == null)
        return;

      Transform towerPlacementTransform = towerPlacement.transform;

      _towerGhost.transform.position = towerPlacementTransform.position + Vector3.up * (_towerGhost.transform.localScale.y / 2f + towerPlacementTransform.localScale.y / 2f);
    }

    private void OnTryCreateTower(InputAction.CallbackContext obj)
    {
      if (_towerGhost == null)
      {
        Tower tower = buildInputManager.GetSelectedTower();

        if (tower != null)
        {
          SelectedTowerPlacementUI(tower.TowerPlacement);
          return;
        }
      }
      
      if (towerToBuild == null)
        return;

      TowerPlacement towerPlacement = buildInputManager.GetPositionInstallTower();

      if (towerPlacement == null)
        return;

      towerPlacement.CreateTower(towerToBuild);

      DeselectUI();

      UnSelectTower();

      _selectTowerUI.ButtonUnSelected();
    }

    private void SelectCreateTower(Tower parTower)
    {
      UnSelectTower();

      _towerInstallUI.Show();

      towerToBuild = parTower;

      _towerGhost = Instantiate(parTower.TowerGhost, Vector3.zero, Quaternion.identity);

      DeselectUI();
    }

    private void UnSelectTower()
    {
      towerToBuild = null;

      _towerInstallUI.Hide();

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
using UnityEngine;
using UnityEngine.InputSystem;

using GuardiansDefense.Towers;
using GuardiansDefense.InputManager;

namespace GuardiansDefense.BuildSystem
{
  public class PlacementSystem : MonoBehaviour
  {
    [SerializeField] private Tower tower;

    //--------------------------------------

    private BuildInputManager buildInputManager;

    private InputHandler inputHandler;

    private Tower towerToBuild;

    private TowerPlacement selectedTowerPlacement;

    //======================================

    public Tower TowerToBuild => towerToBuild;

    //======================================

    private void Awake()
    {
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
    }

    private void OnDisable()
    {
      inputHandler.IA_Player.Player.LeftMouse.performed -= OnTryCreateTower;
    }

    //======================================

    private void OnTryCreateTower(InputAction.CallbackContext obj)
    {
      Vector3 mousePosition = buildInputManager.GetSelectedMapPosition(out TowerPlacement parTowerPlacement);

      if (parTowerPlacement == null)
        return;

      if (parTowerPlacement.CurrentTower != null)
      {
        SelectedTowerPlacement(parTowerPlacement);
        return;
      }

      parTowerPlacement.CreateTower(towerToBuild, mousePosition);
    }

    public void SelectTower(Tower parTower)
    {
      towerToBuild = parTower;

      selectedTowerPlacement = null;
    }

    public void SelectedTowerPlacement(TowerPlacement parTowerPlacement)
    {
      if (towerToBuild != null)
        return;

      selectedTowerPlacement = parTowerPlacement;
    }

    //======================================
  }
}
using UnityEngine;

using GuardiansDefense.Towers;
using Unity.VisualScripting;

namespace GuardiansDefense.BuildSystem
{
  public class BuildInputManager : MonoBehaviour
  {
    [SerializeField] private LayerMask _placementLayermask;

    //--------------------------------------

    private new Camera camera;

    private Vector3 lastPosition;

    //======================================

    private void Awake()
    {
      camera = Camera.main;
    }

    //======================================

    /*public Vector3 GetPositionInstallTower(out TowerPlacement parTowerPlacement)
    {
      Ray ray = camera.ScreenPointToRay(GetMousePosition());
      parTowerPlacement = null;

      RaycastHit[] hits = Physics.RaycastAll(ray, float.MaxValue, _placementLayermask);

      if (hits.Length > 0)
      {
        foreach (var hitTowerPlacement in hits)
        {
          TowerPlacement towerPlacement = hitTowerPlacement.collider.GetComponentInParent<TowerPlacement>();

          if (!towerPlacement)
            continue;

          var breakingBlock = GetBreakingBlock(towerPlacement.transform.position);
          if (breakingBlock != null)
            return hitTowerPlacement.point;

          parTowerPlacement = towerPlacement;

          float heightAboveBlock = 1.0f;
          Vector3 indicatorPosition = hitTowerPlacement.collider.bounds.center + Vector3.up * heightAboveBlock;

          lastPosition = indicatorPosition;
          return lastPosition;
        }
      }

      return lastPosition;
    }*/

    public TowerPlacement GetPositionInstallTower()
    {
      Ray ray = camera.ScreenPointToRay(GetMousePosition());

      RaycastHit[] hits = new RaycastHit[10];
      int numberHits = Physics.RaycastNonAlloc(ray, hits, float.MaxValue, _placementLayermask);

      if (numberHits > 0)
      {
        foreach (var hitTowerPlacement in hits)
        {
          TowerPlacement towerPlacement = hitTowerPlacement.collider.GetComponentInParent<TowerPlacement>();

          if (towerPlacement == null)
            return null;

          if (towerPlacement.CurrentTower != null)
            return null;

          var breakingBlock = GetBreakingBlock(towerPlacement.transform.position);
          if (breakingBlock != null)
            return null;

          return towerPlacement;
        }
      }

      return null;
    }

    public bool YouCanInstallTower()
    {
      TowerPlacement towerPlacement = GetPositionInstallTower();

      return towerPlacement != null && towerPlacement.CurrentTower == null;
    }

    public Tower GetSelectedTower()
    {
      Ray ray = camera.ScreenPointToRay(GetMousePosition());

      if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue))
        return hit.collider.GetComponentInParent<Tower>();

      return null;
    }

    //======================================

    private BreakingBlock GetBreakingBlock(Vector3 parPositionParentBlock)
    {
      Ray ray = new(parPositionParentBlock, Vector3.up);
      if (Physics.Raycast(ray, out RaycastHit hit, 1))
        return hit.collider.GetComponent<BreakingBlock>();

      return null;
    }

    private Vector3 GetMousePosition()
    {
      Vector3 mousePos = Input.mousePosition;
      mousePos.z = camera.nearClipPlane;

      return mousePos;
    }

    //======================================
  }
}
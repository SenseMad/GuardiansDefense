using UnityEngine;

using GuardiansDefense.Towers;

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

    public Vector3 GetSelectedMapPositionTowerPlacement(out TowerPlacement parTowerPlacement)
    {
      Vector3 mousePos = Input.mousePosition;
      mousePos.z = camera.nearClipPlane;
      Ray ray = camera.ScreenPointToRay(mousePos);
      parTowerPlacement = null;

      if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, _placementLayermask))
      {
        TowerPlacement towerPlacement = hit.collider.GetComponentInParent<TowerPlacement>();
        parTowerPlacement = towerPlacement;

        if (!towerPlacement)
          return lastPosition;

        Ray rayUp = new Ray(hit.point, Vector3.up);
        if (Physics.Raycast(rayUp, out RaycastHit hitUp, 1))
          return lastPosition;

        float heightAboveBlock = 1.0f;
        Vector3 indicatorPosition = hit.collider.bounds.center + Vector3.up * heightAboveBlock;

        lastPosition = indicatorPosition;
      }

      return lastPosition;
    }

    /*public TowerPlacement GetSelectedTowerPlacement()
    {
      Vector3 mousePos = Input.mousePosition;
      mousePos.z = camera.nearClipPlane;
      Ray ray = camera.ScreenPointToRay(mousePos);

      if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue))
      {
        Tower tower = hit.collider.GetComponentInParent<Tower>();
        if (!tower)
          return null;

        Ray rayDown = new Ray(hit.point, -Vector3.up);
        if (Physics.Raycast(rayDown, out RaycastHit hitDown, 1))
        {
          TowerPlacement towerPlacement = hitDown.collider.GetComponent<TowerPlacement>();

          if (!towerPlacement)
            return null;

          return towerPlacement;
        }
      }

      return null;
    }*/

    public Tower GetSelectedTower()
    {
      Vector3 mousePos = Input.mousePosition;
      mousePos.z = camera.nearClipPlane;
      Ray ray = camera.ScreenPointToRay(mousePos);

      if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue))
      {
        Tower tower = hit.collider.GetComponentInParent<Tower>();

        return tower;
      }

      return null;
    }

    //======================================
  }
}
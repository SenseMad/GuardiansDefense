using UnityEngine;

using GuardiansDefense.Towers;

namespace GuardiansDefense.BuildSystem
{
  public class BuildInputManager : SingletonInSceneNoInstance<BuildInputManager>
  {
    [SerializeField] private LayerMask _placementLayermask;

    //--------------------------------------

    private new Camera camera;

    private Vector3 lastPosition;

    //======================================

    protected override void Awake()
    {
      base.Awake();

      camera = Camera.main;
    }

    //======================================

    public Vector3 GetSelectedMapPosition(out TowerPlacement parTowerPlacement)
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

    //======================================
  }
}
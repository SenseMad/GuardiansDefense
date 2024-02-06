using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GuardiansDefense.Towers
{
  public class TowerPlacement : MonoBehaviour
  {
    [SerializeField] private Tower _currentTower;

    //======================================

    public Tower CurrentTower => _currentTower;

    //======================================



    //======================================

    public void CreateTower(Tower parTower, Vector3 parPosition)
    {
      if (_currentTower != null)
        return;

      _currentTower = Instantiate(parTower, parPosition, transform.rotation);
      _currentTower.TowerPlacement = this;
    }

    //======================================



    //======================================
  }
}
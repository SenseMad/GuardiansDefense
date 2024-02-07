using UnityEngine;
using Zenject;

namespace GuardiansDefense.UI
{
  public class MenuUI : MonoBehaviour
  {
    protected PanelController panelController;

    //======================================

    protected virtual void Awake() { }

    //======================================

    [Inject]
    private void Construct(PanelController parPanelController)
    {
      panelController = parPanelController;
    }

    //======================================



    //======================================
  }
}
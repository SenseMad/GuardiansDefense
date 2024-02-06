using UnityEngine;

namespace GuardiansDefense.UI
{
  public class Panel : MonoBehaviour
  {
    [SerializeField] private GameObject _panelObject;

    [Space(10)]
    [SerializeField] private bool _isShow;

    //======================================

    private void Awake()
    {
      if (_panelObject == null)
        _panelObject = gameObject;
    }

    //======================================

    public bool IsShow
    {
      get => _isShow;
      set
      {
        _isShow = value;
        _panelObject.SetActive(value);
      }
    }

    //======================================

    public void Show()
    {
      IsShow = true;
    }

    public void Hide()
    {
      IsShow = false;
    }

    //======================================
  }
}

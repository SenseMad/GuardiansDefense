using System;
using System.Collections.Generic;
using UnityEngine;

namespace GuardiansDefense.UI
{
  public class PanelController : MonoBehaviour
  {
    [SerializeField] private Panel _currentActivePanel;

    //--------------------------------------

    public List<Panel> listAllOpenPanels = new();

    //======================================

    public void Show(Panel parPanel)
    {
      if (parPanel == null)
        return;

      _currentActivePanel = parPanel;

      listAllOpenPanels.Add(parPanel);

      _currentActivePanel.Show();
    }

    public void Hide(Panel parPanel)
    {
      if (parPanel == null)
        return;

      parPanel.Hide();

      listAllOpenPanels.Remove(parPanel);

      if (_currentActivePanel == parPanel)
      {
        _currentActivePanel = null;
      }
    }

    public void SetActive(Panel parPanel)
    {
      Hide(_currentActivePanel);

      Show(parPanel);
    }

    public void ClosingOrder()
    {
      if (listAllOpenPanels.Count == 0 || _currentActivePanel == null)
        return;

      Hide(_currentActivePanel);

      if (listAllOpenPanels.Count == 0)
      {
        _currentActivePanel = null;
        return;
      }

      _currentActivePanel = listAllOpenPanels[^1];
      _currentActivePanel.Show();
    }

    public void CloseAll()
    {
      for (int i = 0; i < listAllOpenPanels.Count; i++)
      {
        ClosingOrder();
      }
    }

    //======================================
  }
}
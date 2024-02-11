using System;
using UnityEngine;
using UnityEngine.UI;

using GuardiansDefense.Towers;

namespace GuardiansDefense.UI.SelectTower
{
  public class SelectTowerButton : MonoBehaviour
  {
    [SerializeField] private Button _selectButton;

    [SerializeField] private Image _towerImage;

    [SerializeField] private Color _colorSelectedButton;

    //--------------------------------------

    private Image buttonImage;

    private Color standartButtonColor;

    //======================================

    public Tower Tower { get; private set; }

    //======================================

    public event Action<SelectTowerButton> OnButtonSelected;

    //======================================

    private void Awake()
    {
      buttonImage = _selectButton.GetComponent<Image>();
    }

    private void OnEnable()
    {
      _selectButton.onClick.AddListener(PressingButton);
    }

    private void OnDisable()
    {
      _selectButton.onClick.RemoveListener(PressingButton);
    }

    //======================================

    private void PressingButton()
    {
      OnButtonSelected?.Invoke(this);
    }

    public void Selection()
    {
      buttonImage.color = _colorSelectedButton;
    }

    public void UnSelection()
    {
      buttonImage.color = standartButtonColor;
    }

    //======================================

    public void Initialize(Tower parTower)
    {
      standartButtonColor = buttonImage.color;

      Tower = parTower;
    }

    //======================================
  }
}
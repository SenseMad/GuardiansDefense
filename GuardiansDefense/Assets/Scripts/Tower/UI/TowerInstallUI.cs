using UnityEngine;
using UnityEngine.InputSystem;

namespace GuardiansDefense.UI
{
  public class TowerInstallUI : MonoBehaviour
  {
    [SerializeField] private Vector2 offset;

    //======================================

    private void Start()
    {
      Hide();
    }

    private void Update()
    {
      Vector2 mousePosition = Mouse.current.position.ReadValue();

      transform.position = mousePosition + offset;
    }

    //======================================

    public void Show()
    {
      gameObject.SetActive(true);
    }

    public void Hide()
    {
      gameObject.SetActive(false);
    }

    //======================================
  }
}
using UnityEngine;
using Zenject;

namespace GuardiansDefense.InputManager
{
  public class InputHandler : MonoBehaviour
  {
    public IA_Player IA_Player { get; private set; }

    //======================================

    private void Awake()
    {
      IA_Player = new IA_Player();
    }

    private void OnEnable()
    {
      IA_Player.Enable();
    }

    private void OnDisable()
    {
      IA_Player.Disable();
    }

    //======================================
  }
}
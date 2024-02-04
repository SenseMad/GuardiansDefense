using UnityEngine;

namespace GuardiansDefense.InputManager
{
  public class InputHandler : SingletonInGame<InputHandler>
  {
    public IA_Player IA_Player { get; private set; }

    //======================================

    protected override void Awake()
    {
      base.Awake();

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
using Zenject;

using GuardiansDefense.InputManager;

public class GlobalInstaller : MonoInstaller
{
  public override void InstallBindings()
  {
    Container.Bind<InputHandler>().FromNewComponentOnNewGameObject().AsSingle();
  }
}
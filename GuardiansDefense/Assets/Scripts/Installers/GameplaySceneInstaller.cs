using Zenject;

using GuardiansDefense.Level;
using GuardiansDefense.Wave;
using GuardiansDefense.BuildSystem;
using GuardiansDefense.UI;

public class GameplaySceneInstaller : MonoInstaller
{
  public override void InstallBindings()
  {
    //var levelManagerInstance = Container.InstantiatePrefabForComponent<LevelManager>(levelManager);

    //Container.Bind<LevelManager>().FromInstance(levelManagerInstance).AsSingle().NonLazy();

    //Container.Bind<LevelManager>().To<LevelManager>().FromNew().AsSingle();
    //Container.Bind<LevelManager>().AsSingle().NonLazy();
    Container.Bind<LevelManager>().FromComponentInHierarchy().AsSingle().NonLazy();

    Container.Bind<Waypoints>().FromComponentInHierarchy().AsSingle().NonLazy();

    Container.Bind<WaveManager>().FromComponentInHierarchy().AsSingle().NonLazy();

    Container.Bind<PlayerHomeBase>().FromComponentInHierarchy().AsSingle().NonLazy();

    Container.Bind<BuildInputManager>().FromComponentInHierarchy().AsSingle().NonLazy();

    Container.Bind<PanelController>().FromComponentInHierarchy().AsSingle().NonLazy();
  }
}
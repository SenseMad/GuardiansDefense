using GuardiansDefense.HealthManager;

namespace GuardiansDefense.Level
{
  public class PlayerHomeBase : SingletonInSceneNoInstance<PlayerHomeBase>
  {
    public Health Health { get; private set; }

    //======================================

    protected override void Awake()
    {
      base.Awake();

      Health = GetComponent<Health>();
    }

    //======================================
  }
}
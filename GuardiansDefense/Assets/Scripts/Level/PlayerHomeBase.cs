using UnityEngine;

using GuardiansDefense.HealthManager;

namespace GuardiansDefense.Level
{
  public class PlayerHomeBase : MonoBehaviour
  {
    [field: SerializeField] public Health Health { get; private set; }

    //======================================

    private void Awake()
    {
      Health = GetComponent<Health>();
    }

    //======================================
  }
}
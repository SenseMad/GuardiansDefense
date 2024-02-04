using UnityEngine;

namespace GuardiansDefense.Pool
{
  public class ObjectPooler : MonoBehaviour
  {
    public void ReturnToPool()
    {
      gameObject.SetActive(false);
    }
  }
}
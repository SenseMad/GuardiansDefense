using UnityEngine;

namespace GuardiansDefense.Wave
{
  public class Wave : MonoBehaviour
  {


    //======================================

    public virtual void Init() { }

    protected virtual void StartWave() { }

    protected virtual void StopWave() { }

    //======================================
  }
}
using UnityEngine;
using UnityEngine.UI;

namespace GuardiansDefense.HealthManager
{
  public class HealthVisualizer : MonoBehaviour
  {
    [SerializeField] private GameObject _healthBarObject;
    [SerializeField] private bool _visibleFullHealth;

    [Space(10), SerializeField] private Image _healthBar;

    //--------------------------------------

    private Health health;

    private new Camera camera;

    //======================================

    private void Awake()
    {
      health = GetComponentInParent<Health>();
    }

    private void Start()
    {
      camera = Camera.main;

      UpdateHealtdBar();
    }

    private void OnEnable()
    {
      health.OnChangeHealth += UpdateHealthBar;
    }

    private void OnDisable()
    {
      health.OnChangeHealth -= UpdateHealthBar;
    }

    private void LateUpdate()
    {
      transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
    }

    //======================================

    private void UpdateHealthBar(int parHealth)
    {
      UpdateHealtdBar();
    }

    private void UpdateHealtdBar()
    {
      _healthBarObject.SetActive(!(health.CurrentHealth >= health.MaxHealth && !_visibleFullHealth));

      _healthBar.fillAmount = (float)health.CurrentHealth / (float)health.MaxHealth;
    }

    //======================================
  }
}
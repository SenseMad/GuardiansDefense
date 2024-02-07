using TMPro;
using UnityEngine;
using Zenject;

using GuardiansDefense.Level;

namespace GuardiansDefense.UI
{
  public class GameUI : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _healthText;

    [SerializeField] private TextMeshProUGUI _numberWaveText;

    [SerializeField] private TextMeshProUGUI _coinsText;

    //--------------------------------------

    private LevelManager levelManager;

    //======================================

    private void Start()
    {
      UpdateHealthText();

      UpdateCoinsText();
    }

    private void OnEnable()
    {
      levelManager.PlayerHomeBase.Health.OnChangeHealth += UpdateHealthText;

      levelManager.—urrency.OnChange—urrency += UpdateCoinsText;
    }

    private void OnDisable()
    {
      levelManager.PlayerHomeBase.Health.OnChangeHealth -= UpdateHealthText;

      levelManager.—urrency.OnChange—urrency -= UpdateCoinsText;
    }

    //======================================

    [Inject]
    private void Construct(LevelManager parLevelManager)
    {
      levelManager = parLevelManager;
    }

    private void UpdateHealthText()
    {
      UpdateHealthText(levelManager.PlayerHomeBase.Health.CurrentHealth);
    }

    private void UpdateHealthText(int parHealth)
    {
      _healthText.text = $"{parHealth}";
    }

    private void UpdateCoinsText()
    {
      UpdateCoinsText(levelManager.—urrency.Current—urrency);
    }

    private void UpdateCoinsText(int parCurrence)
    {
      _coinsText.text = $"{parCurrence}";
    }

    //======================================
  }
}
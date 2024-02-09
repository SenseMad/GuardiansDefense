using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

using GuardiansDefense.Level;

namespace GuardiansDefense.UI
{
  public class GameUI : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _healthText;

    [SerializeField] private TextMeshProUGUI _numberWaveText;

    [SerializeField] private TextMeshProUGUI _coinsText;

    [Space(10)]
    [SerializeField] private Button _startWaveButton;

    //--------------------------------------

    private LevelManager levelManager;

    //======================================

    private void Start()
    {
      UpdateHealthText();

      UpdateNumberWaveText();

      UpdateCoinsText();
    }

    private void OnEnable()
    {
      levelManager.PlayerHomeBase.Health.OnChangeHealth += UpdateHealthText;

      levelManager.WaveManager.OnWaveBegun += UpdateNumberWaveText;

      levelManager.WaveManager.OnWaveCompleted += StartWaveButton;

      levelManager.—urrency.OnChange—urrency += UpdateCoinsText;

      _startWaveButton.onClick.AddListener(StartWave);
    }

    private void OnDisable()
    {
      levelManager.PlayerHomeBase.Health.OnChangeHealth -= UpdateHealthText;

      levelManager.WaveManager.OnWaveBegun -= UpdateNumberWaveText;

      levelManager.WaveManager.OnWaveCompleted -= StartWaveButton;

      levelManager.—urrency.OnChange—urrency -= UpdateCoinsText;

      _startWaveButton.onClick.RemoveListener(StartWave);
    }

    //======================================

    [Inject]
    private void Construct(LevelManager parLevelManager)
    {
      levelManager = parLevelManager;
    }

    //======================================

    private void UpdateHealthText()
    {
      UpdateHealthText(levelManager.PlayerHomeBase.Health.CurrentHealth);
    }

    private void UpdateHealthText(int parHealth)
    {
      _healthText.text = $"{parHealth}";
    }

    //--------------------------------------

    private void UpdateNumberWaveText()
    {
      UpdateNumberWaveText(levelManager.WaveManager.CurrentWaveIndex + 1);
    }

    private void UpdateNumberWaveText(int parWave)
    {
      _numberWaveText.text = $"{parWave}/{levelManager.WaveManager.NumberWaves}";
    }

    //--------------------------------------

    private void UpdateCoinsText()
    {
      UpdateCoinsText(levelManager.—urrency.Current—urrency);
    }

    private void UpdateCoinsText(int parCurrence)
    {
      _coinsText.text = $"{parCurrence}";
    }

    //--------------------------------------

    private void StartWave()
    {
      levelManager.StartWave();

      _startWaveButton.gameObject.SetActive(false);
    }

    private void StartWaveButton(int parWave)
    {
      _startWaveButton.gameObject.SetActive(true);
    }

    //======================================
  }
}
using System;

namespace GuardiansDefense.Currencies
{
  public class Сurrency
  {
    public int CurrentСurrency { get; private set; }

    //======================================

    public event Action<int> OnAddСurrency;

    public event Action<int> OnTakeСurrency;

    public event Action<int> OnChangeСurrency;

    //======================================

    public Сurrency(int parStartingСurrency)
    {
      AddСurrency(parStartingСurrency);
    }

    //======================================

    public void AddСurrency(int parСurrency)
    {
      if (parСurrency < 0)
        return;

      if (CurrentСurrency < 0)
        CurrentСurrency = 0;

      int currencyBefore = CurrentСurrency;
      CurrentСurrency += parСurrency;

      int currencyAmount = CurrentСurrency - currencyBefore;
      if (currencyAmount > 0)
        OnAddСurrency?.Invoke(currencyAmount);

      OnChangeСurrency?.Invoke(CurrentСurrency);
    }

    public void TakeСurrency(int parСurrency)
    {
      if (parСurrency < 0)
        return;

      int currencyBefore = CurrentСurrency;
      CurrentСurrency -= parСurrency;

      if (CurrentСurrency < 0)
        CurrentСurrency = 0;

      int currencyAmount = currencyBefore - CurrentСurrency;
      if (currencyAmount > 0)
        OnTakeСurrency?.Invoke(currencyAmount);

      OnChangeСurrency?.Invoke(CurrentСurrency);
    }

    public bool CanAfford(int parValue)
    {
      return CurrentСurrency >= parValue;
    }

    //======================================
  }
}
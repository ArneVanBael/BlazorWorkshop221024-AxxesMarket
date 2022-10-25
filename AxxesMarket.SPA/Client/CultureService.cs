using System.Globalization;

namespace AxxesMarket.SPA.Client;

public class CultureState
{
    public Action NewState { get; set; }

    public string Culture { get; private set; }

    public void SetCulture(string culture)
    {
        Culture = culture;
        NewState.Invoke();
    }
}

using System.Runtime.InteropServices;
using MachineACafe;
using MachineACafe.Ports;

namespace MachineACafé.LowLevelAdapter;

public class LowLevelAdapter : IMachineHardware
{
    [DllImport("MachineType52.dll")] public static extern int GetCoffeeCounter();
    [DllImport("MachineType52.dll")] public static extern bool DetectCoffee();
    [DllImport("MachineType52.dll")] public static extern bool DetectCup();
    [DllImport("MachineType52.dll")] public static extern int MoneyPresent();
    [DllImport("MachineType52.dll")] public static extern int OrderOneCoffee();

    private MoneyInserted _onMoneyInserted = _ => { };

    public void StartPolling(TimeSpan interval, CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            var amount = MoneyPresent();
            if (amount != 0) _onMoneyInserted(Pièce.FromAmount(amount));

            Thread.Sleep(interval);
        }
    }

    /// <inheritdoc />
    public ushort CoffeeCounter => (ushort) GetCoffeeCounter();

    /// <inheritdoc />
    /// <remarks>Sur cette machine, utilisation du réseau d'eau, présumé infini</remarks>
    public bool HasAtLeastOneVolumeOfWater => true;

    /// <inheritdoc />
    public bool HasAtLeastOneMeasureOfCoffee => DetectCoffee();

    /// <inheritdoc />
    public bool HasAtLeastOneCup => DetectCup();

    /// <inheritdoc />
    public void MakeOneCoffee()
    {
        var result = OrderOneCoffee();
        if (result != 0) throw new ExternalException("Error while making coffee", result);
    }

    /// <inheritdoc />
    public void RegisterMoneyInsertedCallback(MoneyInserted callback)
    {
        _onMoneyInserted = callback;
    }
}
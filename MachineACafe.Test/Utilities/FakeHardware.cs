using MachineACafe.Ports;

namespace MachineACafe.Test.Utilities;

internal class FakeHardware : IMachineHardware
{
    private ushort _stockOfCoffee;
    private ushort _stockOfCups;
    public ushort CoffeeCounter { get; init; }
    private MoneyInserted _onMoneyInserted = _ => { };

    public FakeHardware(ushort stockOfCoffee, ushort stockOfCups, bool hasWater)
    {
        _stockOfCoffee = stockOfCoffee;
        _stockOfCups = stockOfCups;
        HasAtLeastOneVolumeOfWater = hasWater;
    }

    public void SimulateInsertMoney(Pièce pièce)
    {
        _onMoneyInserted(pièce);
    }

    /// <inheritdoc />
    public bool HasAtLeastOneVolumeOfWater { get; }

    /// <inheritdoc />
    public bool HasAtLeastOneMeasureOfCoffee => _stockOfCoffee > 0;

    /// <inheritdoc />
    public bool HasAtLeastOneCup => _stockOfCups > 0;

    /// <inheritdoc />
    public void MakeOneCoffee()
    {
        if (_stockOfCoffee == 0) throw new InvalidOperationException("Plus de café");
        if (_stockOfCups == 0) throw new InvalidOperationException("Plus de gobelets");
        if(!HasAtLeastOneVolumeOfWater) throw new InvalidOperationException("Pas d'eau");

        _stockOfCoffee--;
        _stockOfCups--;
    }

    /// <inheritdoc />
    public void RegisterMoneyInsertedCallback(MoneyInserted callback)
    {
        _onMoneyInserted = callback;
    }
}
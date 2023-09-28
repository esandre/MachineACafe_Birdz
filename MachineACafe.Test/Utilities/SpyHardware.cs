using MachineACafe.Ports;

namespace MachineACafe.Test.Utilities;

internal class SpyHardware : IMachineHardware
{
    private readonly IMachineHardware _spied;

    public SpyHardware(IMachineHardware spied)
    {
        _spied = spied;
    }

    /// <inheritdoc />
    public ushort CoffeeCounter => _spied.CoffeeCounter;

    /// <inheritdoc />
    public bool HasAtLeastOneVolumeOfWater => _spied.HasAtLeastOneVolumeOfWater;

    /// <inheritdoc />
    public bool HasAtLeastOneMeasureOfCoffee => _spied.HasAtLeastOneMeasureOfCoffee;

    /// <inheritdoc />
    public bool HasAtLeastOneCup => _spied.HasAtLeastOneCup;

    /// <inheritdoc />
    public void MakeOneCoffee()
    {
        MakeOneCoffeeHasBeenCalled = true;
        _spied.MakeOneCoffee();
    }

    /// <inheritdoc />
    public void RegisterMoneyInsertedCallback(MoneyInserted callback)
    {
        _spied.RegisterMoneyInsertedCallback(callback);
    }

    public bool MakeOneCoffeeHasBeenCalled { get; private set; }
}
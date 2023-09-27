using MachineACafe.Ports;

namespace MachineACafe.Test.Utilities;

public class StubHardware : IMachineHardware
{
    /// <inheritdoc />
    public ushort CoffeeCounter => 0;

    /// <inheritdoc />
    public bool HasAtLeastOneVolumeOfWater => false;

    /// <inheritdoc />
    public bool HasAtLeastOneMeasureOfCoffee => false;

    /// <inheritdoc />
    public bool HasAtLeastOneCup => false;

    /// <inheritdoc />
    public void MakeOneCoffee()
    {
    }

    /// <inheritdoc />
    public void RegisterMoneyInsertedCallback(MoneyInserted callback)
    {
    }
}
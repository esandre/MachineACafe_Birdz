using MachineACafe.Ports;

namespace MachineACafe.Test.Utilities;

internal class SpyHardware : IMachineHardware
{
    private readonly IMachineHardware _spied;
    public uint DosesDEauConsommées { get; private set; }

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
        DosesDEauConsommées++;
        MakeOneCoffeeHasBeenCalled = true;
        _spied.MakeOneCoffee();
    }

    /// <inheritdoc />
    public void AddOneDoseOfWater()
    {
        DosesDEauConsommées++;
        AddOneDoseOfWaterHasBeenCalled = true;
        _spied.AddOneDoseOfWater();
    }

    /// <inheritdoc />
    public void RegisterMoneyInsertedCallback(MoneyInserted callback)
    {
        _spied.RegisterMoneyInsertedCallback(callback);
    }

    /// <inheritdoc />
    public void RegisterMoreWaterButtonPressed(ButtonPressed callback)
    {
        _spied.RegisterMoreWaterButtonPressed(callback);
    }

    public bool MakeOneCoffeeHasBeenCalled { get; private set; }
    public bool AddOneDoseOfWaterHasBeenCalled { get; private set; }
}
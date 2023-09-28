using MachineACafe.Ports;

namespace MachineACafe.Test.Utilities;

public class FakeHardware : IMachineHardware
{
    private ushort _stockOfCoffee;
    private ushort _stockOfCups;
    private bool _allongéDemandé;
    public ushort CoffeeCounter { get; }
    private MoneyInserted _onMoneyInserted = _ => { };
    private ButtonPressed _onLongCoffeeButtonPressed = _ => { };

    public FakeHardware(ushort stockOfCoffee, ushort stockOfCups, bool hasWater)
    {
        _stockOfCoffee = stockOfCoffee;
        _stockOfCups = stockOfCups;
        HasAtLeastOneVolumeOfWater = hasWater;
    }

    public void SimulerInsertionPièce(Pièce pièce)
    {
        _onMoneyInserted(pièce);
    }

    public void SimulerAppuiBoutonAllongé()
    {
        _allongéDemandé = !_allongéDemandé;
        _onLongCoffeeButtonPressed(_allongéDemandé);
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
    public void AddOneDoseOfWater()
    {
    }

    /// <inheritdoc />
    public void RegisterMoneyInsertedCallback(MoneyInserted callback)
    {
        _onMoneyInserted = callback;
    }

    /// <inheritdoc />
    public void RegisterMoreWaterButtonPressed(ButtonPressed callback)
    {
        _onLongCoffeeButtonPressed = callback;
    }
}
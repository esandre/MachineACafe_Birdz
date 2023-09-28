namespace MachineACafe.Ports;

public interface IMachineHardware
{
    ushort CoffeeCounter { get; }

    bool HasAtLeastOneVolumeOfWater { get; }
    bool HasAtLeastOneMeasureOfCoffee { get; }
    bool HasAtLeastOneCup { get; }

    void MakeOneCoffee();
    void AddOneDoseOfWater();

    void RegisterMoneyInsertedCallback(MoneyInserted callback);
    void RegisterMoreWaterButtonPressed(ButtonPressed callback);
}

public delegate void MoneyInserted(Pièce pièceDétectée);
public delegate void ButtonPressed(bool newState);
namespace MachineACafe.Ports;

public interface IMachineHardware
{
    ushort CoffeeCounter { get; }

    bool HasAtLeastOneVolumeOfWater { get; }
    bool HasAtLeastOneMeasureOfCoffee { get; }
    bool HasAtLeastOneCup { get; }

    void MakeOneCoffee();
    void RegisterMoneyInsertedCallback(MoneyInserted callback);
}

public delegate void MoneyInserted(Pièce pièceDétectée);
using MachineACafe.Ports;

namespace MachineACafe;

public class MachineACafé : IMachineACafé
{
    private readonly IMachineHardware _hardware;

    // Devrait être 40cents, mais vu que la machine n'accepte qu'une pièce
    // pour le moment, c'est 50cts
    public static readonly Pièce SommeMinimale = Pièce.CinquanteCentimes;

    public MachineACafé(IMachineHardware hardware)
    {
        _hardware = hardware;
        hardware.RegisterMoneyInsertedCallback(Insérer);
        hardware.RegisterMoreWaterButtonPressed(DemanderCaféAllongé);
    }

    private void DemanderCaféAllongé(bool buttonIsPressed)
    {
        CaféAllongéDemandé = buttonIsPressed;
    }

    public uint NombreCafésServis { get; private set; }
    public uint SommeEncaisséeEnCentimes { get; private set; }

    private bool CaféAllongéDemandé { get; set; }

    private void Insérer(Pièce pièce)
    {
        if (pièce < SommeMinimale) return;
        if (!_hardware.HasAtLeastOneVolumeOfWater) return;
        if (!_hardware.HasAtLeastOneCup) return;
        if (!_hardware.HasAtLeastOneMeasureOfCoffee) return;

        NombreCafésServis++;
        SommeEncaisséeEnCentimes += pièce.ValeurEnCentimes;
        _hardware.MakeOneCoffee();

        if(CaféAllongéDemandé)
            _hardware.AddOneDoseOfWater();
    }
}
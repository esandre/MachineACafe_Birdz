using MachineACafe.Ports;

namespace MachineACafe;

public class MachineACafé
{
    private readonly IMachineHardware _hardware;

    public MachineACafé(IMachineHardware hardware)
    {
        _hardware = hardware;
        hardware.RegisterMoneyInsertedCallback(Insérer);
    }

    public uint NombreCafésServis { get; private set; }
    public uint SommeEncaisséeEnCentimes { get; private set; }

    private void Insérer(Pièce pièce)
    {
        if (pièce < Pièce.CinquanteCentimes) return;
        if (!_hardware.HasAtLeastOneVolumeOfWater) return;
        if (!_hardware.HasAtLeastOneCup) return;

        NombreCafésServis++;
        SommeEncaisséeEnCentimes += pièce.ValeurEnCentimes;
    }
}
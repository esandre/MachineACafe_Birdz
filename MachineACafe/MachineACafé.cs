using MachineACafe.Ports;

namespace MachineACafe;

public class MachineACafé
{
    public MachineACafé(IMachineHardware hardware)
    {
        hardware.RegisterMoneyInsertedCallback(Insérer);
    }

    public uint NombreCafésServis { get; private set; }
    public uint SommeEncaisséeEnCentimes { get; private set; }

    private void Insérer(Pièce pièce)
    {
        NombreCafésServis++;
        SommeEncaisséeEnCentimes += pièce.ValeurEnCentimes;
    }
}
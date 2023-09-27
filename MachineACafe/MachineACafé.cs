using MachineACafe.Ports;

namespace MachineACafe;

public class MachineACafé
{
    public MachineACafé(IMachineHardware hardware)
    {
    }

    public uint NombreCafésServis { get; private set; }
    public uint SommeEncaisséeEnCentimes { get; private set; }

    public void Insérer(Pièce pièce)
    {
        NombreCafésServis++;
        SommeEncaisséeEnCentimes += pièce.ValeurEnCentimes;
    }
}
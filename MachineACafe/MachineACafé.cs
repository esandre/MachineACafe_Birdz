namespace MachineACafe;

public class MachineACafé
{
    public uint NombreCafésServis { get; private set; }
    public uint SommeEncaisséeEnCentimes { get; private set; }

    public void Insérer(uint valeurPièceEnCentimes)
    {
        NombreCafésServis++;
        SommeEncaisséeEnCentimes += valeurPièceEnCentimes;
    }
}
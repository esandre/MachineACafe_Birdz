namespace MachineACafe;

public class Pièce
{
    public ushort ValeurEnCentimes { get; }

    public static readonly Pièce CinquanteCentimes = new(50);
    public static readonly Pièce UnEuro = new(100);
    public static readonly Pièce DeuxEuros = new(200);
        
    private Pièce(ushort valeurEnCentimes)
    {
        ValeurEnCentimes = valeurEnCentimes;
    }
}
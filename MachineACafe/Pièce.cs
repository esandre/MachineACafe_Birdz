namespace MachineACafe;

public class Pièce
{
    public ushort ValeurEnCentimes { get; }

    public static readonly Pièce UnCentime = new(1);
    public static readonly Pièce DeuxCentimes = new(2);
    public static readonly Pièce CinqCentimes = new(5);
    public static readonly Pièce DixCentimes = new(10);
    public static readonly Pièce VingtCentimes = new(20);
    public static readonly Pièce CinquanteCentimes = new(50);
    public static readonly Pièce UnEuro = new(100);
    public static readonly Pièce DeuxEuros = new(200);
        
    private Pièce(ushort valeurEnCentimes)
    {
        ValeurEnCentimes = valeurEnCentimes;
    }

    public static bool operator<(Pièce a, Pièce b) 
        => a.ValeurEnCentimes < b.ValeurEnCentimes;

    public static bool operator >(Pièce a, Pièce b) 
        => a.ValeurEnCentimes > b.ValeurEnCentimes;

    public static Pièce FromAmount(int amount)
        => amount switch
           {
               50  => CinquanteCentimes,
               100 => UnEuro,
               200 => DeuxEuros,
               _   => throw new ArgumentException(
                   "Montant ne correspondant pas à une pièce connue", 
                   nameof(amount))
           };
}
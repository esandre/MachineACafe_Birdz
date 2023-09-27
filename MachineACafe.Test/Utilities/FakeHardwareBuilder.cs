namespace MachineACafe.Test.Utilities;

public class FakeHardwareBuilder
{
    public static FakeHardware Default => new FakeHardwareBuilder().Build();

    private bool _hasWater = true;
    private bool _hasCoffee = true;
    private bool _hasCups = true;

    public FakeHardwareBuilder NAyantPasDEau()
    {
        _hasWater = false;
        return this;
    }

    public FakeHardware Build()
    {
        var dosesCafé = (ushort) (_hasCoffee ? 1 : 0);
        var nombreGobelets = (ushort) (_hasCups ? 1 : 0);
        return new FakeHardware(dosesCafé, nombreGobelets, _hasWater);
    }

    public FakeHardwareBuilder NAyantPasDeCafé()
    {
        _hasCoffee = false;
        return this;
    }

    public FakeHardwareBuilder NAyantPasDeGobelets()
    {
        _hasCups = false;
        return this;
    }

    public FakeHardwareBuilder AyantUneRessourceManquante(Ressources ressource)
        => ressource switch
           {
               Ressources.Eau     => NAyantPasDEau(),
               Ressources.Gobelet => NAyantPasDeGobelets(),
               Ressources.Café    => NAyantPasDeCafé(),
               _                  => throw new ArgumentOutOfRangeException(nameof(ressource), ressource, null)
           };

    public enum Ressources
    {
        Eau,
        Gobelet,
        Café
    }
}
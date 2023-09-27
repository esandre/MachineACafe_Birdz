namespace MachineACafe.Test.Utilities;

public class MachineACaféBuilder
{
    public static MachineACaféHarness Default => new MachineACaféBuilder().Build();

    private bool _hasWater = true;
    private bool _hasCoffee = true;
    private bool _hasCups = true;

    public MachineACaféBuilder NAyantPasDeCafé()
    {
        _hasCoffee = false;
        return this;
    }

    public MachineACaféBuilder NAyantPasDeGobelets()
    {
        _hasCups = false;
        return this;
    }

    public MachineACaféBuilder NAyantPasDEau()
    {
        _hasWater = false;
        return this;
    }

    public MachineACaféHarness Build()
    {
        var dosesCafé = (ushort)(_hasCoffee ? 1 : 0);
        var nombreGobelets = (ushort)(_hasCups ? 1 : 0);
        var hardware = new FakeHardware(dosesCafé, nombreGobelets, _hasWater);

        return new MachineACaféHarness(new MachineACafé(hardware), hardware);
    }

    public MachineACaféBuilder AyantUneRessourceManquante(Ressources ressource)
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
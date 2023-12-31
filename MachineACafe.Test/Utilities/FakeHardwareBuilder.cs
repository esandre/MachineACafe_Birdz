﻿namespace MachineACafe.Test.Utilities;

public class FakeHardwareBuilder
{
    public static FakeHardware Default => new FakeHardwareBuilder().Build();

    private ushort _limiteEnEau = 3;
    private bool _hasCoffee = true;
    private bool _hasCups = true;

    public FakeHardwareBuilder NAyantPasDEau()
        => LimitéEnEau(0);

    public FakeHardware Build()
    {
        var dosesCafé = (ushort) (_hasCoffee ? 2 : 0);
        var nombreGobelets = (ushort) (_hasCups ? 2 : 0);
        return new FakeHardware(dosesCafé, nombreGobelets, _limiteEnEau);
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

    public FakeHardwareBuilder LimitéEnEau(ushort limite)
    {
        _limiteEnEau = limite;
        return this;
    }
}
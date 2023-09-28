using MachineACafe.Test.Utilities;

namespace MachineACafe.Test;

public class MachineACaféTest
{
    public static IEnumerable<object[]> CasCasNominal => new[]
    {
        new[] { Pièce.CinquanteCentimes },
        new[] { Pièce.UnEuro },
        new[] { Pièce.DeuxEuros }
    };

    public static IEnumerable<object[]> CasPasAssezArgent => new[]
    {
        new[] { Pièce.VingtCentimes },
        new[] { Pièce.DixCentimes },
        new[] { Pièce.CinqCentimes },
        new[] { Pièce.DeuxCentimes },
        new[] { Pièce.UnCentime }
    };

    [Theory(DisplayName = "Quand on met la bonne somme, le café coule.")]
    [MemberData(nameof(CasCasNominal))]
    public void CasNominal(Pièce pièce)
    {
        // ETANT DONNE une pièce d'une valeur supérieure ou égale à 40cts
        var spiedHardware = new FakeHardwareBuilder().Build();
        var hardwareSpy = new SpyHardware(spiedHardware);
        var machine = MachineACaféBuilder.AvecHardware(hardwareSpy);

        // QUAND la pièce est insérée
        spiedHardware.SimulerInsertionPièce(pièce);

        // ALORS le compteur de cafés servis s'incrémente
        Assert.UnCaféEstServi(machine);

        // ET la valeur de la pièce est encaissée
        Assert.LeMontantEstEncaissé(machine, pièce);

        // ET aucune dose d'eau supplémentaire n'est demandée
        Assert.False(hardwareSpy.AddOneDoseOfWaterHasBeenCalled);
    }

    [Theory(DisplayName = "Quand on met une somme insuffisante, l'argent est rendu.")]
    [MemberData(nameof(CasPasAssezArgent))]
    public void PasAssezArgent(Pièce pièce)
    {
        // ETANT DONNE une pièce d'une valeur inférieure à 40cts
        var hardware = FakeHardwareBuilder.Default;
        var machine = MachineACaféBuilder.AvecHardware(hardware);

        // QUAND la pièce est insérée
        hardware.SimulerInsertionPièce(pièce);

        // ALORS le compteur de cafés servis reste identique
        Assert.AucunCaféNEstServi(machine);

        // ET la somme encaissée de même
        Assert.AucunArgentNEstEncaissé(machine);
    }

    [Theory(DisplayName = "Quand une ressource manque, l'argent est rendu.")]
    [InlineData(FakeHardwareBuilder.Ressources.Café)]
    [InlineData(FakeHardwareBuilder.Ressources.Eau)]
    [InlineData(FakeHardwareBuilder.Ressources.Gobelet)]
    public void AbsenceRessource(FakeHardwareBuilder.Ressources ressourceManquante)
    {
        // ETANT DONNE une machine manquant d'une ressource
        // ET une pièce d'une valeur suffisante
        var hardware = new FakeHardwareBuilder()
            .AyantUneRessourceManquante(ressourceManquante)
            .Build();
        var machine = MachineACaféBuilder.AvecHardware(hardware);

        // QUAND la pièce est insérée
        hardware.SimulerInsertionPièce(MachineACafé.SommeMinimale);

        // ALORS le compteur de cafés servis reste identique
        Assert.AucunCaféNEstServi(machine);

        // ET la somme encaissée de même
        Assert.AucunArgentNEstEncaissé(machine);
    }

    [Fact]
    public void DemandeAuHardwareUnCafé()
    {
        // ETANT DONNE une machine reliée à un hardware
        // ET une pièce d'une valeur suffisante
        var spiedHardware = new FakeHardwareBuilder().Build();
        var hardwareSpy = new SpyHardware(spiedHardware);
        MachineACaféBuilder.AvecHardware(hardwareSpy);

        // QUAND la pièce est insérée
        spiedHardware.SimulerInsertionPièce(MachineACafé.SommeMinimale);

        // ALORS le hardware est bien sollicité pour couler un café
        Assert.True(hardwareSpy.MakeOneCoffeeHasBeenCalled);
    }

    [Fact (DisplayName ="Quand un café long est demandé et on a au moins deux doses d'eau")]
    public void DemandeDeCafeLong()
    { 
        // ETANT DONNE une pièce d'une valeur supérieure à 40cts
        var pièce = MachineACafé.SommeMinimale;

        // ET une machine dont le bouton "allongé" est appuyé
        var spiedHardware = new FakeHardwareBuilder().Build();
        var hardwareSpy = new SpyHardware(spiedHardware);
        var machine = MachineACaféBuilder.AvecHardware(hardwareSpy);
        spiedHardware.SimulerAppuiBoutonAllongé();

        // QUAND la pièce est insérée
        spiedHardware.SimulerInsertionPièce(pièce);

        // ALORS le compteur de cafés servis s'incrémente
        Assert.UnCaféEstServi(machine);

        // ET la valeur de la pièce est encaissée
        Assert.LeMontantEstEncaissé(machine, pièce);

        // ET une dose d'eau supplémentaire est demandée au hardware
        Assert.True(hardwareSpy.AddOneDoseOfWaterHasBeenCalled);
    }


    [Fact (DisplayName ="Quand un café long est demandé et on a une seule dose d'eau")]
    public void DemandeDeCafeLongMaisUneSeuleDoseEau()
    { 
        // ETANT DONNE une pièce d'une valeur supérieure à 40cts
        var pièce = MachineACafé.SommeMinimale;

        // ET une machine dont le bouton "allongé" est appuyé
        // ET il n'y a qu'une seule dose d'eau
        var spiedHardware = new FakeHardwareBuilder()
            .LimitéEnEau(1)
            .Build();
        var hardwareSpy = new SpyHardware(spiedHardware);
        var machine = MachineACaféBuilder.AvecHardware(hardwareSpy);
        spiedHardware.SimulerAppuiBoutonAllongé();

        // QUAND la pièce est insérée
        spiedHardware.SimulerInsertionPièce(pièce);

        // ALORS le compteur de cafés servis s'incrémente
        Assert.UnCaféEstServi(machine);

        // ET la valeur de la pièce est encaissée
        Assert.LeMontantEstEncaissé(machine, pièce);

        // ET une dose d'eau n'est pas demandé car plus d'eau
        Assert.False(hardwareSpy.AddOneDoseOfWaterHasBeenCalled);
    }


    [Fact(DisplayName = "Quand un café long est demandé le button se reset")]
    public void DemandeDeCafeLongResetLEtatDuBoutton()
    {
        // ETANT DONNE une pièce d'une valeur supérieure à 40cts
        var pièce = MachineACafé.SommeMinimale;

        // ET une machine dont le bouton "allongé" est appuyé
        var spiedHardware = new FakeHardwareBuilder().Build();
        var hardwareSpy = new SpyHardware(spiedHardware);
        var machine = MachineACaféBuilder.AvecHardware(hardwareSpy);
        spiedHardware.SimulerAppuiBoutonAllongé();

        // QUAND deux pièces sont insérées de suite
        spiedHardware.SimulerInsertionPièce(pièce);
        spiedHardware.SimulerInsertionPièce(pièce);

        // ALORS le compteur de cafés servis s'incrémente de 2
        Assert.DeuxCafesSontServis(machine);

        // ET trois doses d'eau sont consommées
        Assert.Equal(3U, hardwareSpy.DosesDEauConsommées);
    }
}
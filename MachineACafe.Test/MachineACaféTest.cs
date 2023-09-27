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
        var machine = MachineACaféBuilder.Default;

        // QUAND la pièce est insérée
        machine.SimulerInsertionPièce(pièce);

        // ALORS le compteur de cafés servis s'incrémente
        Assert.UnCaféEstServi(machine);

        // ET la valeur de la pièce est encaissée
        Assert.LeMontantEstEncaissé(machine, pièce);
    }

    [Theory(DisplayName = "Quand on met une somme insuffisante, l'argent est rendu.")]
    [MemberData(nameof(CasPasAssezArgent))]
    public void PasAssezArgent(Pièce pièce)
    {
        // ETANT DONNE une pièce d'une valeur inférieure à 40cts
        var machine = MachineACaféBuilder.Default;

        // QUAND la pièce est insérée
        machine.SimulerInsertionPièce(pièce);

        // ALORS le compteur de cafés servis reste identique
        Assert.AucunCaféNEstServi(machine);

        // ET la somme encaissée de même
        Assert.AucunArgentNEstEncaissé(machine);
    }

    [Theory(DisplayName = "Quand une ressource manque, l'argent est rendu.")]
    [InlineData(MachineACaféBuilder.Ressources.Café)]
    [InlineData(MachineACaféBuilder.Ressources.Eau)]
    [InlineData(MachineACaféBuilder.Ressources.Gobelet)]
    public void AbsenceRessource(MachineACaféBuilder.Ressources ressourceManquante)
    {
        // ETANT DONNE une machine manquant d'une ressource
        // ET une pièce d'une valeur suffisante
        var pièce = Pièce.CinquanteCentimes;

        var machine = new MachineACaféBuilder()
            .AyantUneRessourceManquante(ressourceManquante)
            .Build();

        // QUAND la pièce est insérée
        machine.SimulerInsertionPièce(pièce);

        // ALORS le compteur de cafés servis reste identique
        Assert.AucunCaféNEstServi(machine);

        // ET la somme encaissée de même
        Assert.AucunArgentNEstEncaissé(machine);
    }
}
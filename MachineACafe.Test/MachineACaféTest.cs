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
        var hardware = FakeHardwareBuilder.Default;
        var machine = MachineACaféBuilder.AvecHardware(hardware);

        var nombreCafésServisInitiaux = machine.NombreCafésServis;
        var sommeInitiale = machine.SommeEncaisséeEnCentimes;

        // QUAND la pièce est insérée
        hardware.SimulateInsertMoney(pièce);

        // ALORS le compteur de cafés servis s'incrémente
        var nombreCafésServisFinaux = machine.NombreCafésServis;
        Assert.Equal(nombreCafésServisInitiaux + 1, nombreCafésServisFinaux);

        // ET la valeur de la pièce est encaissée
        var sommeFinale = machine.SommeEncaisséeEnCentimes;
        Assert.Equal(sommeInitiale + pièce.ValeurEnCentimes, sommeFinale);
    }

    [Theory(DisplayName = "Quand on met une somme insuffisante, l'argent est rendu.")]
    [MemberData(nameof(CasPasAssezArgent))]
    public void PasAssezArgent(Pièce pièce)
    {
        // ETANT DONNE une pièce d'une valeur inférieure à 40cts
        var hardware = FakeHardwareBuilder.Default;
        var machine = MachineACaféBuilder.AvecHardware(hardware);
        var nombreCafésServisInitiaux = machine.NombreCafésServis;
        var sommeInitiale = machine.SommeEncaisséeEnCentimes;

        // QUAND la pièce est insérée
        hardware.SimulateInsertMoney(pièce);

        // ALORS le compteur de cafés servis reste identique
        var nombreCafésServisFinaux = machine.NombreCafésServis;
        Assert.Equal(nombreCafésServisInitiaux, nombreCafésServisFinaux);

        // ET la somme encaissée de même
        var sommeFinale = machine.SommeEncaisséeEnCentimes;
        Assert.Equal(sommeInitiale, sommeFinale);
    }

    [Fact(DisplayName = "Quand il n'y a pas d'eau, l'argent est rendu.")]
    public void AbsenceEau()
    {
        // ETANT DONNE une machine n'ayant pas d'eau
        // ET une pièce d'une valeur suffisante
        var pièce = Pièce.CinquanteCentimes;

        var hardware = new FakeHardwareBuilder().NAyantPasDEau().Build();
        var machine = MachineACaféBuilder.AvecHardware(hardware);

        var nombreCafésServisInitiaux = machine.NombreCafésServis;
        var sommeInitiale = machine.SommeEncaisséeEnCentimes;

        // QUAND la pièce est insérée
        hardware.SimulateInsertMoney(pièce);

        // ALORS le compteur de cafés servis reste identique
        var nombreCafésServisFinaux = machine.NombreCafésServis;
        Assert.Equal(nombreCafésServisInitiaux, nombreCafésServisFinaux);

        // ET la somme encaissée de même
        var sommeFinale = machine.SommeEncaisséeEnCentimes;
        Assert.Equal(sommeInitiale, sommeFinale);
    }

    [Fact(DisplayName = "Quand il n'y a plus de gobelets, l'argent est rendu.")]
    public void AbsenceGobelets()
    {
        // ETANT DONNE une machine n'ayant pas de gobelets
        // ET une pièce d'une valeur suffisante
        var pièce = Pièce.CinquanteCentimes;

        var hardware = new FakeHardwareBuilder()
            .NAyantPasDeGobelets()
            .Build();

        var machine = MachineACaféBuilder.AvecHardware(hardware);

        var nombreCafésServisInitiaux = machine.NombreCafésServis;
        var sommeInitiale = machine.SommeEncaisséeEnCentimes;

        // QUAND la pièce est insérée
        hardware.SimulateInsertMoney(pièce);

        // ALORS le compteur de cafés servis reste identique
        var nombreCafésServisFinaux = machine.NombreCafésServis;
        Assert.Equal(nombreCafésServisInitiaux, nombreCafésServisFinaux);

        // ET la somme encaissée de même
        var sommeFinale = machine.SommeEncaisséeEnCentimes;
        Assert.Equal(sommeInitiale, sommeFinale);
    }

    [Fact(DisplayName = "Quand il n'y a plus de café, l'argent est rendu.")]
    public void AbsenceCafé()
    {
        // ETANT DONNE une machine n'ayant pas de café
        // ET une pièce d'une valeur suffisante
        var pièce = Pièce.CinquanteCentimes;

        var hardware = new FakeHardwareBuilder().NAyantPasDeCafé().Build();
        var machine = MachineACaféBuilder.AvecHardware(hardware);

        var nombreCafésServisInitiaux = machine.NombreCafésServis;
        var sommeInitiale = machine.SommeEncaisséeEnCentimes;

        // QUAND la pièce est insérée
        hardware.SimulateInsertMoney(pièce);

        // ALORS le compteur de cafés servis reste identique
        var nombreCafésServisFinaux = machine.NombreCafésServis;
        Assert.Equal(nombreCafésServisInitiaux, nombreCafésServisFinaux);

        // ET la somme encaissée de même
        var sommeFinale = machine.SommeEncaisséeEnCentimes;
        Assert.Equal(sommeInitiale, sommeFinale);
    }
}
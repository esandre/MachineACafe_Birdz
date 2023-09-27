namespace MachineACafe.Test;

public class MachineACaféTest
{
    [Theory(DisplayName = "Quand on met la bonne somme, le café coule.")]
    [InlineData(40)]
    [InlineData(41)]
    [InlineData(uint.MaxValue)]
    public void CasNominal(uint valeurPièceEnCentimes)
    {
        // ETANT DONNE une pièce d'une valeur supérieure ou égale à 40cts
        var machine = new MachineACafé();
        var nombreCafésServisInitiaux = machine.NombreCafésServis;
        var sommeInitiale = machine.SommeEncaisséeEnCentimes;

        // QUAND la pièce est insérée
        machine.Insérer(valeurPièceEnCentimes);

        // ALORS le compteur de cafés servis s'incrémente
        var nombreCafésServisFinaux = machine.NombreCafésServis;
        Assert.Equal(nombreCafésServisInitiaux + 1, nombreCafésServisFinaux);

        // ET la valeur de la pièce est encaissée
        var sommeFinale = machine.SommeEncaisséeEnCentimes;
        Assert.Equal(sommeInitiale + valeurPièceEnCentimes, sommeFinale);
    }
}
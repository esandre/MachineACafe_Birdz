using MachineACafe;

// ReSharper disable once CheckNamespace
namespace Xunit;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Assert
{
    public static void UnCaféEstServi(MachineACafé machine, uint cafésServisAuDémarrage)
    {
        var nombreCafésAttendus = cafésServisAuDémarrage + 1;
        var nombreCafésRéel = machine.NombreCafésServis;

        Equal(nombreCafésAttendus, nombreCafésRéel);
    }

    public static void AucunCaféNEstServi(MachineACafé machine, uint cafésServisAuDémarrage)
    {
        var nombreCafésAttendus = cafésServisAuDémarrage;
        var nombreCafésRéel = machine.NombreCafésServis;

        Equal(nombreCafésAttendus, nombreCafésRéel);
    }

    public static void LeMontantEstEncaissé(MachineACafé machine, uint caisseDépart, Pièce pièce)
    {
        var montantAttendu = caisseDépart + pièce.ValeurEnCentimes;
        var montantRéel = machine.SommeEncaisséeEnCentimes;

        Equal(montantAttendu, montantRéel);
    }

    public static void AucunArgentNEstEncaissé(MachineACafé machine, uint caisseDépart)
    {
        var montantRéel = machine.SommeEncaisséeEnCentimes;

        Equal(caisseDépart, montantRéel);
    }
}
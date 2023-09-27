using MachineACafe;
using MachineACafe.Test.Utilities;

// ReSharper disable once CheckNamespace
namespace Xunit;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Assert
{
    public static void UnCaféEstServi(MachineACaféHarness machine)
    {
        Equal(1U, machine.DeltaCafésServis);
    }

    public static void AucunCaféNEstServi(MachineACaféHarness machine)
    {
        Equal(0U, machine.DeltaCafésServis);
    }

    public static void LeMontantEstEncaissé(MachineACaféHarness machine, Pièce pièce)
    {
        Equal(pièce.ValeurEnCentimes, machine.DeltaSommeEncaisséeEnCentimes);
    }

    public static void AucunArgentNEstEncaissé(MachineACaféHarness machine)
    {
        Equal(0U, machine.DeltaSommeEncaisséeEnCentimes);
    }
}
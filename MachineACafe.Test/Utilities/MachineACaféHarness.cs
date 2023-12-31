﻿namespace MachineACafe.Test.Utilities;

public class MachineACaféHarness : IMachineACafé
{
    private readonly uint _sommeEncaisséeAuDépart;
    private readonly uint _nombreCafésServisAuDépart;

    private MachineACafé Machine { get; }

    public MachineACaféHarness(MachineACafé machine)
    {
        Machine = machine;
        _sommeEncaisséeAuDépart = machine.SommeEncaisséeEnCentimes;
        _nombreCafésServisAuDépart = machine.NombreCafésServis;
    }

    /// <inheritdoc />
    public uint NombreCafésServis => Machine.NombreCafésServis;

    /// <inheritdoc />
    public uint SommeEncaisséeEnCentimes => Machine.SommeEncaisséeEnCentimes;

    public uint DeltaCafésServis 
        => NombreCafésServis - _nombreCafésServisAuDépart;

    public uint DeltaSommeEncaisséeEnCentimes 
        => SommeEncaisséeEnCentimes - _sommeEncaisséeAuDépart;
}
using MachineACafe.Ports;

namespace MachineACafe.Test.Utilities;

internal class MachineACaféBuilder
{
    private IMachineHardware _hardware = FakeHardwareBuilder.Default;

    public static MachineACaféHarness AvecHardware(IMachineHardware hardware)
        => new MachineACaféBuilder().AyantPourHardware(hardware).Build();

    public MachineACaféBuilder AyantPourHardware(IMachineHardware hardware)
    {
        _hardware = hardware;
        return this;
    }

    public MachineACaféHarness Build()
    {
        return new MachineACaféHarness(new MachineACafé(_hardware));
    }
}
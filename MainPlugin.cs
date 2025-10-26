using LabApi.Features;
using LabApi.Features.Console;
using LabApi.Loader.Features.Plugins;
using LabApi.Loader.Features.Plugins.Enums;
using SER_UCR_bind.Flag;
using SER.MethodSystem;
using Version = System.Version;

namespace SER_UCR_bind;

public class MainPlugin : Plugin
{
    public override string Name => "SER-UCR-bind";
    public override string Description => "A plugin that adds methods to SER that control UCR";
    public override string Author => "Elektryk_Andrzej";
    public override Version Version => new(1, 0, 0);
    public override Version RequiredApiVersion => LabApiProperties.CurrentVersion;
    public override LoadPriority Priority => LoadPriority.Lowest;

    public override void Enable()
    {
        Logger.Info("Loading SER-UCR-bind");
        // ReSharper disable once ObjectCreationAsStatement
        new SERIntergration();
        
        SER.FlagSystem.Structures.Flag.RegisterFlagsAsExternalPlugin();
        MethodIndex.AddAllDefinedMethodsInAssembly();
    }

    public override void Disable()
    {
    }
}

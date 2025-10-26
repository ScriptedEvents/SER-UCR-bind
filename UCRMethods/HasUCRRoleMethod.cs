using JetBrains.Annotations;
using SER.ArgumentSystem.Arguments;
using SER.ArgumentSystem.BaseArguments;
using SER.MethodSystem.BaseMethods;
using SER.ValueSystem;
using UncomplicatedCustomRoles.API.Features;

namespace SER_UCR_bind.UCRMethods;

[UsedImplicitly]
// ReSharper disable once InconsistentNaming
public class HasUCRRoleMethod : ReturningMethod<BoolValue>
{
    public override string Description => "Returns true if the player has a UCR role, else false.";

    public override Argument[] ExpectedArguments { get; } =
    [
        new PlayerArgument("player")
    ];
    
    public override void Execute()
    {
        ReturnValue = SummonedCustomRole.Get(Args.GetPlayer("player")) is not null;
    }
}
using System;
using JetBrains.Annotations;
using SER.ArgumentSystem.Arguments;
using SER.ArgumentSystem.BaseArguments;
using SER.MethodSystem.BaseMethods;
using UncomplicatedCustomRoles.API.Features;
using UncomplicatedCustomRoles.API.Interfaces;

namespace SER_UCR_bind.UCRMethods;

[UsedImplicitly]
// ReSharper disable once InconsistentNaming
public class GetUCRRoleMethod : ReferenceReturningMethod
{
    public override string Description => 
        "Returns the UCR role a player has. Will error if the player doesn't have a role.";
    
    public override Type ReturnType => typeof(ICustomRole);

    public override Argument[] ExpectedArguments { get; } =
    [
        new PlayerArgument("player")
    ];
    
    public override void Execute()
    {
        ReturnValue = new(SummonedCustomRole.Get(Args.GetPlayer("player")).Role);
    }
}
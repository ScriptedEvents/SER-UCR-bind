using System;
using JetBrains.Annotations;
using SER.ArgumentSystem.Arguments;
using SER.ArgumentSystem.BaseArguments;
using SER.Helpers.Exceptions;

using SER.MethodSystem.BaseMethods;
using SER.MethodSystem.MethodDescriptors;
using SER.ValueSystem;
using UncomplicatedCustomRoles.API.Interfaces;

namespace SER_UCR_bind.UCRMethods;

[UsedImplicitly]
// ReSharper disable once InconsistentNaming
public class GetUCRRoleInfoMethod : LiteralValueReturningMethod, IReferenceResolvingMethod
{
    public Type ReferenceType => typeof(ICustomRole);
    public override Type[] LiteralReturnTypes => [typeof(NumberValue), typeof(TextValue)];
    public override string Description => "Returns information about a custom role.";
    
    public override Argument[] ExpectedArguments { get; } =
    [
        new ReferenceArgument<ICustomRole>("custom role reference"),
        new OptionsArgument("property",
            "id",
            "name"
        )
    ];

    public override void Execute()
    {
        var role = Args.GetReference<ICustomRole>("custom role reference");
        ReturnValue = Args.GetOption("property") switch
        {
            "id" => new NumberValue(role.Id),
            "name" => new TextValue(role.Name),
            _ => throw new AndrzejFuckedUpException()
        };
    }
}
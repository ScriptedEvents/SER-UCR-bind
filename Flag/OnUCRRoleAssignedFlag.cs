using System;
using System.Collections.Generic;
using System.Linq;
using LabApi.Features.Console;
using SER.Helpers.Exceptions;
using SER.Helpers.Extensions;
using SER.Helpers.ResultSystem;
using SER.ScriptSystem;
using SER.ScriptSystem.Structures;
using SER.ValueSystem;
using SER.VariableSystem.Variables;
using UncomplicatedCustomRoles.API.Features;

namespace SER_UCR_bind.Flag;

// ReSharper disable once InconsistentNaming
public class OnUCRRoleAssignedFlag : SER.FlagSystem.Structures.Flag
{
    public override string Description => 
        "Executes a script when a given UCR role has spawned. " +
        "Creates an @evPlayer player variable with the player that has this role. " +
        "Creates an *evRole reference variable with the role.";

    private int _roleId;
    private static readonly Dictionary<int, List<string>> ScriptsBoundToRoleId = [];

    public override Result TryInitialize(string[] inlineArgs)
    {
        switch (inlineArgs.Length)
        {
            case < 1: return "The flag requires to provide the ID of the role.";
            case > 1: return "The flag requires to provide ONLY the ID of the role.";
        }
        
        if (!int.TryParse(inlineArgs.First(), out var roleId))
        {
            return $"Value '{inlineArgs.First()}' is not a valid role ID.";
        }
        
        ScriptsBoundToRoleId.AddOrInitListWithKey(roleId, ScriptName);
        _roleId = roleId;
        Logger.Info($"Script {ScriptName} bound to role ID {_roleId}.");
        return true;
    }

    public override void FinalizeFlag()
    {
    }

    public override void Unbind()
    {
        if (ScriptsBoundToRoleId.TryGetValue(_roleId, out var scripts))
        {
            scripts.Remove(ScriptName);
        }
    }

    public static void OnRoleSpawned(SummonedCustomRole role)
    {
        if (!ScriptsBoundToRoleId.TryGetValue(role.Role.Id, out var scripts))
        {
            return;
        }

        foreach (var scriptName in scripts)
        {
            if (Script.CreateByScriptName(scriptName, ScriptExecutor.Get()).HasErrored(out var error, out var script))
            {
                throw new ScriptRuntimeError(error);
            }
            
            script.AddVariable(new PlayerVariable("evPlayer", new(role.Player)));
            script.AddVariable(new ReferenceVariable("evRole", new ReferenceValue(role.Role)));
            script.Run();
        }
    }

    public override (string argName, string description)? InlineArgDescription => ("roleId", "The ID of the role to bind the script to.");
    public override Dictionary<string, (string description, Func<string[], Result> handler)> Arguments => [];
}
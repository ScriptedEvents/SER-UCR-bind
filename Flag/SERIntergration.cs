using UncomplicatedCustomRoles.API.Features.CustomModules;

namespace SER_UCR_bind.Flag;

// ReSharper disable once InconsistentNaming
public class SERIntergration : CustomModule
{
    public override void OnAdded()
    {
        OnUCRRoleAssignedFlag.OnRoleSpawned(CustomRole);
    }
}
# SER-UCR-bind
Allows for better integration between SER and UCR.

# Prerequesits
- `SER.dll` installed in the LabAPI plugins folder.
- `UCR.dll` installed in the LabAPI plugins folder (the EXILED version was not tested, so if possible, stick to LabAPI)

# Implemented
### `GetUCRRoleInfo` method
Returns information about a custom role.
### `GetUCRRole` method
Returns a reference to the UCR role a player has. 
> [!IMPORTANT]
> The reference may be invalid. If so, the player doesn't have a role.

### `OnUCRRoleAssigned` flag
> Executes a script when a given UCR role has spawned.

> [!IMPORTANT]
> In order to have this flag work, you need to add a `SERIntegration` flag in the role config.
> ```
> ...
> custom_flags: 
> - SERIntergration
> ...
> ```

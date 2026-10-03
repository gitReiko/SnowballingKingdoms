---
name: snowballingkingdoms-compatibility
description: "Review SnowballingKingdoms for compatibility with other Mount & Blade II Bannerlord 1.4.8 mods and saved campaigns. Use for requested compatibility reviews, crash diagnosis and fixes to mod interactions."
metadata:
  game: "Mount & Blade II Bannerlord"
  game-version: "1.4.8"
---

# SnowballingKingdoms compatibility

Target game version: **Mount & Blade II Bannerlord 1.4.8**.
Assess compatibility with specific mods and versions when they are named. A general review does not confirm compatibility with every mod collection.
A review request calls for findings and priorities; implement fixes when the user requests code changes.

## Where to inspect

Locate the repository root using `SnowballingKingdoms.sln`. The main interaction points are:

- `CampaignEvents.cs`: event subscriptions, clan selection criteria, clan creation and joining a kingdom.
- `ClanMembersGenerator.cs`: shared culture templates, hero sex, age and skills.
- `Snowball.cs` and `snowballs_data/ee1100.xml`: clan IDs and culture and kingdom associations.
- `SnowConfig.cs`: configurable clan member additions and safe defaults when XML is missing.
- `MySubModule.cs`, `Patches.cs`, `.csproj` and `SubModule.xml`, when available: lifecycle, Harmony, DLLs and load order.

All C# and XML files are under `SnowballingKingdoms/` relative to the repository root.
`SubModule.xml` may only be available in the installed mod package. Do not invent its dependencies when the file has not been provided.

## Review criteria

Check the items relevant to the request or observed failure:

- **Shared objects.** The generator must read templates from `LordTemplates` without modifying them. Preserve female heroes by selecting female templates. Account for missing cultures, `null` entries and templates of only one sex in other mods.
- **Scope of effects.** Distinguish this mod's clans from other clans. An ID can come from XML, so the `sb_clan_` prefix alone is insufficient to determine ownership. When reliable tracking of this mod's clans is needed, propose saving their created IDs.
- **Heroes and saves.** Do not restore the removed `SnowballFixesBehavior` as a compatibility measure. Missing skills alone do not justify deleting other mods' heroes. Changes to saved data must account for the previous schema.
- **Clan creation.** Check validation before creation, ID uniqueness, the leader and home settlement. For joining a kingdom, compare direct assignment to `Kingdom` with the game's `ChangeKingdomAction`: verify its signature and behavior in 1.4.8 to avoid missing required events.
- **Clan member additions.** Check which clans are affected by `ClanTierIncrease` and whether the configuration switch works. Distinguish conflicts in mechanics or balance from technical failures.
- **Skills.** The project uses `MBObjectManager.Instance.GetObjectTypeList<SkillObject>()`. Do not replace the registry with only a list of vanilla skills if this would exclude skills registered by other mods.
- **Harmony.** Check actual patch activation, the owner ID and lifecycle. The existing XML patch targets `Snowballs` and `SnowConfigs`; do not extend disabled validation to all XML. Check shared use of `0Harmony.dll` and the dependency on `Bannerlord.Harmony`.
- **Configuration.** Missing or invalid XML should produce usable defaults. The creation interval must not cause division by zero.
- **API versions.** DLL references must match 1.4.8. The mod's `AssemblyVersion` or a TaleWorlds assembly version is not proof of the game version.

The [Harmony documentation](https://harmony.pardeike.net/v2/articles/annotations.html) and [Bannerlord.Harmony instructions](https://github.com/BUTR/Bannerlord.Harmony) help verify patch integration; they do not confirm compatibility of a particular mod version with 1.4.8.

## Results and verification

For each finding, identify the code location, trigger, impact and smallest suitable fix. Indicate whether the conflict is visible in the code or requires a log, another mod's source code or an in-game run.
Use the relevant verification scenario: a new campaign, loading a save, creating a clan or increasing its tier. Do not claim these checks were performed when they were only proposed.
Do not present API test doubles or 1.4.7 documentation as an actual build and run against **1.4.8** DLLs.

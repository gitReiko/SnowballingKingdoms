---
name: snowballingkingdoms-development
description: "Develop and fix the SnowballingKingdoms C# mod for Mount & Blade II Bannerlord 1.4.8, including clan generation, campaign events, XML data and compilation errors. Use for requested code changes in this mod."
metadata:
  game: "Mount & Blade II Bannerlord"
  game-version: "1.4.8"
  target-framework: ".NET Framework 4.7.2"
---

# SnowballingKingdoms development

Target game version: **Mount & Blade II Bannerlord 1.4.8**.
The project currently uses **C# and .NET Framework 4.7.2**. This is the project's target framework, not confirmation of the requirements of every game DLL.
Change the target game version or framework only within the scope of the user's request.

## Project and API

Locate the repository root using `SnowballingKingdoms.sln`; the paths below are relative to it. Avoid relying on the developer's personal filesystem path.

- `SnowballingKingdoms/ClanMembersGenerator.cs`: hero creation, family relationships, template selection and skill transfer.
- `SnowballingKingdoms/CampaignEvents.cs`: daily clan creation and adding a hero when a clan's tier increases.
- `SnowballingKingdoms/MySubModule.cs`: registration of campaign behaviors and XML types.
- `SnowballingKingdoms/Snowball.cs`: XML clan templates, cultures, priorities and finding unused templates.
- `SnowballingKingdoms/SnowConfig.cs`: configuration parsing and default values.
- `SnowballingKingdoms/Patches.cs`: the Harmony patch for loading custom XML.
- `SnowballingKingdoms/snowballs_data/ee1100.xml`: clan templates.
- `SnowballingKingdoms/SnowballingKingdoms.csproj`: game DLL references and the source file list.

Verify uncertain signatures against local **1.4.8** DLLs or official documentation for the same version.
Label documentation for other versions as supporting material; it does not confirm compatibility with 1.4.8.
The project enumerates skills using:

```csharp
using TaleWorlds.ObjectSystem;

foreach (SkillObject skill in MBObjectManager.Instance.GetObjectTypeList<SkillObject>())
{
    // Read template skills or initialize the newly created hero.
}
```

This accesses registered skills without depending on `Skills.All`.
The [official MBObjectManager API for 1.4.7](https://apidoc.bannerlord.com/v/1.4.7/class_tale_worlds_1_1_object_system_1_1_m_b_object_manager.html) is a supporting source when 1.4.8 documentation is unavailable.

## Preserve when changing the generator

- Do not modify `CharacterObject` instances from the shared `CultureObject.LordTemplates` collection, including `IsFemale`. Select a template of the required sex.
- Preserve the creation of women, mothers and female clan leaders when female templates are available.
- Use `ShouldCreateFemaleMember` when adding a single clan member: choose a male when no female templates exist; choose a female when only female templates exist; preserve the current probability when both sexes are available unless the user requests a different one.
- Filter out `null` templates. Skip generation when the culture or usable templates are unavailable. When templates of only one sex exist, create heroes without inventing family roles.
- Respect `Campaign.Current.Models.AgeModel.HeroComesOfAge`. The leader and any new adult clan member must be adults. Do not turn children into adults through a blanket age check.
- `GetAdultAge` uses an exclusive upper bound. If the minimum is at least that bound, return the minimum without making an invalid random call.
- Validate the culture, templates and home settlement before `Clan.CreateClan` so that skipped generation does not leave an incomplete clan.
- `SnowballFixesBehavior` was removed at the user's request. Do not restore automatic removal of heroes or clans with zero skills.

## Build and verification

This is a classic MSBuild project. First check the actual `HintPath` values and the availability of the required 1.4.8 DLLs.
The Debug configuration currently has an `OutputPath` inside the installed game's directory. For local verification, use Release with output in the workspace or explicitly override OutputPath.

For generation changes, verify that shared templates remain unchanged, female family roles are preserved, and empty lists, templates of only one sex, a higher adulthood age and single-member additions are handled. For a narrow fix, verify the relevant scenario.

API test doubles do not prove that the code builds against 1.4.8 DLLs. In the result, distinguish source inspection, tests using doubles, an actual build and an in-game run.
If the path in a compiler message differs from the working copy, state which copy was changed.

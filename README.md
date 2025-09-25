# Changelog

All notable changes to this project will be documented in this file.  
Adheres to [Keep a Changelog](https://keepachangelog.com) and uses Semantic Versioning.

## [Unreleased]

### Added
- GrowPlants buff now affects all locations (not just main farm)  
- 1.6+ support: create forage items via `ItemRegistry.Create`  
- Alias `SObject = StardewValley.Object` to disambiguate `Object` type  

### Changed
- OfferingData and buff logic migrated to 1.6 API (use `Buff(id, effects)` instead of static fields)  
- `.csproj` updated to reference `SpaceCore.dll` from MO2 path  

### Fixed
- Method-group comparisons corrected (added parentheses where needed)  
- Ambiguous `Object` references resolved  
- Build errors due to missing namespace imports fixed  

## [v1.0.0] – 2025-09-25
### Added
- Initial version with GrowPlants buff  
- Support for wild seed crops and regular crops  
- Config/API integration with OfferingData  

### Fixed
- N/A

# Welcome to Ridgeside Village!

A small village located on the west side of Stardew Valley.
Go up the stairs north-east of the bus stop and hop in the cable car to the village!.
Meet new interesting people with immersive backstories, engaging dialogue and exciting events! Discover new forage, stories, secrets, maybe even fall in love? Who knows!? A lot can happen in Ridgeside Village!

## Join the Ridgeside Community!
[Ridgeside Discord!](https://discord.gg/J5z9JMNQTE)

## Learn more about Ridgeside on the Ridgeside Wiki!
[Ridgeside Wiki!](https://ridgeside.fandom.com/wiki/Ridgeside_Village_Wiki)

## Support me!
[Ko-Fi](https://ko-fi.com/rafseazz)

## Ridgeside Socials!
[Facebook](https://www.facebook.com/RidgesideVillage) - [Instagram](https://www.instagram.com/ridgesidevillage/) - [Reddit](https://www.reddit.com/r/RidgesideVillage) - [Tumblr](https://ridgesidevillage.tumblr.com/) - [Twitter](https://twitter.com/RidgesideVilla) - [YouTube](https://www.youtube.com/channel/UCYECtl3Rhp5ZIt4LTam8BuA)

## LICENSE Information

Ridgeside Village is licensed under the [CC BY-SA 4.0 License International](https://creativecommons.org/licenses/by-sa/4.0/) for all sub-directories outside of the SMAPI Component. The SMAPI Component is licensed under the [MIT License](https://opensource.org/licenses/MIT). When in doubt, look at the LICENSE file in the directory under the top level repo.

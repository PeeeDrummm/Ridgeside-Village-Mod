using StardewValley;
using StardewValley.Buffs;    //BuffEffects
using StardewValley.TerrainFeatures;
using Microsoft.Xna.Framework; // Vector2
using System;
using System.Collections.Generic;
using System.Linq;
using SObject = StardewValley.Object;

namespace RidgesideVillage.Offering
{
    internal class OfferingData
    {
        internal Dictionary<string, OfferEntry> lookup;

        internal OfferingData()
        {
            lookup = new Dictionary<string, OfferEntry>();
            lookup = ModEntry.Helper.Data.ReadJsonFile<Dictionary<string, OfferEntry>>("assets//OfferingData.json");
        }
    }

    enum OfferingType
    {
        WaterPlants,
        GrowPlants,
        Buff,
        ForecastRain,
        ForecastSun,
        BoostLuck,
        BabyChance,
        FairyChance,
        MeteorChance
    }

    internal class OfferEntry
    {
        // força do buff ou quantidade para regar/crescer
        public int Value { get; set; }
        // duração em segundos (para buff)
        public int Duration { get; set; }
        public OfferingType Effect { get; set; }
        // nome do buff se for buff/debuff
        public string BuffType { get; set; }
        // chave do script
        public string ScriptKey { get; set; }

        // aplica o efeito
        internal void Apply()
        {
            switch (this.Effect)
            {
                case OfferingType.WaterPlants:
                    UtilFunctions.WaterPlants(Game1.getFarm());
                    break;

                case OfferingType.Buff:
                    this.ApplyBuff();
                    break;

                case OfferingType.GrowPlants:
                    this.GrowPlants();
                    break;

                case OfferingType.ForecastRain:
                    Game1.weatherForTomorrow = Game1.weather_rain;
                    break;

                case OfferingType.ForecastSun:
                    Game1.weatherForTomorrow = Game1.weather_sunny;
                    break;

                case OfferingType.BoostLuck:
                    Game1.player.team.sharedDailyLuck.Value = 0.12;
                    break;

                case OfferingType.BabyChance:
                    Game1.player.mailReceived.Add(RSVConstants.M_TORTSLOVE);
                    break;

                case OfferingType.MeteorChance:
                    Game1.player.mailReceived.Add(RSVConstants.M_TORTSMETEOR);
                    break;
            }
        }

        // faz crescer em TODAS as localizações
        private void GrowPlants()
        {
            int grown = 0;
            int limit = this.Value;

            foreach (var location in Game1.locations)
            {
                if (location?.terrainFeatures == null || location.terrainFeatures.Count() == 0)
                    continue;

                foreach (var pair in location.terrainFeatures.Pairs)
                {
                    if (grown >= limit)
                        return;

                    if (pair.Value is not HoeDirt dirt || dirt.crop is null)
                        continue;

                    var crop = dirt.crop;
                    bool needsGrowth =
                        crop.currentPhase.Value < crop.phaseDays.Count - 1
                        || (crop.fullyGrown.Value && crop.dayOfCurrentPhase.Value > 0);

                    if (!needsGrowth)
                        continue;

                    if (crop.isWildSeedCrop())
                    {
                        string forageQid = crop.getRandomWildCropForSeason(Game1.season); // 1.6: IDs string
                        if (!location.objects.ContainsKey(dirt.Tile))
                        {
                            var item = ItemRegistry.Create(forageQid, 1);
                            if (item is SObject obj)
                            {
                                obj.IsSpawnedObject = true;
                                obj.CanBeGrabbed = true;
                                location.objects[dirt.Tile] = obj;
                                Log.Verbose($"RSV: Forage crop fully grown at {dirt.Tile.X}, {dirt.Tile.Y} in {location.Name}.");
                                dirt.destroyCrop(false);
                            }
                        }
                    }
                    else
                    {
                        crop.growCompletely();
                        Log.Verbose($"RSV: Regular crop fully grown at {dirt.Tile.X}, {dirt.Tile.Y} in {location.Name}.");
                    }

                    grown++;
                }
            }
        }

        private void ApplyBuff()
        {
            string id = $"RSV.{BuffType?.Trim()}";
            int ms = Math.Max(0, this.Duration) * 1000;

            var effects = new StardewValley.Buffs.BuffEffects();

            switch (BuffType?.ToLowerInvariant())
            {
                case "fishing":
                    effects.FishingLevel.Value = this.Value;
                    break;

                case "magnetism":
                    effects.MagneticRadius.Value = this.Value;
                    break;

                case "maxenergy":
                    effects.MaxStamina.Value = this.Value;
                    break;

                case "speed":
                    effects.Speed.Value = this.Value;
                    break;

                case "defense":
                    effects.Defense.Value = this.Value;
                    break;

                case "attack":
                    effects.Attack.Value = this.Value;
                    break;
            }

            var buff = new Buff(
                id: id,
                displayName: BuffType ?? "RSV Buff",
                description: null,
                iconTexture: null, // usa ícone padrão quando nulo
                iconSheetIndex: 0,
                duration: ms,
                effects: effects
            );

            Game1.player.applyBuff(buff);
        }
    }
}

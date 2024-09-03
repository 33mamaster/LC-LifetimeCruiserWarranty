using System;
using BepInEx.Configuration;
using LifetimeCruiserWarranty;

namespace LifetimeCruiserWarranty
{
    internal class Config
    {
        public static ConfigEntry<bool> applyPenalty;
        public static ConfigEntry<bool> isPercentagePenalty;
        public static ConfigEntry<float> percentagePenaltyAmount;
        public static ConfigEntry<int> flatPenaltyAmount;

        public static void Load()
        {
            applyPenalty = Plugin.config.Bind(
                "Apply Penalty",
                "ApplyPenalty",
                true,
                "Determines if leaving behind or destroying the Company Cruiser will incur a credit penalty."
            );
            isPercentagePenalty = Plugin.config.Bind(
                "Is Penalty Percentage",
                "IsPercentagePenalty",
                true,
                "Determines if the credit penalty is issued as a percentage of your total credits or as a flat rate. Apply Penalty must be true for this to have any effect."
            );
            percentagePenaltyAmount = Plugin.config.Bind(
                "Percentage Penalty Amount",
                "PercentagePenaltyAmount",
                0.2f,
                "The percentage penalty which will be applied. Is Penalty Percentage must be true for this to have any effect."
            );
            flatPenaltyAmount = Plugin.config.Bind(
                "Flat Penalty Amount",
                "FlatPenaltyAmount",
                150,
                "The flat rate penalty which will be applied. Is Penalty Percentage must be false for this to have any effect."
            );
        }
    }
}

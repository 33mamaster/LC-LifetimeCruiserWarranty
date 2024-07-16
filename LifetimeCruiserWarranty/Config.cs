using System;
using BepInEx.Configuration;
using LifetimeCruiserWarranty;

namespace LifetimeCruiserWarranty
{
    internal class Config
    {
        public static ConfigEntry<bool> applyPenalty;

        public static void Load()
        {
            applyPenalty = Plugin.config.Bind(
                "Apply Penalty",
                "ApplyPenalty",
                true,
                "Determines if leaving behind or destroying the Company Cruiser will incur a credit penalty."
            );
        }
    }
}

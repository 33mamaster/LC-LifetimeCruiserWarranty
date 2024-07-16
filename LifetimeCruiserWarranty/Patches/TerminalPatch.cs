using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifetimeCruiserWarranty;
using HarmonyLib;

namespace LifetimeCruiserWarranty.Patches
{
	[HarmonyPatch(typeof(Terminal))]
    internal class TerminalPatch
    {
        [HarmonyPatch("BuyVehicleServerRpc")]
        [HarmonyPostfix]
        private static void BuyVehicleServerRpcPatch(ref bool ___hasWarrantyTicket)
        {
            ___hasWarrantyTicket = true;
        }

        [HarmonyPatch("BuyVehicleClientRpc")]
        [HarmonyPostfix]
        private static void BuyVehicleClientRpcPatch(ref bool ___hasWarrantyTicket)
        {
            ___hasWarrantyTicket = true;
        }
    }
}

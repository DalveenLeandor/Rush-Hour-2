﻿using Harmony;
using RushHour2.Citizens.Reporting;

namespace RushHour2.Citizens.Extensions
{
    public static class ResidentAIExtensions
    {
        public static bool FindAFunActivity(this ResidentAI residentAI, uint citizenId, ref Citizen citizen, ushort proximityBuilding)
        {
            var visitMonument = SimulationManager.instance.m_randomizer.Int32(10) < 3;
            var proximityBuildingInstance = BuildingManager.instance.m_buildings.m_buffer[proximityBuilding];

            if (visitMonument && proximityBuilding != 0)
            {
                var monument = residentAI.FindSomewhere(citizenId, ref citizen, proximityBuildingInstance, new[] { ItemClass.Service.Monument }, new[] { ItemClass.SubService.None });
                if (monument != 0)
                {
                    residentAI.GoToBuilding(citizenId, ref citizen, monument);

                    return true;
                }
            }

            var foundBuilding = residentAI.FindSomewhere(citizenId, ref citizen, proximityBuildingInstance, new[] { ItemClass.Service.Beautification, ItemClass.Service.Commercial, ItemClass.Service.Natural, ItemClass.Service.Tourism }, new[] { ItemClass.SubService.None });
            if (foundBuilding != 0)
            {
                residentAI.GoToBuilding(citizenId, ref citizen, foundBuilding);

                return true;
            }

            return false;
        }

        public static bool FindAShop(this ResidentAI residentAI, uint citizenId, ref Citizen citizen)
        {
            var buildingId = citizen.GetBuilding();

            return residentAI.FindAShop(citizenId, ref citizen, buildingId);
        }

        public static bool FindAShop(this ResidentAI residentAI, uint citizenId, ref Citizen citizen, ushort proximityBuilding)
        {
            var proximityBuildingInstance = BuildingManager.instance.m_buildings.m_buffer[proximityBuilding];
            var foundBuilding = residentAI.FindSomewhere(citizenId, ref citizen, proximityBuildingInstance, new[] { ItemClass.Service.Commercial }, new[] { ItemClass.SubService.CommercialEco, ItemClass.SubService.CommercialHigh, ItemClass.SubService.CommercialLow });

            if (foundBuilding != 0)
            {
                residentAI.GoToBuilding(citizenId, ref citizen, foundBuilding);

                return true;
            }

            return false;
        }
    }
}

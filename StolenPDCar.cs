using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using FivePD.API;
using FivePD.API.Utils;
using CitizenFX.Core.Native;

namespace First_FivePD_Callout
{
    [CalloutProperties("Stolen Police Car","SkylarPlayz348","1.0.0")]
    public class StolenPoliceCar : Callout
    {
        private Vehicle car;
        private Ped suspect;

        public StolenPoliceCar()
        {
            Random rand = new Random();
            float offsetX = rand.Next(100, 700);
            float offsetY = rand.Next(100, 700);

            InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(new Vector3(offsetX, offsetY, 0))));
            ShortName = "Stolen Police Car";
            CalloutDescription = "A police car has been stolen";
            ResponseCode = 3;
            StartDistance = 200f;
        }

        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            suspect = await SpawnPed(RandomUtils.GetRandomPed(), Location + 1);
            suspect.AlwaysKeepTask = true;
            suspect.BlockPermanentEvents = true;

            car = await SpawnVehicle(VehicleHash.Police, Location);
            suspect.SetIntoVehicle(car, VehicleSeat.Driver);

            API.SetDriveTaskDrivingStyle(suspect.GetHashCode(), 786956);
            
            suspect.Task.FleeFrom(player);

            suspect.AttachBlip();

        }

        public async override Task OnAccept()
        {
            InitBlip();
            UpdateData();
        }


    }
}

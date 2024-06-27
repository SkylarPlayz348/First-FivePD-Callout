using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using FivePD.API;
using FivePD.API.Utils;
using static System.Net.Mime.MediaTypeNames;


namespace First_FivePD_Callout
{
    [CalloutProperties("Police Downed", "Kil0h3rt", "0.1.0")]
    public class PoliceDown_EMS : Callout
    {
        Vehicle policeCar;
        Ped officer;

        public PoliceDown_EMS()
        {
            Random rand = new Random();
            float offsetX = rand.Next(100, 700);
            float offsetY = rand.Next(100, 700);

            InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(new Vector3(offsetX, offsetY, 0))));
            ShortName = "Officer Down";
            CalloutDescription = "A Police Officer is Down.";
            ResponseCode = 3;
            StartDistance = 200f;
        }
        
        public async override void OnStart(Ped player)
        {
            officer = await SpawnPed(PedHash.Cop01SFY, Location + 1);
            while (!HasAnimDictLoaded("combat@damage@writhe"))
            {
                RequestAnimDict("combat@damage@writhe");
                Wait(100);
            }
            TaskPlayAnim(officer.NetworkId, "combat@damage@writhe", "writhe_loop", 1.0f, 8.0f, -1, 1, -1, false, false, false);
        }
        public override void OnBackupCalled(int code)
        {
            if (code == 99)
            {
                Console.WriteLine("Code: " + code);
            }
        }

        public async override Task OnAccept()
        {
            InitBlip();
            UpdateData();
        }
    }
}

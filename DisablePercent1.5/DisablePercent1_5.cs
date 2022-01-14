using System.Diagnostics;
using System.Reflection;
using Modding;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Vasi;

namespace DisablePercent
{
    public class DisablePercent : Mod
    {
        internal static DisablePercent Instance;
        public override string GetVersion()
        {
            return FileVersionInfo.GetVersionInfo(Assembly.GetAssembly(typeof(DisablePercent)).Location).FileVersion; 
        }
        public override void Initialize()
        {
            Instance = this;
            Log("Initializing DisablePercent");
            On.PlayMakerFSM.OnEnable += disablePercentInInv;
        }
        public void Unload()
        {
            On.PlayMakerFSM.OnEnable -= disablePercentInInv;
        }
        private static void disablePercentInInv(On.PlayMakerFSM.orig_OnEnable orig, PlayMakerFSM self)
        {
            orig(self);
            if (self.FsmName == "UI Inventory" && self.gameObject.name == "Inv")
            {
                self.GetState("Completion Rate").GetAction<BuildString>().stringParts = new FsmString[] { "No Peeking" };
            }
        }
    }
}

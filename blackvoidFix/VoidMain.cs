using Smod2;
using Smod2.Attributes;
using System.Collections.Generic;

namespace BlackVoidFix
{
	[PluginDetails(
		author = "Mith",
		name = "blackvoidFix",
		description = "Respawns people when they die to anticheat at the start of the round because of a glitch with map loading.",
		id = "mith.blackvoidfix",
		version = "1.03",
		SmodMajor = 3,
		SmodMinor = 3,
		SmodRevision = 0
		)]
	class VoidMain : Plugin
	{
		public static Dictionary<string, bool> checkSteamIdIfDisconnected = new Dictionary<string, bool>();
		public static Dictionary<string, Smod2.API.Role> checkSteamIdForRole = new Dictionary<string, Smod2.API.Role>();

		public override void OnDisable()
		{
			this.Info($"{this.Details.name}(Version:{this.Details.version}) has been disabled.");
		}

		public override void OnEnable()
		{
			this.Info($"{this.Details.name}(Version:{this.Details.version}) has been enabled.");
		}

		public override void Register()
		{
			this.AddCommand("void_version", new VoidVersion(this));
			this.AddCommand("void_disable", new BlackVoidDisable(this));
			this.AddEventHandlers(new VoidEventHandler(this));

			this.AddConfig(new Smod2.Config.ConfigSetting("void_secondstorespawn", 5, Smod2.Config.SettingType.NUMERIC, true, "How many seconds till you stop respawning people."));
		}
	}
}
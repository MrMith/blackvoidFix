using Smod2;
using Smod2.EventHandlers;
using Smod2.Events;

namespace BlackVoidFix
{
	class VoidEventHandler : IEventHandlerPlayerDie, IEventHandlerPlayerJoin, IEventHandlerRoundEnd
	{
		private readonly Plugin plugin;

		bool value = false;

		public VoidEventHandler(Plugin plugin)
		{
			this.plugin = plugin;
		}

		public void OnPlayerDie(PlayerDeathEvent ev)
		{
			if (ev.DamageTypeVar == Smod2.API.DamageType.FLYING || ev.DamageTypeVar == Smod2.API.DamageType.NONE && Smod2.PluginManager.Manager.Server.Round.Duration <= plugin.GetConfigInt("void_secondstorespawn"))
			{
				VoidMain.checkIPIfDisconnected[ev.Player.IpAddress] = true;
				VoidMain.checkIPForRole[ev.Player.IpAddress] = ev.Player.TeamRole.Role;
				ev.Player.Disconnect("Seems like you experienced a glitch! To reconnect fast Go to Play -> Join Game -> Direct Connect -> Connect. Don't put anything in the IP field.");
			} // Above can have some issues and wasn't consistant when I tried to access it, for example instead of showing direct connect it would skip to server browser :(
		}

		public void OnPlayerJoin(PlayerJoinEvent ev)
		{
			if (VoidMain.checkIPIfDisconnected.TryGetValue(ev.Player.IpAddress, out value))
			{
				ev.Player.ChangeRole(VoidMain.checkIPForRole[ev.Player.IpAddress]);
				VoidMain.checkIPForRole.Remove(ev.Player.IpAddress);
			}
		}

		public void OnRoundEnd(RoundEndEvent ev)
		{
			VoidMain.checkIPForRole.Clear();
			VoidMain.checkIPIfDisconnected.Clear();
		}
	}
}

using Smod2;
using Smod2.EventHandlers;
using Smod2.Events;

namespace BlackVoidFix
{
	class VoidEventHandler : IEventHandlerPlayerDie, IEventHandlerPlayerJoin, IEventHandlerRoundEnd, IEventHandlerWaitingForPlayers
	{
		private readonly Plugin plugin;

		int void_SecondsToRespawn = 0;

		bool value = false;

		public VoidEventHandler(Plugin plugin)
		{
			this.plugin = plugin;
		}

		public void OnPlayerDie(PlayerDeathEvent ev)
		{
			if (ev.DamageTypeVar == Smod2.API.DamageType.FLYING || ev.DamageTypeVar == Smod2.API.DamageType.NONE && Smod2.PluginManager.Manager.Server.Round.Duration <= void_SecondsToRespawn)
			{
				if (VoidMain.checkSteamIdIfDisconnected.TryGetValue(ev.Player.SteamId, out value))
				{
					if(value)
					{
						return;
					}
				}
				VoidMain.checkSteamIdIfDisconnected[ev.Player.SteamId] = true;
				VoidMain.checkSteamIdForRole[ev.Player.SteamId] = ev.Player.TeamRole.Role;
				ev.Player.Disconnect("Seems like you experienced a glitch! To reconnect fast Go to Play -> Join Game -> Back (bottom right) -> Direct Connect -> Connect. Don't put anything in the IP field.");
			}// Above can have some issues and wasn't consistant when I tried to access it, for example instead of showing direct connect it would skip to server browser :(

		}

		public void OnPlayerJoin(PlayerJoinEvent ev)
		{
			if (VoidMain.checkSteamIdIfDisconnected.TryGetValue(ev.Player.SteamId, out value))
			{
				if(value)
				{
					ev.Player.ChangeRole(VoidMain.checkSteamIdForRole[ev.Player.SteamId]);
					VoidMain.checkSteamIdForRole.Remove(ev.Player.SteamId);
					VoidMain.checkSteamIdIfDisconnected.Remove(ev.Player.SteamId);
				}
			}
		}

		public void OnRoundEnd(RoundEndEvent ev)
		{
			VoidMain.checkSteamIdForRole.Clear();
			VoidMain.checkSteamIdIfDisconnected.Clear();
		}

		public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
		{
			if(plugin.GetConfigBool("void_disable"))
			{
				plugin.PluginManager.DisablePlugin(plugin);
			}
			void_SecondsToRespawn = plugin.GetConfigInt("void_secondstorespawn");
		}
	}
}
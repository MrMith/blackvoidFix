using Smod2.Commands;
using Smod2;

namespace BlackVoidFix
{
	class VoidVersion : ICommandHandler
	{
		private Plugin plugin;

		public VoidVersion(Plugin plugin)
		{
			this.plugin = plugin;
		}

		public string GetCommandDescription()
		{
			return "Gets version for debugging";
		}

		public string GetUsage()
		{
			return "void_version";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			return new string[] { "mith.blackvoidfix is version " + plugin.Details.version };
		}
	}
}

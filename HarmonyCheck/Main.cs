using System.Linq;
using Verse;
using System.Text.RegularExpressions;
using System.Reflection;
using System;

namespace HarmonyCheck
{
	[StaticConstructorOnStartup]
	public static class HarmonyCheck
	{
		static Version minimumVersion = new Version("1.0.9.0");

		static HarmonyCheck()
		{
			foreach (var pack in LoadedModManager.RunningMods.ToList())
			{
				var assemblies = pack.assemblies.loadedAssemblies;
				var harmony = assemblies.FirstOrDefault(asm => asm.GetName().Name == "0Harmony");
				if (harmony == null) continue;

				var harmonyVersion = harmony.GetName().Version;
				if (harmonyVersion.CompareTo(minimumVersion) >= 0) continue;

				var info = GetMetaData(pack);
				var versionString = Regex.Replace(harmonyVersion.ToString(), @"(\.0)+$", string.Empty);

				var message = "Mod \"" + info.Name + "\" written by \"" + info.Author + "\" uses Harmony " + versionString + " which is too old and will create conflicts with other mods. Please inform the author to update Harmony!";
				LongEventHandler.QueueLongEvent(() =>
				{
					Find.WindowStack.Add(new HarmonyVersionDialog("Harmony library problem", message));
				}, null, false, null);
			}
		}

		static ModMetaData GetMetaData(ModContentPack pack)
		{
			var method = typeof(ModLister).GetMethod("GetModWithIdentifier", BindingFlags.NonPublic | BindingFlags.Static);
			return method.Invoke(null, new object[] { pack.Identifier }) as ModMetaData;
		}
	}
}
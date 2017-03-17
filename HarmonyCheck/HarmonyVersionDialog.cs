using UnityEngine;
using Verse;

namespace HarmonyCheck
{
	public class HarmonyVersionDialog : Window
	{
		private readonly string title;
		private readonly string message;

		public override Vector2 InitialSize
		{
			get { return new Vector2(400f, 300f); }
		}

		public HarmonyVersionDialog(string title, string message)
		{
			this.title = title;
			this.message = message;
			closeOnEscapeKey = true;
			doCloseButton = false;
			doCloseX = false;
			forcePause = true;
			absorbInputAroundWindow = true;
			layer = WindowLayer.SubSuper;
		}

		public override void DoWindowContents(Rect inRect)
		{
			Text.Font = GameFont.Medium;
			var titleRect = new Rect(inRect.x, inRect.y, inRect.width, 40);
			Widgets.Label(titleRect, title);
			Text.Font = GameFont.Small;
			Widgets.Label(new Rect(inRect.x, inRect.y + titleRect.height, inRect.width, inRect.height - titleRect.height), message);
			var closeButtonRect = new Rect(inRect.width / 2f - CloseButSize.x / 2f, inRect.height - CloseButSize.y, CloseButSize.x, CloseButSize.y);

			if (Widgets.ButtonText(closeButtonRect, "CloseButton".Translate()))
				Close();
		}
	}
}
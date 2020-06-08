using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Isometric
{

	/// <summary>
	/// Automatically loads images and fonts from the Content project
	/// TEXT-TEMPLATE GENERATED, DO NOT EDIT MANUALLY
	/// To re-generate, right click the .tt file and select "Run Custom tool"
	///   Or click Build->"Transform All T4 Templates"
	/// </summary>
	static class ContentLoader
	{
		public static Texture2D altSelector { get; private set; }
		public static Texture2D iso_land { get; private set; }
		public static Texture2D plains { get; private set; }
		public static Texture2D selector { get; private set; }
		public static Texture2D snow { get; private set; }
		public static Texture2D UDT_Aggressor { get; private set; }
		public static SpriteFont Arial { get; private set; }
		
		public static void Initialize(ContentManager cm)
		{

			altSelector = cm.Load<Texture2D>(@"altSelector");
			iso_land = cm.Load<Texture2D>(@"iso_land");
			plains = cm.Load<Texture2D>(@"plains");
			selector = cm.Load<Texture2D>(@"selector");
			snow = cm.Load<Texture2D>(@"snow");
			UDT_Aggressor = cm.Load<Texture2D>(@"UDT_Aggressor");
			Arial = cm.Load<SpriteFont>(@"Arial");
		}
	}
}
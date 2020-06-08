using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


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
		public static Dictionary<string, Texture2D> AllTextures { get; private set; }
		public static Texture2D ash { get; private set; }
		public static Texture2D battlefield { get; private set; }
		public static Texture2D battlefield_2 { get; private set; }
		public static Texture2D battlefield_3 { get; private set; }
		public static Texture2D battlefield_4 { get; private set; }
		public static Texture2D dirt { get; private set; }
		public static Texture2D foliage { get; private set; }
		public static Texture2D forest { get; private set; }
		public static Texture2D mud { get; private set; }
		public static Texture2D plains { get; private set; }
		public static Texture2D plainsHills { get; private set; }
		public static Texture2D plainsMountains { get; private set; }
		public static Texture2D selector { get; private set; }
		public static Texture2D UDT_Aggressor { get; private set; }
		public static Texture2D UDT_Aggressor_tint { get; private set; }
		public static SpriteFont Arial { get; private set; }
		
		public static void Initialize(ContentManager cm)
		{
			AllTextures = new Dictionary<string, Texture2D>();

			ash = cm.Load<Texture2D>(@"ash");
			AllTextures.Add("ash", ash);

			battlefield = cm.Load<Texture2D>(@"battlefield");
			AllTextures.Add("battlefield", battlefield);

			battlefield_2 = cm.Load<Texture2D>(@"battlefield_2");
			AllTextures.Add("battlefield_2", battlefield_2);

			battlefield_3 = cm.Load<Texture2D>(@"battlefield_3");
			AllTextures.Add("battlefield_3", battlefield_3);

			battlefield_4 = cm.Load<Texture2D>(@"battlefield_4");
			AllTextures.Add("battlefield_4", battlefield_4);

			dirt = cm.Load<Texture2D>(@"dirt");
			AllTextures.Add("dirt", dirt);

			foliage = cm.Load<Texture2D>(@"foliage");
			AllTextures.Add("foliage", foliage);

			forest = cm.Load<Texture2D>(@"forest");
			AllTextures.Add("forest", forest);

			mud = cm.Load<Texture2D>(@"mud");
			AllTextures.Add("mud", mud);

			plains = cm.Load<Texture2D>(@"plains");
			AllTextures.Add("plains", plains);

			plainsHills = cm.Load<Texture2D>(@"plainsHills");
			AllTextures.Add("plainsHills", plainsHills);

			plainsMountains = cm.Load<Texture2D>(@"plainsMountains");
			AllTextures.Add("plainsMountains", plainsMountains);

			selector = cm.Load<Texture2D>(@"selector");
			AllTextures.Add("selector", selector);

			UDT_Aggressor = cm.Load<Texture2D>(@"UDT_Aggressor");
			AllTextures.Add("UDT_Aggressor", UDT_Aggressor);

			UDT_Aggressor_tint = cm.Load<Texture2D>(@"UDT_Aggressor_tint");
			AllTextures.Add("UDT_Aggressor_tint", UDT_Aggressor_tint);

			Arial = cm.Load<SpriteFont>(@"Arial");
		}
	}
}
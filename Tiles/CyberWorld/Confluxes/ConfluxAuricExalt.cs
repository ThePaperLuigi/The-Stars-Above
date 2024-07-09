using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Enums;
using System;
using ReLogic.Content;
using Terraria.Localization;
using StarsAbove.Utilities;
using StarsAbove.Items.Placeable.CyberWorld;
using StarsAbove.Systems;
using StarsAbove.Items.Prisms;
using Terraria.Audio;
using Terraria.GameContent;

namespace StarsAbove.Tiles.CyberWorld.Confluxes
{
	// Common code for a Master Mode boss relic
	// Contains comments for optional Item.placeStyle handling if you wish to add more relics but use the same tile type (then it would be wise to name this class something more generic like BossRelic)
	// And in case of wanting to add more relics but not wanting to go the optional way, scroll down to the bottom of the file
	public class ConfluxAuricExalt : ModTile
	{
		public const int FrameWidth = 18 * 3;
		public const int FrameHeight = 18 * 4;
		public const int HorizontalFrames = 1;
		public const int VerticalFrames = 1; // Optional: Increase this number to match the amount of relics you have on your extra sheet, if you choose to go the Item.placeStyle way

		public Asset<Texture2D> RelicTexture;

		// Every relic has its own extra floating part, should be 50x50. Optional: Expand this sheet if you want to add more, stacked vertically
		// If you do not go the optional way, and you extend from this class, you can override this to point to a different texture
		public virtual string RelicTextureName => "StarsAbove/Items/Prisms/AuricExaltPrism";

		// All relics use the same pedestal texture, this one is copied from vanilla
		public override string Texture => "StarsAbove/Tiles/CyberWorld/Confluxes/ConfluxPlinth";

		public override void Load()
		{
			if (!Main.dedServ)
			{
				// Cache the extra texture displayed on the pedestal
				RelicTexture = ModContent.Request<Texture2D>(RelicTextureName);
			}
		}

		public override void Unload()
		{
			// Unload the extra texture displayed on the pedestal
			RelicTexture = null;
		}

		public override void SetStaticDefaults()
		{
			//Main.tileShine[Type] = 400; // Responsible for golden particles
			Main.tileFrameImportant[Type] = true; // Any multitile requires this
			TileID.Sets.InteractibleByNPCs[Type] = true; // Town NPCs will palm their hand at this tile

			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4); // Relics are 3x4
			TileObjectData.newTile.LavaDeath = false; // Does not break when lava touches it
			TileObjectData.newTile.DrawYOffset = 2; // So the tile sinks into the ground
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft; // Player faces to the left
			TileObjectData.newTile.StyleHorizontal = false; // Based on how the alternate sprites are positioned on the sprite (by default, true)

			// Optional: If you decide to make your tile utilize different styles through Item.placeStyle, you need these, aswell as the code in SetDrawPositions
			// TileObjectData.newTile.StyleWrapLimitVisualOverride = 2;
			// TileObjectData.newTile.StyleMultiplier = 2;
			// TileObjectData.newTile.StyleWrapLimit = 2;
			// TileObjectData.newTile.styleLineSkipVisualOverride = 0;

			// Register an alternate tile data with flipped direction
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile); // Copy everything from above, saves us some code
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight; // Player faces to the right
			TileObjectData.addAlternate(1);

			// Register the tile data itself
			TileObjectData.addTile(Type);
			
			AddMapEntry(new Color(233, 207, 94), CreateMapEntryName());
		}
        public override bool RightClick(int i, int j)
        {
            if(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().hardlight >= 100)
            {
				if(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().andyerQuestProgress == 2)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().andyerQuestProgress = 3;

                }
                SoundEngine.PlaySound(SoundID.Item4, Main.LocalPlayer.Center);

                Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_GiftOrReward(), ModContent.ItemType<AuricExaltPrism>());
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().hardlight -= 100;

                float dustAmount = 44f;
                float randomConstant = MathHelper.ToRadians(Main.rand.Next(0, 360));
                if (Main.rand.NextBool())
                {
                    for (int ir = 0; ir < dustAmount; ir++)
                    {
                        Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                        spinningpoint5 += -Vector2.UnitY.RotatedBy(ir * ((float)Math.PI * 2f / dustAmount)) * new Vector2(115f, 4f);
                        spinningpoint5 = spinningpoint5.RotatedBy(Main.LocalPlayer.velocity.ToRotation() + randomConstant);
                        int dust = Dust.NewDust(Main.LocalPlayer.Center, 0, 0, DustID.GemTopaz);
                        Main.dust[dust].scale = 1.5f;
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].position = Main.LocalPlayer.Center + spinningpoint5;
                        Main.dust[dust].velocity = Main.LocalPlayer.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 13f;
                    }
                }
                else
                {
                    for (int ir = 0; ir < dustAmount; ir++)
                    {
                        Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                        spinningpoint5 += -Vector2.UnitY.RotatedBy(ir * ((float)Math.PI * 2f / dustAmount)) * new Vector2(115f, 4f);
                        spinningpoint5 = spinningpoint5.RotatedBy(Main.LocalPlayer.velocity.ToRotation() + randomConstant);
                        int dust = Dust.NewDust(Main.LocalPlayer.Center, 0, 0, DustID.GemSapphire);
                        Main.dust[dust].scale = 1.5f;
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].position = Main.LocalPlayer.Center + spinningpoint5;
                        Main.dust[dust].velocity = Main.LocalPlayer.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 13f;
                    }
                }
            }
            else
			{
                Main.NewText(LangHelper.GetTextValue("Common.HardlightNotEnough", 100 - Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().hardlight));

            }
            return true;
        }
        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return false;
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			// This code here infers the placeStyle the tile was placed with. Only required if you go the Item.placeStyle approach. You just need Item.NewItem otherwise
			// The placeStyle calculated here corresponds to whatever placeStyle you specified on your items that place this tile (Either through Item.placeTile or Item.DefaultToPlacableTile)
			int placeStyle = frameX / FrameWidth;

			int itemType = 0;


            if (itemType > 0)
			{
				// Spawn the item
				Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 32, itemType);
			}
		}
		public override bool CreateDust(int i, int j, ref int type)
		{
			return false;
		}
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];

            // If you are using ModTile.SpecialDraw or PostDraw or PreDraw, use this snippet and add zero to all calls to spriteBatch.Draw
            // The reason for this is to accommodate the shift in drawing coordinates that occurs when using the different Lighting mode
            // Press Shift+F9 to change lighting modes quickly to verify your code works for all lighting modes
            Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);

            // Because height of third tile is different we change it
            int height = tile.TileFrameY;

            // Offset along the Y axis depending on the current frame
            int frameYOffset = Main.tileFrame[Type] * AnimationFrameHeight;

            // Firstly we draw the original texture and then glow mask texture
            spriteBatch.Draw(
                TextureAssets.Tile[Type].Value,
                new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero,
                new Rectangle(tile.TileFrameX, tile.TileFrameY + frameYOffset, 16, height),
                Lighting.GetColor(i, j), 0f, default, 1f, SpriteEffects.None, 0f);
            // Make sure to draw with Color.White or at least a color that is fully opaque
            // Achieve opaqueness by increasing the alpha channel closer to 255. (lowering closer to 0 will achieve transparency)
            spriteBatch.Draw(
                ModContent.Request<Texture2D>(Texture + "_Glow").Value,
                new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero,
                new Rectangle(tile.TileFrameX, tile.TileFrameY + frameYOffset, 16, height),
                Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);


        }
        public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
		{
			// Only required If you decide to make your tile utilize different styles through Item.placeStyle

			// This preserves its original frameX/Y which is required for determining the correct texture floating on the pedestal, but makes it draw properly
			tileFrameX %= FrameWidth; // Clamps the frameX
			tileFrameY %= FrameHeight * 2; // Clamps the frameY (two horizontally aligned place styles, hence * 2)
		}

		public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
		{
			// Since this tile does not have the hovering part on its sheet, we have to animate it ourselves
			// Therefore we register the top-left of the tile as a "special point"
			// This allows us to draw things in SpecialDraw
			

			if (drawData.tileFrameX % FrameWidth == 0 && drawData.tileFrameY % FrameHeight == 0)
			{
				Main.instance.TilesRenderer.AddSpecialLegacyPoint(i, j);
			}

			

		}

		public override void SpecialDraw(int i, int j, SpriteBatch spriteBatch)
		{
			// This is lighting-mode specific, always include this if you draw tiles manually
			Vector2 offScreen = new Vector2(Main.offScreenRange);
			if (Main.drawToScreen)
			{
				offScreen = Vector2.Zero;
			}

			// Take the tile, check if it actually exists
			Point p = new Point(i, j);
			Tile tile = Main.tile[p.X, p.Y];
			if (tile == null || !tile.HasTile)
			{
				return;
			}

			// Get the initial draw parameters
			Texture2D texture = RelicTexture.Value;

			int frameY = tile.TileFrameX / FrameWidth; // Picks the frame on the sheet based on the placeStyle of the item
			Rectangle frame = texture.Frame(HorizontalFrames, VerticalFrames, 0, frameY);

			Vector2 origin = frame.Size() / 2f;
			Vector2 worldPos = p.ToWorldCoordinates(24f, 104f);

			Color color = Color.White;

			bool direction = tile.TileFrameY / FrameHeight != 0; // This is related to the alternate tile data we registered before
			SpriteEffects effects = direction ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

			// Some math magic to make it smoothly move up and down over time
			const float TwoPi = (float)Math.PI * 2f;
			float offset = (float)Math.Sin(Main.GlobalTimeWrappedHourly * TwoPi / 5f);
			Vector2 drawPos = worldPos + offScreen - Main.screenPosition + new Vector2(0f, -95f) + new Vector2(0f, offset * 4f);

			// Draw the main texture
			spriteBatch.Draw(texture, drawPos, frame, color, 0f, origin, 1f, effects, 0f);

			// Draw the periodic glow effect
			float scale = (float)Math.Sin(Main.GlobalTimeWrappedHourly * TwoPi / 2f) * 0.3f + 0.7f;
			Color effectColor = color;
			effectColor.A = 0;
			effectColor = effectColor * 0.1f * scale;
			for (float num5 = 0f; num5 < 1f; num5 += 355f / (678f * (float)Math.PI))
			{
				spriteBatch.Draw(texture, drawPos + (TwoPi * num5).ToRotationVector2() * (6f + offset * 2f), frame, effectColor, 0f, origin, 1f, effects, 0f);
			}
		}
	}
}
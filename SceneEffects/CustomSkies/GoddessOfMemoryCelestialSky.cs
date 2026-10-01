using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace StarsAbove.SceneEffects.CustomSkies
{
    public class GoddessOfMemoryCelestialSky : CustomSky
    {
        private struct Particle
        {
            public Vector2 Position;
            public float Depth;
            public float Scale;
            public float Phase;
            public float Frequency;
            public int TextureIndex;
            public int Life;
        }

        private readonly UnifiedRandom random = new UnifiedRandom();
        private readonly Particle[] meteors = new Particle[16];
        private readonly Particle[] pillars = new Particle[6];
        private readonly Particle[] stars = new Particle[120];
        private readonly Particle[] bolts = new Particle[24];
        private readonly Dictionary<Texture2D, Texture2D> desaturatedTextures = new Dictionary<Texture2D, Texture2D>();
        private Asset<Texture2D> meteorTexture;
        private Asset<Texture2D> beamTexture;
        private Asset<Texture2D>[] rockTextures;
        private Asset<Texture2D>[] starTextures;
        private Asset<Texture2D> boltTexture;
        private Asset<Texture2D> flashTexture;
        private Vector2 lastScreenPosition;
        private Point screenSize;
        private float opacity;
        private float animationTime;
        private int nextBolt;
        private bool initialized;
        private bool isActive;

        public override void OnLoad()
        {
            if (Main.dedServ)
            {
                return;
            }

            meteorTexture = Main.Assets.Request<Texture2D>("Images/Misc/SolarSky/Meteor");
            beamTexture = Main.Assets.Request<Texture2D>("Images/Misc/NebulaSky/Beam");
            boltTexture = Main.Assets.Request<Texture2D>("Images/Misc/VortexSky/Bolt");
            flashTexture = Main.Assets.Request<Texture2D>("Images/Misc/VortexSky/Flash");
            rockTextures = new Asset<Texture2D>[3];
            starTextures = new Asset<Texture2D>[2];

            for (int i = 0; i < rockTextures.Length; i++)
            {
                rockTextures[i] = Main.Assets.Request<Texture2D>("Images/Misc/NebulaSky/Rock_" + i);
            }

            for (int i = 0; i < starTextures.Length; i++)
            {
                starTextures[i] = Main.Assets.Request<Texture2D>("Images/Misc/StarDustSky/Star " + i);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Main.dedServ)
            {
                return;
            }

            if (Main.gameMenu)
            {
                Reset();
                return;
            }

            opacity = MathHelper.Clamp(opacity + (isActive ? 0.01f : -0.01f), 0f, 1f);
            if (opacity <= 0f)
            {
                initialized = false;
                return;
            }

            Vector2 cameraMovement = Main.screenPosition - lastScreenPosition;
            if (!initialized || screenSize.X != Main.screenWidth || screenSize.Y != Main.screenHeight || cameraMovement.LengthSquared() > 4000000f)
            {
                InitializeParticles();
                cameraMovement = Vector2.Zero;
            }

            lastScreenPosition = Main.screenPosition;
            animationTime += 1f / 60f;

            for (int i = 0; i < meteors.Length; i++)
            {
                ref Particle meteor = ref meteors[i];
                meteor.Position -= cameraMovement / meteor.Depth;
                meteor.Position.X = Wrap(meteor.Position.X, -400f, Main.screenWidth + 400f);
                meteor.Position.Y = Wrap(meteor.Position.Y, -400f, Main.screenHeight + 400f);
                meteor.Position += new Vector2(-20f, 20f) / meteor.Depth;
                if (meteor.Position.X < -400f || meteor.Position.Y > Main.screenHeight + 400f)
                {
                    meteor.Position = random.NextBool()
                        ? new Vector2(random.NextFloat() * (Main.screenWidth + 800f) - 400f, -400f)
                        : new Vector2(Main.screenWidth + 400f, random.NextFloat() * (Main.screenHeight + 400f) - 400f);
                }
            }

            for (int i = 0; i < pillars.Length; i++)
            {
                pillars[i].Position.X = Wrap(pillars[i].Position.X - cameraMovement.X / pillars[i].Depth, -400f, Main.screenWidth + 400f);
            }

            for (int i = 0; i < stars.Length; i++)
            {
                ref Particle star = ref stars[i];
                star.Position -= cameraMovement / star.Depth;
                star.Position.X = Wrap(star.Position.X, -200f, Main.screenWidth + 200f);
                star.Position.Y = Wrap(star.Position.Y, -200f, Main.screenHeight + 200f);
            }

            for (int i = 0; i < bolts.Length; i++)
            {
                if (bolts[i].Life > 0)
                {
                    bolts[i].Position -= cameraMovement / bolts[i].Depth;
                    bolts[i].Life--;
                }
            }

            if (--nextBolt <= 0)
            {
                for (int i = 0; i < bolts.Length; i++)
                {
                    if (bolts[i].Life > 0)
                    {
                        continue;
                    }

                    bolts[i].Position = new Vector2(random.NextFloat() * Main.screenWidth, random.NextFloat() * Main.screenHeight * 0.4f - 200f);
                    bolts[i].Depth = random.NextFloat() * 8f + 2f;
                    bolts[i].Life = 30;
                    break;
                }

                nextBolt = random.Next(20, 61);
            }
        }

        private void InitializeParticles()
        {
            screenSize = new Point(Main.screenWidth, Main.screenHeight);
            lastScreenPosition = Main.screenPosition;
            animationTime = 0f;
            nextBolt = 1;
            Array.Clear(bolts, 0, bolts.Length);

            for (int i = 0; i < meteors.Length; i++)
            {
                meteors[i].Position = new Vector2(random.NextFloat() * (Main.screenWidth + 800f) - 400f, random.NextFloat() * (Main.screenHeight + 800f) - 400f);
                meteors[i].Depth = random.NextFloat() * 8f + 1.8f;
                meteors[i].Scale = random.NextFloat() * 0.5f + 1f;
                meteors[i].Phase = random.Next(12);
            }

            for (int i = 0; i < pillars.Length; i++)
            {
                pillars[i].Position = new Vector2((i + random.NextFloat()) / pillars.Length * (Main.screenWidth + 800f) - 400f, -400f);
                pillars[i].Depth = random.NextFloat() * 8f + 7f;
                pillars[i].Phase = random.NextFloat();
            }

            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].Position = new Vector2(random.NextFloat() * (Main.screenWidth + 400f) - 200f, random.NextFloat() * (Main.screenHeight + 400f) - 200f);
                stars[i].Depth = random.NextFloat() * 8f + 1.5f;
                stars[i].TextureIndex = random.Next(starTextures.Length);
                stars[i].Phase = random.NextFloat() * MathHelper.TwoPi;
                stars[i].Scale = random.NextFloat() * 5f;
                stars[i].Frequency = random.NextFloat() + 1f;
            }

            Array.Sort(meteors, (first, second) => second.Depth.CompareTo(first.Depth));
            Array.Sort(pillars, (first, second) => second.Depth.CompareTo(first.Depth));
            Array.Sort(stars, (first, second) => second.Depth.CompareTo(first.Depth));
            initialized = true;
        }

        private static float Wrap(float value, float minimum, float maximum)
        {
            float range = maximum - minimum;
            return minimum + ((value - minimum) % range + range) % range;
        }

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (Main.dedServ || Main.gameMenu || !initialized || opacity <= 0f || maxDepth < float.MaxValue || minDepth >= float.MaxValue)
            {
                return;
            }

            DrawPillars(spriteBatch);
            DrawStars(spriteBatch);
            DrawBolts(spriteBatch);
            DrawMeteors(spriteBatch);
        }

        private Texture2D GetDesaturatedTexture(Asset<Texture2D> asset)
        {
            Texture2D source = asset.Value;
            if (desaturatedTextures.TryGetValue(source, out Texture2D texture))
            {
                return texture;
            }

            Color[] pixels = new Color[source.Width * source.Height];
            source.GetData(pixels);
            for (int i = 0; i < pixels.Length; i++)
            {
                Color color = pixels[i];
                byte brightness = (byte)((color.R * 299 + color.G * 587 + color.B * 114 + 500) / 1000);
                pixels[i] = new Color(brightness, brightness, brightness, color.A);
            }

            texture = new Texture2D(source.GraphicsDevice, source.Width, source.Height);
            texture.SetData(pixels);
            desaturatedTextures.Add(source, texture);
            return texture;
        }

        private void DrawPillars(SpriteBatch spriteBatch)
        {
            Texture2D beam = GetDesaturatedTexture(beamTexture);
            float beamHeight = Main.screenHeight + 800f;
            for (int i = 0; i < pillars.Length; i++)
            {
                Particle pillar = pillars[i];
                float width = 450f / pillar.Depth;
                spriteBatch.Draw(beam, pillar.Position, null, Color.White * 0.2f * opacity, 0f, Vector2.Zero, new Vector2(width / beam.Width, beamHeight / beam.Height), SpriteEffects.None, 0f);

                for (int j = 0; j < 34; j++)
                {
                    float progress = j * 0.03f;
                    float height = 1f - (progress + animationTime * 0.02f + pillar.Phase) % 1f;
                    Texture2D texture = GetDesaturatedTexture(rockTextures[j % rockTextures.Length]);
                    Vector2 position = pillar.Position + new Vector2((float)Math.Sin(progress * 1582f) * width * 0.5f + width * 0.5f, height * beamHeight);
                    spriteBatch.Draw(texture, position, null, Color.White * height * opacity, height * 20f, texture.Size() * 0.5f, 0.9f, SpriteEffects.None, 0f);
                }
            }
        }

        private void DrawStars(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < stars.Length; i++)
            {
                Particle star = stars[i];
                float brightness = MathHelper.Clamp((float)Math.Sin(star.Frequency * animationTime + star.Phase) * star.Scale + star.Scale, 0f, 1f);
                float flicker = (float)Math.Sin(star.Frequency * animationTime * 5f + star.Phase) * 0.1f - 0.1f;
                Texture2D texture = GetDesaturatedTexture(starTextures[star.TextureIndex]);
                float scale = (0.5f / star.Depth + 0.5f) * (brightness * 0.3f + 0.7f);
                spriteBatch.Draw(texture, star.Position, null, Color.White * brightness * 0.8f * (1f - flicker) * opacity, 0f, texture.Size() * 0.5f, scale, SpriteEffects.None, 0f);
            }
        }

        private void DrawBolts(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < bolts.Length; i++)
            {
                Particle bolt = bolts[i];
                if (bolt.Life <= 0)
                {
                    continue;
                }

                Texture2D texture = GetDesaturatedTexture(bolt.Life > 26 && bolt.Life % 2 == 0 ? flashTexture : boltTexture);
                spriteBatch.Draw(texture, bolt.Position, null, Color.White * (bolt.Life / 30f) * opacity, 0f, Vector2.Zero, 5f / bolt.Depth, SpriteEffects.None, 0f);
            }
        }

        private void DrawMeteors(SpriteBatch spriteBatch)
        {
            Texture2D texture = GetDesaturatedTexture(meteorTexture);
            int frameHeight = texture.Height / 4;
            for (int i = 0; i < meteors.Length; i++)
            {
                Particle meteor = meteors[i];
                int frame = (int)(animationTime * 60f + meteor.Phase) % 12 / 3;
                Rectangle source = new Rectangle(0, frame * frameHeight, texture.Width, frameHeight);
                spriteBatch.Draw(texture, meteor.Position, source, Color.White * opacity, 0f, Vector2.Zero, 5f / meteor.Depth * meteor.Scale, SpriteEffects.None, 0f);
            }
        }

        public override void Reset()
        {
            isActive = false;
            opacity = 0f;
            animationTime = 0f;
            nextBolt = 0;
            initialized = false;
            Array.Clear(bolts, 0, bolts.Length);
        }

        public override void Activate(Vector2 position, params object[] args)
        {
            isActive = true;
        }

        public override void Deactivate(params object[] args)
        {
            isActive = false;
        }

        public override bool IsActive()
        {
            return isActive || opacity > 0f;
        }

        public void Unload()
        {
            Main.QueueMainThreadAction(() =>
            {
                foreach (Texture2D texture in desaturatedTextures.Values)
                {
                    texture.Dispose();
                }

                desaturatedTextures.Clear();
            });
        }
    }
}

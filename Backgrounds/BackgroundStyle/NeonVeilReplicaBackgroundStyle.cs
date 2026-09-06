using Terraria.ModLoader;

namespace StarsAbove.Backgrounds.BackgroundStyle
{
    public class NeonVeilReplicaBackgroundStyle : ModSurfaceBackgroundStyle
    {

        // Use this to keep far Backgrounds like the mountains.
        public override void ModifyFarFades(float[] fades, float transitionSpeed)
        {
            for (int i = 0; i < fades.Length; i++)
            {
                if (i == Slot)
                {
                    fades[i] += transitionSpeed;
                    if (fades[i] > 1f)
                    {
                        fades[i] = 1f;
                    }
                }
                else
                {
                    fades[i] -= transitionSpeed;
                    if (fades[i] < 0f)
                    {
                        fades[i] = 0f;
                    }
                }
            }
        }


        public override int ChooseFarTexture()
        {
            return BackgroundTextureLoader.GetBackgroundSlot("StarsAbove/Backgrounds/NeonVeil/NeonVeilReplicaFar");
            //return -1;
        }

        private static int SurfaceFrameCounter;
        private static int SurfaceFrame;
        public override int ChooseMiddleTexture()
        {
            
            return BackgroundTextureLoader.GetBackgroundSlot("StarsAbove/Backgrounds/NeonVeil/NeonVeilReplicaMid");

        }

        public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
        {
            //return -1;
            return BackgroundTextureLoader.GetBackgroundSlot("StarsAbove/Backgrounds/NeonVeil/NeonVeilReplicaClose");
        }
    }
}
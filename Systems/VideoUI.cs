using Microsoft.Xna.Framework.Media;
using ReLogic.Content;
using System;
using Terraria.GameContent;
using Terraria.UI;

namespace StarsAbove.Systems;

public class UIVideo(Asset<Video> video) : UIElement
{
	private readonly Asset<Video>? video = video;
	private VideoPlayer videoPlayer = null;
	public bool StartVideo { get; set; } = true;
	public bool FinishedVideo { get; set; } = false;
	public Color BackgroundColor { get; set; } = Color.Black;

    public override void OnDeactivate()
    {
        // Clear the video player object
        videoPlayer?.Dispose();
        videoPlayer = null;
    }

    public override void Update(GameTime gameTime)
    {
		// Check if the video file is valid
        if (this.video?.Value is not Video video)
        {
            return;
        }

        // Ensure that the video player object exists
        videoPlayer ??= new();

        // Wait for the start signal to play the video
        if (StartVideo)
        {
            // Set videoplayer settings and then play the video 
            videoPlayer.IsLooped = false;
            videoPlayer.Play(video);

            // Set flags
            StartVideo = false;
            FinishedVideo = false;
        }

        // Wait for the video to finish playing
        if (videoPlayer.State != MediaState.Playing)
        {
            // Stop the video from playing
            videoPlayer.Stop();

            // Set flags
            FinishedVideo = true;
        }
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
	{
        // Check if the video is currently playing
        if (videoPlayer?.State != MediaState.Playing)
        {
            return;
        }

        // Get the size of the UI element.
		CalculatedStyle dimensions = GetDimensions();

        // Draw a black background to fill the entire screen
        spriteBatch.Draw(
            TextureAssets.MagicPixel.Value,
            dimensions.Position(),
            new Rectangle(0, 0, (int)Math.Ceiling(dimensions.Width), (int)Math.Ceiling(dimensions.Height)),
            BackgroundColor,
            0f,
            Vector2.Zero,
            1f,
            SpriteEffects.None,
            0);

        // Get the current frame of the video as a Texture2D
        Texture2D frameTexture = videoPlayer.GetTexture();

        // Get the size of the texture in pixels
        int width = frameTexture.Width;
        int height = frameTexture.Height;

        // Determine the scale on the X an Y
        float scaleX = dimensions.Width / width;
        float scaleY = dimensions.Height / height;

        // Get the smallest scale
        float scale = Math.Min(scaleX, scaleY);

        // Draw the frame of the video to the element with
        // the appropriate scale to fill the entire screen.
        // When not on 16:9 aspect ratio the black background
        // will be visible on the places not covered by the video.
		spriteBatch.Draw(
            frameTexture,
            dimensions.Center(),
            frameTexture.Bounds,
            Color.White,
            0f,
            frameTexture.Size() * 0.5f,
            scale,
            SpriteEffects.None,
            0f);
	}
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using ReLogic.Content;
using Terraria;
using Terraria.UI;

namespace StarsAbove.Systems;

public class UIVideo : UIElement
{
	private Asset<Video>? video;
	private VideoPlayer? videoPlayer;
	private bool pendingResize;

	public bool ScaleToFit { get; set; }
	public bool DoLoop { get; set; } = true;
	public bool WaitForStart { get; set; } = false;
	public bool StartVideo { get; set; } = true;
	public bool FinishedVideo { get; set; } = false;
	public bool AllowResizingDimensions { get; set; } = true;
	public bool RemoveFloatingPointsFromDrawPosition { get; set; }
	public float ImageScale { get; set; } = 1f;
	public float Rotation { get; set; }
	public Color Color { get; set; } = Color.White;
	public Vector2 NormalizedOrigin { get; set; }

	public UIVideo(Asset<Video> video)
	{
		SetVideo(video);
	}

	protected override void DrawSelf(SpriteBatch spriteBatch)
	{
		CalculatedStyle dimensions = GetDimensions();

		if (this.video?.Value is not Video video)
		{
			return;
		}

		EnsureInitialized();

		
		//true
		if (WaitForStart) //If you need to prompt the video to begin...
        {
			//false
			if (DoLoop && videoPlayer.IsLooped)//If DoLoop is true and the video looped once.
			{
				if (videoPlayer!.State != MediaState.Playing)
				{
					videoPlayer.Play(video);
				}
				//videoPlayer.IsLooped = false;
			}
			else
			{
				if (videoPlayer.IsLooped && !StartVideo) //Stop the video after the first loop.
				{
					videoPlayer.Stop();
				}
			}
			if (StartVideo) //If the video should start.
			{
				StartVideo = false;
				videoPlayer.IsLooped = false;
				FinishedVideo = false;

				videoPlayer.Play(video);
			}
			if (videoPlayer!.State != MediaState.Playing) //If the video ended.
			{
				videoPlayer.Stop();

				FinishedVideo = true;
				videoPlayer.IsLooped = true;
			}
		}
		else //If the video loops forever.
		{
			if (videoPlayer!.State != MediaState.Playing)
			{
				videoPlayer.IsLooped = true;

				videoPlayer.Play(video);
			}
		}
		
		

		// Perhaps this should be done in Update() instead.
		if (pendingResize && AllowResizingDimensions)
		{
			Width.Set(video.Width, 0f);
			Height.Set(video.Height, 0f);

			pendingResize = false;
		}

		var frameTexture = videoPlayer.GetTexture();

		if (ScaleToFit)
		{
			spriteBatch.Draw(frameTexture, dimensions.ToRectangle(), Color);
			return;
		}

		Vector2 size = frameTexture.Size();
		Vector2 position = dimensions.Position() + size * (1f - ImageScale) / 2f + size * NormalizedOrigin;

		if (RemoveFloatingPointsFromDrawPosition)
		{
			position = position.Floor();
		}

		spriteBatch.Draw(frameTexture, position, null, Color, Rotation, size * NormalizedOrigin, ImageScale, SpriteEffects.None, 0f);
	}

	public override void OnActivate()
		=> EnsureInitialized();

	public override void OnDeactivate()
	{
		videoPlayer?.Dispose();

		videoPlayer = null;
	}

	public void SetVideo(Asset<Video> video)
	{
		this.video = video;
		pendingResize = AllowResizingDimensions;
	}

	private void EnsureInitialized()
	{
		videoPlayer ??= new VideoPlayer();
	}
}
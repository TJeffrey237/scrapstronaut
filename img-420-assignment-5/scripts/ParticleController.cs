using Godot;
using System;

public partial class ParticleController : GpuParticles2D
{
	[Export] public float IntensitySpeed { get; set; } = 0.5f;
	[Export] public float ColorSpeed { get; set; } = 0.2f;
	
	[Export] public float IntensityMin { get; set; } = 0.05f;
	[Export] public float IntensityMax { get; set; } = 0.15f;

	private ShaderMaterial _shaderMaterial;

	public override void _Ready()
	{
		if (Material is ShaderMaterial sm)
		{
			_shaderMaterial = sm;
		}
		else
		{
			GD.PushError("GpuParticles2D must have a ShaderMaterial assigned to its 'Material' property.");
			SetProcess(false);
		}
	}

	public override void _Process(double delta)
	{
		if (_shaderMaterial == null) return;

		// convert time to seconds
		double time = Time.GetTicksMsec() / 1000.0;
		float fTime = (float)time;

		// Lerp based on a smooth sin wave output (0.0 to 1.0)
		float sinValue = Mathf.Sin(fTime * IntensitySpeed);
		float t = (sinValue + 1.0f) / 2.0f;
		float newIntensity = Mathf.Lerp(IntensityMin, IntensityMax, t);
		
		// update uniform in shader
		_shaderMaterial.SetShaderParameter("wave_intensity", newIntensity);

		// for animating the color gradient
		float hueShift = Mathf.PosMod(fTime * ColorSpeed, 1.0f);

		// get the current colors
		Color startColor = (Color)_shaderMaterial.GetShaderParameter("color_start");
		Color endColor = (Color)_shaderMaterial.GetShaderParameter("color_end");
		
		// for changing the start color
		startColor.H = Mathf.PosMod(startColor.H + hueShift * (float)delta, 1.0f);
		_shaderMaterial.SetShaderParameter("color_start", startColor);

		// for changing the end color
		endColor.H = Mathf.PosMod(endColor.H + hueShift * (float)delta, 1.0f);
		_shaderMaterial.SetShaderParameter("color_end", endColor);
	}
}

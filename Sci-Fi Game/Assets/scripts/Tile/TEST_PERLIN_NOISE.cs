using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_PERLIN_NOISE : MonoBehaviour
{
	// Width and height of the texture in pixels.
	public int pixWidth;
	public int pixHeight;

	// The origin of the sampled area in the plane.
	public float xOrg;
	public float yOrg;

	// The number of cycles of the basic noise pattern that are repeated
	// over the width and height of the texture.
	public float scale = 1.0F;

	private Texture2D noiseTex;
	private Color[] pix;
	private Renderer rend;

	public PERLIN_NOISE noise;

	public int seed;
	public int fractal;
	public bool auto_update = false;

	public float cutoff;
	public float multiplier = 2f;

	void Start()
	{
		rend = GetComponent<Renderer>();

		// Set up the texture and a Color array to hold pixels during processing.
		noiseTex = new Texture2D(pixWidth, pixHeight);
		pix = new Color[noiseTex.width * noiseTex.height];
		rend.material.mainTexture = noiseTex;

		noise = new PERLIN_NOISE(seed, new Vector2(scale, scale));
		CalcNoise();
	}

	void CalcNoise()
	{
		// For each pixel in the texture...
		float y = 0.0F;

		while (y < noiseTex.height)
		{
			float x = 0.0F;
			while (x < noiseTex.width)
			{
				float xCoord = xOrg + x / noiseTex.width * scale;
				float yCoord = yOrg + y / noiseTex.height * scale;
				//float sample = noise.Get_Noise_PERLIN_NOISE(new Vector2(xCoord, yCoord), cutoff);
				float sample = noise.Get_Fractal_PERLIN_NOISE(new Vector2(xCoord, yCoord), fractal, cutoff);
				pix[(int)y * noiseTex.width + (int)x] = new Color(sample* multiplier, sample * multiplier, sample * multiplier);
				x++;
			}
			y++;
		}

		// Copy the pixel data to the texture and load it into the GPU.
		noiseTex.SetPixels(pix);
		noiseTex.Apply();
	}

	void Update()
	{
		if (auto_update)
		{
			noise = new PERLIN_NOISE(seed, new Vector2(scale, scale));
			CalcNoise();
		}

		if (Input.GetKeyDown(KeyCode.M))
		{
			noise = new PERLIN_NOISE(seed, new Vector2(scale, scale));
			CalcNoise();
		}
	}
}

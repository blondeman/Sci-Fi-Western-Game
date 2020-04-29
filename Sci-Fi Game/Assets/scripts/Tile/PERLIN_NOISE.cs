using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PERLIN_NOISE
{
	public int seed;
	public Vector2 size;

	public PERLIN_NOISE(int seed, Vector2 size)
	{
		this.seed = seed;
		this.size = size;
	}

	public float Fade_PERLIN_NOISE(float input)
	{
		return input * input * input * (input * (input * 6.0f - 15.0f) + 10.0f);
	}

	public Vector2 Random_PERLIN_NOISE(Vector2 input)
	{
		float x = Mathf.Sin(Vector2.Dot(input, new Vector2(4.1242f + seed, 23.9245f + seed))) * 43758.5453123f;
		float frac_x = x - Mathf.Floor(x);
		float y = Mathf.Sin(Vector2.Dot(input, new Vector2(6184.5824f + seed, 1268.5913f + seed))) * 68915.1948524f;
		float frac_y = y - Mathf.Floor(y);
		Vector2 output = (new Vector2(frac_x, frac_y)* 2 - Vector2.one).normalized;
		return output;
	}

	Vector2 Floor_PERLIN_NOISE(Vector2 input)
	{
		return new Vector2(Mathf.Floor(input.x), Mathf.Floor(input.y));
	}
	
	public float Get_Noise_PERLIN_NOISE(Vector2 input, float cutoff)
	{
		Vector2 p0 = Floor_PERLIN_NOISE(input);
		Vector2 p1 = p0 + new Vector2(1, 0);
		Vector2 p2 = p0 + new Vector2(0, 1);
		Vector2 p3 = p0 + new Vector2(1, 1);
		
		Vector2 g0 = Random_PERLIN_NOISE(new Vector2(p0.x % size.x, p0.y % size.y));
		Vector2 g1 = Random_PERLIN_NOISE(new Vector2(p1.x % size.x, p1.y % size.y));
		Vector2 g2 = Random_PERLIN_NOISE(new Vector2(p2.x % size.x, p2.y % size.y));
		Vector2 g3 = Random_PERLIN_NOISE(new Vector2(p3.x % size.x, p3.y % size.y));

		float t0 = input.x - p0.x;
		float fade_t0 = Fade_PERLIN_NOISE(t0); /* Used for interpolation in horizontal direction */

		float t1 = input.y - p0.y;
		float fade_t1 = Fade_PERLIN_NOISE(t1); /* Used for interpolation in vertical direction. */

		/* Calculate dot products and interpolate.*/
		float p0p1 = (1.0f - fade_t0) * Vector2.Dot(g0, (input - p0)) + fade_t0 * Vector2.Dot(g1, (input - p1)); /* between upper two lattice points */
		float p2p3 = (1.0f - fade_t0) * Vector2.Dot(g2, (input - p2)) + fade_t0 * Vector2.Dot(g3, (input - p3)); /* between lower two lattice points */

		/* Calculate final result */
		float result = (1.0f - fade_t1) * p0p1 + fade_t1 * p2p3;
		result += cutoff;
		return result;
	}

	public float Get_Fractal_PERLIN_NOISE(Vector2 input, int fractal, float cutoff)
	{
		float value = 0;
		for(int i = 0; i < fractal; i++)
		{
			value += Get_Noise_PERLIN_NOISE(input * (1*i), cutoff) * (1f/Mathf.Pow(2,i));
		}
		return value;
	}
}
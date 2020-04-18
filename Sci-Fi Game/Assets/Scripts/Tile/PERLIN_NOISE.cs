using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PERLIN_NOISE
{
	int seed;

	public PERLIN_NOISE(int seed)
	{
		this.seed = seed;
	}

	public float Random_PERLIN_NOISE(Vector2 input)
	{
		float x = Mathf.Sin(Vector2.Dot(input, new Vector2(4.1242f + seed, 23.9245f + seed))) * 43758.5453123f;
		float frac = x - Mathf.Floor(x);
		return frac;
	}

	public float Get_Noise_PERLIN_NOISE(Vector2 input)
	{
		Vector2 i = new Vector2(Mathf.Floor(input.x), Mathf.Floor(input.y));
		Vector2 f = new Vector2(input.x - Mathf.Floor(input.x), input.y - Mathf.Floor(input.y));

		float a = Random_PERLIN_NOISE(i);
		float b = Random_PERLIN_NOISE(i + new Vector2(1, 0));
		float c = Random_PERLIN_NOISE(i + new Vector2(0, 1));
		float d = Random_PERLIN_NOISE(i + new Vector2(1, 1));

		Vector2 u = new Vector2(Mathf.SmoothStep(0, 1, f.x), Mathf.SmoothStep(0, 1, f.y));

		return (
		Mathf.Lerp(a, b, u.x) +
		(c - a) * u.y * (1 - u.x) +
		(d - b) * u.x * u.y
		);
	}
}

/*
 def noise(x, y, per):
    def surflet(gridX, gridY):
        distX, distY = abs(x-gridX), abs(y-gridY)
        polyX = 1 - 6*distX**5 + 15*distX**4 - 10*distX**3
        polyY = 1 - 6*distY**5 + 15*distY**4 - 10*distY**3
        hashed = perm[perm[int(gridX)%per] + int(gridY)%per]
        grad = (x-gridX)*dirs[hashed][0] + (y-gridY)*dirs[hashed][1]
        return polyX * polyY * grad
    intX, intY = int(x), int(y)
    return (surflet(intX+0, intY+0) + surflet(intX+1, intY+0) +
            surflet(intX+0, intY+1) + surflet(intX+1, intY+1))
*/

/*    vec2 i = floor(st);
    vec2 f = fract(st);

    // Four corners in 2D of a tile
    float a = random(i);
    float b = random(i + vec2(1.0, 0.0));
    float c = random(i + vec2(0.0, 1.0));
    float d = random(i + vec2(1.0, 1.0));

    // Smooth Interpolation

    // Cubic Hermine Curve.  Same as SmoothStep()
    vec2 u = f*f*f*(3.0-2.0*f);
    // u = smoothstep(0.,1.,f);

    // Mix 4 coorners percentages
    return mix(a, b, u.x) +
            (c - a)* u.y * (1.0 - u.x) +
            (d - b) * u.x * u.y;*/

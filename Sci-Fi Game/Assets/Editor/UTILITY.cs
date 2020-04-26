using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Threading;

public static class UTILITY
{
	public static List<T> FindAssetsByType<T>() where T : UnityEngine.Object
	{
		List<T> assets = new List<T>();
		string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T)));
		for (int i = 0; i < guids.Length; i++)
		{
			string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
			T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
			if (asset != null)
			{
				assets.Add(asset);
			}
		}
		return assets;
	}
	
	public static Texture2D Resize(Sprite sprite, int scale)
	{
		int width = (int)sprite.rect.width;
		int height = (int)sprite.rect.height;

		Texture2D image = new Texture2D(width * scale, height * scale);
		Color[] pixels = sprite.texture.GetPixels(
		(int)sprite.textureRect.x,
		(int)sprite.textureRect.y,
		width, height);

		Color[] new_pixels = new Color[width * scale * height * scale];
		int new_iterator = 0;

		for (int iterator = 0; iterator < width * height; iterator++)
		{
			for (int x = 0; x < scale; x++)
			{
				for (int y = 0; y < scale; y++)
				{
					new_pixels[new_iterator + (width * scale * y) + x] = pixels[iterator];
				}
			}

			if (iterator % width == width - 1)
			{
				new_iterator += width * scale * (scale - 1);
			}

			new_iterator += scale;
		}

		image.SetPixels(new_pixels);
		image.Apply();

		return image;
	}
}

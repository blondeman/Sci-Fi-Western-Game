using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PATHFINDER
{
	public NODE A_Star_PATHFINDER(NODE start, NODE end)
	{
		NODE current = new NODE();

		List<NODE> open_set = new List<NODE>();
		List<NODE> closed_set = new List<NODE>();

		open_set.Add(start);

		while (open_set.Count > 0)
		{
			NODE lowest = open_set[0];
			foreach(NODE n in open_set)
			{
				if (n.f_score < lowest.f_score)
					lowest = n;
			}
			current = lowest;

			if (current.Equals(end))
				return current;

			open_set.Remove(current);
			closed_set.Add(current);

			foreach(NODE child in current.Get_Surround_NODE())
			{
				//if (!TILE_RENDERER.instance.Node_Valid_TILE_RENDERER(child.x, child.y))
				//continue;
				
				if (Contains(closed_set.ToArray(), child)>=0)
					continue;

				child.g_score = current.g_score+Vector2.Distance(child.Get_Position_NODE(), current.Get_Position_NODE());
				child.h_score = Vector2.Distance(child.Get_Position_NODE(), end.Get_Position_NODE());
				child.f_score = child.g_score + child.h_score;

				int i = Contains(open_set.ToArray(), child);
				if (i >= 0)
				{
					if (child.g_score > open_set[i].g_score)
						continue;
					else
						open_set.RemoveAt(i);
				}

				open_set.Add(child);
				child.parent = current;
				child.distance = current.distance + 1;
			}
		}

		return null;
	}

	int Contains(NODE[] set, NODE node)
	{
		for(int n = 0;n<set.Length;n++)
		{
			if (node.Equals(set[n]))
				return n;
		}

		return -1;
	}
}

public class NODE
{
	public int x, y;
	public NODE parent;
	public int distance;
	public float h_score, g_score, f_score;

	public bool Equals(NODE other)
	{
		return (x == other.x && y == other.y);
	}

	public NODE(int x, int y)
	{
		this.x = x;
		this.y = y;
		h_score = 0;
		g_score = 0;
		f_score = 0;
		distance = 0;
	}

	public NODE()
	{
		x = 0;
		y = 0;
		h_score = 0;
		g_score = 0;
		f_score = 0;
		distance = 0;
	}

	public Vector2 Get_Position_NODE()
	{
		return new Vector2(x, y);
	}

	public NODE[] Get_Surround_NODE()
	{
		NODE[] array = new NODE[8];
		array[0] = new NODE(x + 1, y);
		array[1] = new NODE(x + 1, y + 1);
		array[2] = new NODE(x, y + 1);
		array[3] = new NODE(x - 1, y + 1);
		array[4] = new NODE(x - 1, y);
		array[5] = new NODE(x - 1, y - 1);
		array[6] = new NODE(x, y - 1);
		array[7] = new NODE(x + 1, y - 1);
		/*array[8] = new NODE(x + 2, y + 1);
		array[9] = new NODE(x + 2, y);
		array[10] = new NODE(x + 2, y - 1);
		array[11] = new NODE(x - 1, y + 2);
		array[12] = new NODE(x, y + 2);
		array[13] = new NODE(x + 1, y + 2);
		array[14] = new NODE(x - 2, y + 1);
		array[15] = new NODE(x - 2, y);
		array[16] = new NODE(x - 2, y - 1);
		array[17] = new NODE(x - 1, y - 2);
		array[18] = new NODE(x, y - 2);
		array[19] = new NODE(x + 1, y - 2);*/
		return array;
	}

	public NODE Get_Parent_NODE(int depth)
	{
		if (distance == depth || distance == 0)
			return this;

		return (parent.Get_Parent_NODE(depth));
	}
}
using System.Collections;
using UnityEngine;

public class CA3DGenerator : MonoBehaviour
{
	[Range (1, 25)]
	public int iterations = 10;
	[Range (0f, 1f)]
	public float spawnChance = 0.75f;
	public int minNeighborCount = 4;
	public int maxNeighborCount = 9;
	public float frameDelay = 0.1f;
	[Range (3, 50)]
	public int size = 10;
	public new CA3DRenderer renderer;

	private int[,,] data;

	private void Awake()
	{
		InitializeData ();
		renderer.InitializeRender (data);
		StartCoroutine (AutomataRoutine ());
	}

	public void InitializeData ()
	{
		data = new int[size, size, size];
		for (int x = 1; x < size - 1; x++)
		{
			for (int y = 1; y < size - 1; y++)
			{
				for (int z = 1; z < size - 1; z++)
				{
					data[x, y, z] = (Random.value > spawnChance) ? 1 : 0;
				}
			}
		}
	}
	public int[,,] ProcessData ()
	{
		var newData = data;
		for (int x = 1; x < size - 1; x++)
		{
			for (int y = 1; y < size - 1; y++)
			{
				for (int z = 1; z < size - 1; z++)
				{
					var neighborCount = GetNeighborCount (x, y, z);
					newData[x, y, z] = (neighborCount > minNeighborCount && neighborCount < maxNeighborCount) ? 1 : 0;
				}
			}
		}
		return newData;
	}

	private IEnumerator AutomataRoutine ()
	{
		var currentIteration = 0;
		while (currentIteration < iterations)
		{
			data = ProcessData ();
			renderer.UpdateRender (data);
			currentIteration++;
			yield return new WaitForSeconds (frameDelay);
		}
		renderer.CacheRender ();
	}

	private int GetNeighborCount (int x, int y, int z)
	{
		var count = 0;
		// Adjacent neighbors
		if (data[x - 1, y, z] == 1)
			count++;
		if (data[x + 1, y, z] == 1)
			count++;
		if (data[x, y - 1, z] == 1)
			count++;
		if (data[x, y + 1, z] == 1)
			count++;
		if (data[x, y, z - 1] == 1)
			count++;
		if (data[x, y, z + 1] == 1)
			count++;
		// Corner neighbors
		if (data[x - 1, y + 1, z + 1] == 1)
			count++;
		if (data[x - 1, y + 1, z - 1] == 1)
			count++;
		if (data[x - 1, y - 1, z + 1] == 1)
			count++;
		if (data[x - 1, y - 1, z - 1] == 1)
			count++;
		if (data[x + 1, y + 1, z + 1] == 1)
			count++;
		if (data[x + 1, y + 1, z - 1] == 1)
			count++;
		if (data[x + 1, y - 1, z + 1] == 1)
			count++;
		if (data[x + 1, y - 1, z - 1] == 1)
			count++;
		return count;
	}
}
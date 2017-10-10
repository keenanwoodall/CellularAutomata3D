using System.Collections.Generic;
using UnityEngine;

public class CA3DRenderer : MonoBehaviour
{
	public GameObject prefab;
	private int[,,] data;
	private Transform[,,] instances;

	public void InitializeRender (int[,,] data)
	{
		var sizeX = data.GetLength (0);
		var sizeY = data.GetLength (1);
		var sizeZ = data.GetLength (2);
		instances = new Transform[sizeX, sizeY, sizeZ];
		for (int x = 1; x < sizeX - 1; x++)
		{
			for (int y = 1; y < sizeY - 1; y++)
			{
				for (int z = 1; z < sizeZ - 1; z++)
				{
					instances[x, y, z] = Instantiate (prefab, new Vector3 (x, y, z), Quaternion.identity).transform;
				}
			}
		}
	}
	public void UpdateRender (int[,,] data)
	{
		this.data = data;
		if (data == null)
			return;
		var sizeX = data.GetLength (0);
		var sizeY = data.GetLength (1);
		var sizeZ = data.GetLength (2);
		for (int x = 1; x < sizeX - 1; x++)
		{
			for (int y = 1; y < sizeY - 1; y++)
			{
				for (int z = 1; z < sizeZ - 1; z++)
				{
					instances[x, y, z].localScale = Vector3.one * data[x, y, z];
				}
			}
		}
	}
	public void CacheRender ()
	{
		var sizeX = data.GetLength (0);
		var sizeY = data.GetLength (1);
		var sizeZ = data.GetLength (2);
		for (int x = 1; x < sizeX - 1; x++)
		{
			for (int y = 1; y < sizeY - 1; y++)
			{
				for (int z = 1; z < sizeZ - 1; z++)
				{
					if (instances[x, y, z].localScale.magnitude < 0.5f)
						Destroy (instances[x, y, z].gameObject);
				}
			}
		}
	}
}
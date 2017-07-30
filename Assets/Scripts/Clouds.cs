using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    public Transform rover;
    public GameObject[] cloudPrefabs;
    private const int WIDTH = 20;
    private const int HEIGHT = 20;
    private int[, ] map = new int[WIDTH, HEIGHT];

    void Awake()
    {
        for (int y = 0; y < HEIGHT; y++)
            for (int x = 0; x < WIDTH; x++)
            {
                map[x, y] = Random.Range(0, cloudPrefabs.Length);
            }
    }

    void Start()
    {
        makeClouds();
    }

    public void makeClouds()
    {
        // Which cells are on camera or just off camera?
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 upperRight = Camera.main.ViewportToWorldPoint(Vector3.one);

		List<Transform> allClouds = new List<Transform>();
		for(int i = 0; i < transform.childCount; i++)
			allClouds.Add(transform.GetChild(i));

        for (float y = bottomLeft.y; y < upperRight.y; y += 8)
            for (float x = bottomLeft.x; x < upperRight.x; x += 8)
            {
				int ix = Mathf.FloorToInt(x / 8);
				int iy = Mathf.FloorToInt(y / 8);
				int[] index = normalizeIndex(ix, iy);

                // Verify each cell has a cloud. 
				Transform cloud = transform.Find("cloud_" + index[0] + "_" + index[1]);

				// If it does not, make a cloud.
                if (cloud == null)
				{
					// Debug.Log("Can't find " + index[0] +"," + iy);
                    makeCloud(ix, iy);
				}
				else allClouds.Remove(cloud);
            }

        // Terminate all other clouds
		foreach(Transform cloud in allClouds)
			GameObject.Destroy(cloud.gameObject);

        Invoke("makeClouds", 1f);
    }

    private int[] normalizeIndex(int x, int y)
    {
        int ix = x % WIDTH;
        int iy = y % HEIGHT;
        if (ix < 0) ix += WIDTH;
        if (iy < 0) iy += HEIGHT;
        return new int[] { ix, iy };
    }

    private void makeCloud(int x, int y)
    {
        int[] index = normalizeIndex(x, y);
        GameObject prefab = cloudPrefabs[map[index[0], index[1]]];
        Vector3 pos = new Vector3(x * 8, y * 8, 0);
        GameObject go = (GameObject) Instantiate(prefab, pos, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        go.transform.parent = transform;
    }
}
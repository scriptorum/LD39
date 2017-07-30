using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private const int WIDTH = 20;
    private const int HEIGHT = 20;
    private int counter = 0;
	void Awake()
	{
		updateName();
	}
    void FixedUpdate()
    {
        transform.Translate(-Random.Range(2.5f, 2.6f) * Time.deltaTime, Random.Range(-0.15f, 0.25f) * Time.deltaTime, 0f, Space.World);

        if (counter++ > 20)
            updateName();
    }

    private void updateName()
    {
        int[] index = normalizeIndex(Mathf.FloorToInt(transform.position.x / 8), Mathf.FloorToInt(transform.position.y / 8));
        gameObject.name = "cloud_" + index[0] + "_" + index[1];
        counter = 0;
    }

    private int[] normalizeIndex(int x, int y)
    {
        int ix = x % WIDTH;
        int iy = y % HEIGHT;
        if (ix < 0) ix += WIDTH;
        if (iy < 0) iy += HEIGHT;
        return new int[] { ix, iy };
    }

}
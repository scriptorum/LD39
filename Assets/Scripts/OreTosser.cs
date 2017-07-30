using System.Collections;
using System.Collections.Generic;
using Spewnity;
using UnityEngine;

public class OreTosser : MonoBehaviour
{
    public static OreTosser instance;
    public GameObject contentsPrefab;

    private const float TOSS = 2f;
    private const float TOSS_SPEED = 0.3f;
	private Transform game;

    void Awake()
    {
        instance = this;
		game = transform.Find("/Game");
    }

    public void toss(Vector3 at, float minToss = 0f)
    {
        float signx = Random.Range(-1, 2);
        float signy = signx == 0 ? (Toolkit.CoinFlip() ? 1 : -1) : Random.Range(-1, 2);
        float tossX = (Random.Range(0, TOSS) + minToss) * signx;
        float tossY = (Random.Range(0, TOSS) + minToss) * signy;
        Vector3 pos = new Vector3(at.x + tossX, at.y + tossY, 0f);

        GameObject go = (GameObject) Instantiate(contentsPrefab, at, Quaternion.identity);
        go.transform.parent = game;

        CoroutineManager.instance.Run(go.transform.LerpPosition(pos, TOSS_SPEED, null,
            (t) => SoundManager.instance.Play("ore-drop")));
    }
}
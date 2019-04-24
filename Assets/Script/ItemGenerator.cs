using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour {
    // ゲームオブジェクトを入れる
    public GameObject carPrefab;
    public GameObject coinPrefab;
    public GameObject conePrefab;
    // スタート地点
    private int startPos = -160;
    // ゴール地点
    private int goalPos;
    // アイテムを出すx方向の範囲
    private float posRange = 3.4f;

    private GameObject unitychan;

	// Use this for initialization
	void Start () {
		// 一定範囲ごとにアイテムを設定
        for(int i = startPos; i < goalPos; i += 15)
        {
            // 出現アイテムをランダムに設定
            int num = Random.Range(1, 11);
            if(num <= 2)
            {
                // コーンをx軸方法に一直線に生成
                for(float j = -1; j <= 1; j += 0.4f)
                {
                    GameObject cone = Instantiate(conePrefab) as GameObject;
                    cone.transform.position = new Vector3(4 * j, cone.transform.position.y, i);
                }
            } else
            {
                // レーンごとにアイテムを設定
                for(int j = -1; j <= 1; j++)
                {
                    // アイテムの種類を決める
                    int item = Random.Range(1, 11);
                    // アイテムの置き場所をランダムに設定
                    int offsetZ = Random.Range(-5, 6);

                    if(1 <= item && item <= 6)
                    {
                        // コイン生成
                        GameObject coin = Instantiate(coinPrefab) as GameObject;
                        coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, i + offsetZ);
                    } else if(7 <= item && item <= 9)
                    {
                        // 車を生成
                        GameObject car = Instantiate(carPrefab) as GameObject;
                        car.transform.position = new Vector3(posRange * j, car.transform.position.y, i + offsetZ);
                    }
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
     
    }
}

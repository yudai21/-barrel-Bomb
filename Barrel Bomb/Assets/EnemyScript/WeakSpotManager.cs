using UnityEngine;
using System.Collections.Generic;

public class WeakSpotManager : MonoBehaviour
{
    public List<GameObject> leftWeakSpots;  // 左側の弱点オブジェクトのリスト
    public List<GameObject> rightWeakSpots; // 右側の弱点オブジェクトのリスト
    public float displayTime = 10f;         // 表示時間
    private float timer;

    void Start()
    {
        timer = displayTime;
        Debug.Log("WeakSpotManager has started"); // スタート時にログ出力
        ShowRandomWeakSpots();
    }

    void Update()
    {
        // タイマーの減少
        timer -= Time.deltaTime;
        //Debug.Log("Timer: " + timer);  // タイマーの値をログに表示して確認

        if (timer <= 0f)
        {
            ShowRandomWeakSpots();
            timer = displayTime;  // タイマーをリセット
           //Debug.Log("Timer reset. Showing new weak spots.");
        }
    }

    void ShowRandomWeakSpots()
    {
        // 左と右の弱点をすべて非表示にする
        HideAllWeakSpots(leftWeakSpots);
        HideAllWeakSpots(rightWeakSpots);

        // 左側と右側からそれぞれ2つずつランダムに弱点を表示する
        ActivateRandomWeakSpots(leftWeakSpots, 2);
        ActivateRandomWeakSpots(rightWeakSpots, 2);
        Debug.Log("Random weak spots are shown.");
    }

    // 指定されたリスト内のすべての弱点を非表示にするメソッド
    void HideAllWeakSpots(List<GameObject> weakSpots)
    {
        foreach (GameObject weakSpot in weakSpots)
        {
            weakSpot.SetActive(false);
        }
    }

    // 指定されたリストからランダムに指定数の弱点を表示するメソッド
    void ActivateRandomWeakSpots(List<GameObject> weakSpots, int count)
    {
        List<int> availableIndexes = new List<int>();
        for (int i = 0; i < weakSpots.Count; i++)
        {
            availableIndexes.Add(i);
        }

        // 指定された数だけランダムに弱点を表示
        for (int i = 0; i < count && availableIndexes.Count > 0; i++)
        {
            int randomIndex = availableIndexes[Random.Range(0, availableIndexes.Count)];
            weakSpots[randomIndex].SetActive(true);  // 選ばれた弱点を表示
            availableIndexes.Remove(randomIndex);     // 選んだインデックスをリストから削除
        }
    }
}

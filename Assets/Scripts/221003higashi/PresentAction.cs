using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PresentAction : MonoBehaviour
{
    [SerializeField] GameObject pnlPresentScene;    // プレゼント開封画面にする用のパネル
    [SerializeField] GameObject pnlPresentTime;     // 制限時間用のパネル
    [SerializeField] Text txtPresentTime;           // プレゼントの制限時間


    // 制限時間関連
    [Header("Time Text")]
    [SerializeField] public float presentTimeMax;          // プレゼントの制限時間の最大
    [SerializeField] public float presentTimeNow;          // 今の時間
    public bool isTime;                             // 時間を進めるかどうか
    public float scaleChangeTime, changeSpeed;      // サイズが変わる時間 / そのスピード 
    public bool enlarge;                            // サイズを大きくするか、小さくするかのフラグ
    bool isIndicatePresent;                           // プレゼントを表示するかどうか

    NewhigashiPresent newhigashiPresent;

    void Start()
    {
        // 各オブジェクトの表示の初期化
        pnlPresentScene.SetActive(false);
        pnlPresentTime.SetActive(false);

        // 制限時間の初期化
        presentTimeNow = presentTimeMax;
        isTime = false;
        enlarge = true;
        isIndicatePresent = false;

        newhigashiPresent = FindObjectOfType<NewhigashiPresent>();
    }

    void Update()
    {
        // 現時点では、スペースキーを押すとプレゼントが出現する
        if (Input.GetKey(KeyCode.Space))
        {
            // プレゼント開封の処理に行く
            StartCoroutine(TryPresent());
        }

        // 制限時間の処理
        OpenPresentTimeText();

        // 制限時間を表示するフラグが立ったら
        if (isTime)
        {
            // 制限時間の処理を行う
            TimeStart();
        }


        // プレセントを表示するフラグが立ったら
        if (isIndicatePresent)
        {
            // プレゼント表示処理を行う
            newhigashiPresent.IndicatePresent();
        }

    }

    // PresentTimeの書式
    void OpenPresentTimeText()
    {
        string timeformat = "Time:" + presentTimeNow.ToString("0.00");
        txtPresentTime.text = timeformat;
    }

    // 制限時間の処理
    void TimeStart()
    {
        // 制限時間を減らす
        presentTimeNow -= Time.deltaTime;
        // 一定数制限時間が迫ったら
        // 制限時間のUIを大小させたり、色を変えたりする
        if (presentTimeNow <= 3.0f)
        {
            // UIの色の変数
            float colorR = 1.0f;
            float colorG = 1.0f;
            float colorB = 1.0f;

            // 変更のスパン
            changeSpeed = Time.deltaTime * 0.7f;
            if (scaleChangeTime < 0)
            {
                enlarge = true;
            }
            if (scaleChangeTime > 0.6f)
            {
                enlarge = false;
            }

            if (enlarge == true)
            {
                scaleChangeTime += Time.deltaTime;
                txtPresentTime.transform.localScale += new Vector3(changeSpeed, changeSpeed, changeSpeed);
                colorG--;
                colorB--;
            }
            else
            {
                scaleChangeTime -= Time.deltaTime;
                txtPresentTime.transform.localScale -= new Vector3(changeSpeed, changeSpeed, changeSpeed);
                colorG++;
                colorB++;
            }
            // UIの色の表示
            txtPresentTime.color = new Color(colorR, colorG, colorB);

            // 制限時間が0秒になったら
            if (presentTimeNow <= 0.0f)
            {
                // --------------------------
                // 開封時間終了(ゲームに戻る)
                // --------------------------
                presentTimeNow = 0.0f;
                //StartCoroutine(NotGetPresent());

                // 色も大きさも戻す
                txtPresentTime.transform.localScale = new Vector3(1, 1, 1);
                txtPresentTime.color = new Color(1.0f, 1.0f, 1.0f);
            }
        }
    }

    // プレゼント開封の処理
    IEnumerator TryPresent()
    {
        // プレゼント画面の表示
        pnlPresentScene.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        // 各UIの表示
        pnlPresentTime.SetActive(true);
        newhigashiPresent.imgPresent.SetActive(true);

        presentTimeNow = presentTimeMax;       // 制限時間をMAXにする


        isIndicatePresent = true;                     // プレゼントを出現させる
        yield return new WaitForSeconds(2.0f);
        // 制限時間のスタート
        isTime = true;
    }
}

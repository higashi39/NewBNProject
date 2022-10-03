using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class NewhigashiPresent : MonoBehaviour
{
    [Header("References")]
    NewhigashiPresentManager newhigashiPresentManager;
    PresentAction presentAction;

    // クリックした時に箱を揺らす用変数
    public float shakeDuration;
    public float strength;
    public int vibrato;
    public float randomness;
    public bool snapping;
    public bool fadeOut;

    // 紙吹雪用パーティクルシステム
    public ParticleSystem[] particles;

    [SerializeField] GameObject pnlPresentScene;    // プレゼント開封画面にする用のパネル
    [SerializeField] public GameObject imgPresent;         // プレゼントのイラスト
    [SerializeField] GameObject pnlPresentTime;     // 制限時間用のパネル
    [SerializeField] Text txtPresentTime;           // プレゼントの制限時間

    //[SerializeField] GameObject imgPresentDecoration;      // デコレーション(リストにする)


    // プレゼント関連
    [Header("Present")]
    public float frameRotate;                       // プレゼントが回転する値
    public float frameSize;                         // プレゼントのサイズを変更する値
    bool isRotate = true;                           // プレゼントが回転するかどうか

    // 各プレゼントの処理(のちに他のスクリプトにまとめる)
    [Header("each PresentAction")]
    int clickCount;                                 // クリックした数

    //// 制限時間関連
    //[Header("Time Text")]
    //[SerializeField] float presentTimeMax;          // プレゼントの制限時間の最大
    //[SerializeField] float presentTimeNow;          // 今の時間
    //public bool isTime;                             // 時間を進めるかどうか
    //public float scaleChangeTime, changeSpeed;      // サイズが変わる時間 / そのスピード 
    //public bool enlarge;                            // サイズを大きくするか、小さくするかのフラグ
    //bool isIndicatePresent;                           // プレゼントを表示するかどうか

    bool isClicked;

    // Start is called before the first frame update
    void Start()
    {
        newhigashiPresentManager = FindObjectOfType<NewhigashiPresentManager>();
        presentAction = FindObjectOfType<PresentAction>();

        isClicked = false;

        //// 各オブジェクトの表示の初期化
        //pnlPresentScene.SetActive(false);
        //imgPresent.SetActive(false);
        //pnlPresentTime.SetActive(false);

        //// 制限時間の初期化
        //presentTimeNow = presentTimeMax;
        //isTime = false;
        //enlarge = true;
        //isIndicatePresent = false;

    }

    // Update is called once per frame
    void Update()
    {
        //// 制限時間の処理
        //OpenPresentTimeText();

        //// プレセントを表示するフラグが立ったら
        //if (isIndicatePresent)
        //{
        //    // プレゼント表示処理を行う
        //    IndicatePresent();
        //}

        //// 制限時間を表示するフラグが立ったら
        //if (isTime)
        //{
        //    // 制限時間の処理を行う
        //    TimeStart();
        //}

        if (presentAction.presentTimeNow > 0.0f && presentAction.presentTimeNow <= 5.0f)
        {
            if (Input.GetMouseButtonDown(0))
            {
                imgPresent.transform.DOShakePosition(shakeDuration, strength, vibrato, randomness, snapping, fadeOut);
                ++clickCount;
            }

            if (clickCount > 15)
            {
                // ----------------
                // デコレーションゲット
                // ----------------
                isClicked = true;
            }
            else
            {
                // ----------------------------
                // デコレーションをゲットできなかった
                // ----------------------------
                //StartCoroutine(NotGetPresent());
            }
        }

        if(isClicked)
        {
            GetPresent();
            newhigashiPresentManager.DecorListRandom();
        }
        else
        {

        }

        if(Input.GetKey(KeyCode.O))
        {
        }
    }

    //// PresentTimeの書式
    //void OpenPresentTimeText()
    //{
    //    string timeformat = "Time:" + presentTimeNow.ToString("0.00");
    //    txtPresentTime.text = timeformat;
    //}

    //// プレゼント開封の処理
    //IEnumerator TryPresent()
    //{
    //    // プレゼント画面の表示
    //    pnlPresentScene.SetActive(true);
    //    yield return new WaitForSeconds(1.0f);
    //    // 各UIの表示
    //    pnlPresentTime.SetActive(true);
    //    imgPresent.SetActive(true);
    //    presentTimeNow = presentTimeMax;       // 制限時間をMAXにする

    //    isIndicatePresent = true;                     // プレゼントを出現させる
    //    yield return new WaitForSeconds(2.0f);
    //    // 制限時間のスタート
    //    isTime = true;
    //}

    // プレゼント表示の処理
    public void IndicatePresent()
    {
        // プレゼントオブジェクトのサイズを変更する
        frameSize += 0.01f;
        // 回転もさせる(最初はtrue)
        imgPresent.transform.localScale = new Vector3(frameSize, frameSize, frameSize);
        if (!isRotate)
        {
            imgPresent.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            imgPresent.transform.rotation *= Quaternion.AngleAxis(frameRotate, Vector3.back);
        }

        // サイズが最大まで行ったら
        if (frameSize > 1.0f)
        {
            frameSize = 1.0f;

            // 回転を止めるためにisRotateをfalseにする
            frameRotate -= 0.1f;
            if (frameRotate <= 0.0f)
            {
                frameRotate = 0.0f;
                isRotate = false;
            }
        }
    }

    //// 制限時間の処理
    //void TimeStart()
    //{
    //    Debug.Log(isRotate);

    //    // 制限時間を減らす
    //    presentTimeNow -= Time.deltaTime;
    //    // 一定数制限時間が迫ったら
    //    // 制限時間のUIを大小させたり、色を変えたりする
    //    if (presentTimeNow <= 3.0f)
    //    {
    //        // UIの色の変数
    //        float colorR = 1.0f;
    //        float colorG = 1.0f;
    //        float colorB = 1.0f;

    //        // 変更のスパン
    //        changeSpeed = Time.deltaTime * 0.7f;
    //        if (scaleChangeTime < 0)
    //        {
    //            enlarge = true;
    //        }
    //        if (scaleChangeTime > 0.6f)
    //        {
    //            enlarge = false;
    //        }

    //        if (enlarge == true)
    //        {
    //            scaleChangeTime += Time.deltaTime;
    //            txtPresentTime.transform.localScale += new Vector3(changeSpeed, changeSpeed, changeSpeed);
    //            colorG--;
    //            colorB--;
    //        }
    //        else
    //        {
    //            scaleChangeTime -= Time.deltaTime;
    //            txtPresentTime.transform.localScale -= new Vector3(changeSpeed, changeSpeed, changeSpeed);
    //            colorG++;
    //            colorB++;
    //        }
    //        // UIの色の表示
    //        txtPresentTime.color = new Color(colorR, colorG, colorB);

    //        // 制限時間が0秒になったら
    //        if (presentTimeNow <= 0.0f)
    //        {
    //            // --------------------------
    //            // 開封時間終了(ゲームに戻る)
    //            // --------------------------
    //            presentTimeNow = 0.0f;
    //            //StartCoroutine(NotGetPresent());

    //            // 色も大きさも戻す
    //            txtPresentTime.transform.localScale = new Vector3(1, 1, 1);
    //            txtPresentTime.color = new Color(1.0f, 1.0f, 1.0f);
    //        }
    //    }
    //}


    // プレゼントをゲットしたときの処理
    void GetPresent()
    {
        isClicked = false;

        presentAction.isTime = false;                 // 制限時間を止める
        newhigashiPresentManager.DecorListRandom();
        newhigashiPresentManager.randObj.SetActive(false);    // プレゼントの表示はしない
        RectTransform imgPresentDecorTransform = newhigashiPresentManager.randObj.GetComponent<RectTransform>();
        imgPresentDecorTransform.DOAnchorPosY(0.0f, 0.2f).SetEase(Ease.OutBounce);
        //ConfettiAction();
    }

    // デコレーションをゲットできなかったときの処理
    IEnumerator NotGetPresent()
    {
        RectTransform notGetPresent = imgPresent.GetComponent<RectTransform>();
        float oriPosY = imgPresent.transform.position.y;

        Debug.Log("プレゼントを開くことができなかった！");
        imgPresent.transform.DOShakeScale(2.0f, 0.15f, 4, 20);
        yield return new WaitForSeconds(2.0f);
        notGetPresent.DOAnchorPosY(oriPosY, 1.25f).SetEase(Ease.InBack);
        yield return new WaitForSeconds(1.25f);
    }

}

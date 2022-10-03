using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NewhigashiPresentManager : MonoBehaviour
{
    // プレゼントの箱のリスト
    [Header("PresentBox")]
    List<GameObject> presentList;
    int[] randomNumArr;

    // デコレーションのリスト
    [Header("in PresentBox")]
    public GameObject[] decorPrefabs;
    [SerializeField] List<GameObject> decorList = new List<GameObject>();
    [HideInInspector] public GameObject randObj;        // ランダム用
    private int rand;
    //[SerializeField] public Vector3 newDecorPos = new Vector3();       // デコレーションの座標


    [Header("References Script")]
    GameManager gameManager;

    //[Header("Present Box Info")]
    //public Text txtPresentInfo;



    // Start is called before the first frame update
    void Start()
    {
        //txtPresentInfo.gameObject.SetActive(false);

        // プレゼントの中身に入れる飾りのプレハブをリストに追加
        foreach (GameObject item in decorPrefabs)
        {
            decorList.Add(item);
        }
    }

    //　PresentBoxManagerをInitする
    //  randomNumArrの準備とpresentListをnewする
    //  プレゼント数UIも更新する
    public void Init(int presentTotal)
    {
        presentList = new List<GameObject>();

        randomNumArr = new int[presentTotal];
        for (int i = 0; i < presentTotal; ++i)
        {
            randomNumArr[i] = i;
        }

        gameManager = FindObjectOfType<GameManager>();
        gameManager.UpdateUIPresentLeft(presentTotal);
    }

    //　残りのプレゼント数
    public int GetAvailablePresent()
    {
        return presentList.Count;
    }

    //　presentListにPresentBoxの参照を追加する
    public void AddPresentListRef(GameObject present)
    {
        presentList.Add(present);
    }

    public void ShowUIBoxInfo()
    {
        StartCoroutine(UIBoxInfoAnimation());
    }

    IEnumerator UIBoxInfoAnimation()
    {
        //txtPresentInfo.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        //txtPresentInfo.gameObject.SetActive(false);
    }

    // 出てくるデコレーションをランダムで選出
    public void DecorListRandom()
    {
        rand = Random.Range(0, decorList.Count);

        randObj = decorList[rand];
        Debug.Log(randObj);

    }

    ////出たデコレーションは削除する(一旦コメントアウト)
    //public void RemoveRandObj()
    //{
    //    decorList.RemoveAt(rand);
    //}


    // Update is called once per frame
    void Update()
    {

    }
}

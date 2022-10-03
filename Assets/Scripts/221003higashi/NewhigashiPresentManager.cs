using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NewhigashiPresentManager : MonoBehaviour
{
    // �v���[���g�̔��̃��X�g
    [Header("PresentBox")]
    List<GameObject> presentList;
    int[] randomNumArr;

    // �f�R���[�V�����̃��X�g
    [Header("in PresentBox")]
    public GameObject[] decorPrefabs;
    [SerializeField] List<GameObject> decorList = new List<GameObject>();
    [HideInInspector] public GameObject randObj;        // �����_���p
    private int rand;
    //[SerializeField] public Vector3 newDecorPos = new Vector3();       // �f�R���[�V�����̍��W


    [Header("References Script")]
    GameManager gameManager;

    //[Header("Present Box Info")]
    //public Text txtPresentInfo;



    // Start is called before the first frame update
    void Start()
    {
        //txtPresentInfo.gameObject.SetActive(false);

        // �v���[���g�̒��g�ɓ�������̃v���n�u�����X�g�ɒǉ�
        foreach (GameObject item in decorPrefabs)
        {
            decorList.Add(item);
        }
    }

    //�@PresentBoxManager��Init����
    //  randomNumArr�̏�����presentList��new����
    //  �v���[���g��UI���X�V����
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

    //�@�c��̃v���[���g��
    public int GetAvailablePresent()
    {
        return presentList.Count;
    }

    //�@presentList��PresentBox�̎Q�Ƃ�ǉ�����
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

    // �o�Ă���f�R���[�V�����������_���őI�o
    public void DecorListRandom()
    {
        rand = Random.Range(0, decorList.Count);

        randObj = decorList[rand];
        Debug.Log(randObj);

    }

    ////�o���f�R���[�V�����͍폜����(��U�R�����g�A�E�g)
    //public void RemoveRandObj()
    //{
    //    decorList.RemoveAt(rand);
    //}


    // Update is called once per frame
    void Update()
    {

    }
}

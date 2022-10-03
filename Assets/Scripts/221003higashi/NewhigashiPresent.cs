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

    // �N���b�N�������ɔ���h�炷�p�ϐ�
    public float shakeDuration;
    public float strength;
    public int vibrato;
    public float randomness;
    public bool snapping;
    public bool fadeOut;

    // ������p�p�[�e�B�N���V�X�e��
    public ParticleSystem[] particles;

    [SerializeField] GameObject pnlPresentScene;    // �v���[���g�J����ʂɂ���p�̃p�l��
    [SerializeField] public GameObject imgPresent;         // �v���[���g�̃C���X�g
    [SerializeField] GameObject pnlPresentTime;     // �������ԗp�̃p�l��
    [SerializeField] Text txtPresentTime;           // �v���[���g�̐�������

    //[SerializeField] GameObject imgPresentDecoration;      // �f�R���[�V����(���X�g�ɂ���)


    // �v���[���g�֘A
    [Header("Present")]
    public float frameRotate;                       // �v���[���g����]����l
    public float frameSize;                         // �v���[���g�̃T�C�Y��ύX����l
    bool isRotate = true;                           // �v���[���g����]���邩�ǂ���

    // �e�v���[���g�̏���(�̂��ɑ��̃X�N���v�g�ɂ܂Ƃ߂�)
    [Header("each PresentAction")]
    int clickCount;                                 // �N���b�N������

    //// �������Ԋ֘A
    //[Header("Time Text")]
    //[SerializeField] float presentTimeMax;          // �v���[���g�̐������Ԃ̍ő�
    //[SerializeField] float presentTimeNow;          // ���̎���
    //public bool isTime;                             // ���Ԃ�i�߂邩�ǂ���
    //public float scaleChangeTime, changeSpeed;      // �T�C�Y���ς�鎞�� / ���̃X�s�[�h 
    //public bool enlarge;                            // �T�C�Y��傫�����邩�A���������邩�̃t���O
    //bool isIndicatePresent;                           // �v���[���g��\�����邩�ǂ���

    bool isClicked;

    // Start is called before the first frame update
    void Start()
    {
        newhigashiPresentManager = FindObjectOfType<NewhigashiPresentManager>();
        presentAction = FindObjectOfType<PresentAction>();

        isClicked = false;

        //// �e�I�u�W�F�N�g�̕\���̏�����
        //pnlPresentScene.SetActive(false);
        //imgPresent.SetActive(false);
        //pnlPresentTime.SetActive(false);

        //// �������Ԃ̏�����
        //presentTimeNow = presentTimeMax;
        //isTime = false;
        //enlarge = true;
        //isIndicatePresent = false;

    }

    // Update is called once per frame
    void Update()
    {
        //// �������Ԃ̏���
        //OpenPresentTimeText();

        //// �v���Z���g��\������t���O����������
        //if (isIndicatePresent)
        //{
        //    // �v���[���g�\���������s��
        //    IndicatePresent();
        //}

        //// �������Ԃ�\������t���O����������
        //if (isTime)
        //{
        //    // �������Ԃ̏������s��
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
                // �f�R���[�V�����Q�b�g
                // ----------------
                isClicked = true;
            }
            else
            {
                // ----------------------------
                // �f�R���[�V�������Q�b�g�ł��Ȃ�����
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

    //// PresentTime�̏���
    //void OpenPresentTimeText()
    //{
    //    string timeformat = "Time:" + presentTimeNow.ToString("0.00");
    //    txtPresentTime.text = timeformat;
    //}

    //// �v���[���g�J���̏���
    //IEnumerator TryPresent()
    //{
    //    // �v���[���g��ʂ̕\��
    //    pnlPresentScene.SetActive(true);
    //    yield return new WaitForSeconds(1.0f);
    //    // �eUI�̕\��
    //    pnlPresentTime.SetActive(true);
    //    imgPresent.SetActive(true);
    //    presentTimeNow = presentTimeMax;       // �������Ԃ�MAX�ɂ���

    //    isIndicatePresent = true;                     // �v���[���g���o��������
    //    yield return new WaitForSeconds(2.0f);
    //    // �������Ԃ̃X�^�[�g
    //    isTime = true;
    //}

    // �v���[���g�\���̏���
    public void IndicatePresent()
    {
        // �v���[���g�I�u�W�F�N�g�̃T�C�Y��ύX����
        frameSize += 0.01f;
        // ��]��������(�ŏ���true)
        imgPresent.transform.localScale = new Vector3(frameSize, frameSize, frameSize);
        if (!isRotate)
        {
            imgPresent.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            imgPresent.transform.rotation *= Quaternion.AngleAxis(frameRotate, Vector3.back);
        }

        // �T�C�Y���ő�܂ōs������
        if (frameSize > 1.0f)
        {
            frameSize = 1.0f;

            // ��]���~�߂邽�߂�isRotate��false�ɂ���
            frameRotate -= 0.1f;
            if (frameRotate <= 0.0f)
            {
                frameRotate = 0.0f;
                isRotate = false;
            }
        }
    }

    //// �������Ԃ̏���
    //void TimeStart()
    //{
    //    Debug.Log(isRotate);

    //    // �������Ԃ����炷
    //    presentTimeNow -= Time.deltaTime;
    //    // ��萔�������Ԃ���������
    //    // �������Ԃ�UI��召��������A�F��ς����肷��
    //    if (presentTimeNow <= 3.0f)
    //    {
    //        // UI�̐F�̕ϐ�
    //        float colorR = 1.0f;
    //        float colorG = 1.0f;
    //        float colorB = 1.0f;

    //        // �ύX�̃X�p��
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
    //        // UI�̐F�̕\��
    //        txtPresentTime.color = new Color(colorR, colorG, colorB);

    //        // �������Ԃ�0�b�ɂȂ�����
    //        if (presentTimeNow <= 0.0f)
    //        {
    //            // --------------------------
    //            // �J�����ԏI��(�Q�[���ɖ߂�)
    //            // --------------------------
    //            presentTimeNow = 0.0f;
    //            //StartCoroutine(NotGetPresent());

    //            // �F���傫�����߂�
    //            txtPresentTime.transform.localScale = new Vector3(1, 1, 1);
    //            txtPresentTime.color = new Color(1.0f, 1.0f, 1.0f);
    //        }
    //    }
    //}


    // �v���[���g���Q�b�g�����Ƃ��̏���
    void GetPresent()
    {
        isClicked = false;

        presentAction.isTime = false;                 // �������Ԃ��~�߂�
        newhigashiPresentManager.DecorListRandom();
        newhigashiPresentManager.randObj.SetActive(false);    // �v���[���g�̕\���͂��Ȃ�
        RectTransform imgPresentDecorTransform = newhigashiPresentManager.randObj.GetComponent<RectTransform>();
        imgPresentDecorTransform.DOAnchorPosY(0.0f, 0.2f).SetEase(Ease.OutBounce);
        //ConfettiAction();
    }

    // �f�R���[�V�������Q�b�g�ł��Ȃ������Ƃ��̏���
    IEnumerator NotGetPresent()
    {
        RectTransform notGetPresent = imgPresent.GetComponent<RectTransform>();
        float oriPosY = imgPresent.transform.position.y;

        Debug.Log("�v���[���g���J�����Ƃ��ł��Ȃ������I");
        imgPresent.transform.DOShakeScale(2.0f, 0.15f, 4, 20);
        yield return new WaitForSeconds(2.0f);
        notGetPresent.DOAnchorPosY(oriPosY, 1.25f).SetEase(Ease.InBack);
        yield return new WaitForSeconds(1.25f);
    }

}

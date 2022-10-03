using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PresentAction : MonoBehaviour
{
    [SerializeField] GameObject pnlPresentScene;    // �v���[���g�J����ʂɂ���p�̃p�l��
    [SerializeField] GameObject pnlPresentTime;     // �������ԗp�̃p�l��
    [SerializeField] Text txtPresentTime;           // �v���[���g�̐�������


    // �������Ԋ֘A
    [Header("Time Text")]
    [SerializeField] public float presentTimeMax;          // �v���[���g�̐������Ԃ̍ő�
    [SerializeField] public float presentTimeNow;          // ���̎���
    public bool isTime;                             // ���Ԃ�i�߂邩�ǂ���
    public float scaleChangeTime, changeSpeed;      // �T�C�Y���ς�鎞�� / ���̃X�s�[�h 
    public bool enlarge;                            // �T�C�Y��傫�����邩�A���������邩�̃t���O
    bool isIndicatePresent;                           // �v���[���g��\�����邩�ǂ���

    NewhigashiPresent newhigashiPresent;

    void Start()
    {
        // �e�I�u�W�F�N�g�̕\���̏�����
        pnlPresentScene.SetActive(false);
        pnlPresentTime.SetActive(false);

        // �������Ԃ̏�����
        presentTimeNow = presentTimeMax;
        isTime = false;
        enlarge = true;
        isIndicatePresent = false;

        newhigashiPresent = FindObjectOfType<NewhigashiPresent>();
    }

    void Update()
    {
        // �����_�ł́A�X�y�[�X�L�[�������ƃv���[���g���o������
        if (Input.GetKey(KeyCode.Space))
        {
            // �v���[���g�J���̏����ɍs��
            StartCoroutine(TryPresent());
        }

        // �������Ԃ̏���
        OpenPresentTimeText();

        // �������Ԃ�\������t���O����������
        if (isTime)
        {
            // �������Ԃ̏������s��
            TimeStart();
        }


        // �v���Z���g��\������t���O����������
        if (isIndicatePresent)
        {
            // �v���[���g�\���������s��
            newhigashiPresent.IndicatePresent();
        }

    }

    // PresentTime�̏���
    void OpenPresentTimeText()
    {
        string timeformat = "Time:" + presentTimeNow.ToString("0.00");
        txtPresentTime.text = timeformat;
    }

    // �������Ԃ̏���
    void TimeStart()
    {
        // �������Ԃ����炷
        presentTimeNow -= Time.deltaTime;
        // ��萔�������Ԃ���������
        // �������Ԃ�UI��召��������A�F��ς����肷��
        if (presentTimeNow <= 3.0f)
        {
            // UI�̐F�̕ϐ�
            float colorR = 1.0f;
            float colorG = 1.0f;
            float colorB = 1.0f;

            // �ύX�̃X�p��
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
            // UI�̐F�̕\��
            txtPresentTime.color = new Color(colorR, colorG, colorB);

            // �������Ԃ�0�b�ɂȂ�����
            if (presentTimeNow <= 0.0f)
            {
                // --------------------------
                // �J�����ԏI��(�Q�[���ɖ߂�)
                // --------------------------
                presentTimeNow = 0.0f;
                //StartCoroutine(NotGetPresent());

                // �F���傫�����߂�
                txtPresentTime.transform.localScale = new Vector3(1, 1, 1);
                txtPresentTime.color = new Color(1.0f, 1.0f, 1.0f);
            }
        }
    }

    // �v���[���g�J���̏���
    IEnumerator TryPresent()
    {
        // �v���[���g��ʂ̕\��
        pnlPresentScene.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        // �eUI�̕\��
        pnlPresentTime.SetActive(true);
        newhigashiPresent.imgPresent.SetActive(true);

        presentTimeNow = presentTimeMax;       // �������Ԃ�MAX�ɂ���


        isIndicatePresent = true;                     // �v���[���g���o��������
        yield return new WaitForSeconds(2.0f);
        // �������Ԃ̃X�^�[�g
        isTime = true;
    }
}

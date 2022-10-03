using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GAME_STATUS
    {
        GAME_START, //�����A�j���[�V�����i321�����Ȃ�ŏ���GAME_START�j
        GAME_PAUSED,
        GAME_NORMAL,
        GAME_PRESENT_APPEAR,
        GAME_PRESENT_OPEN,
        GAME_ENDED,
    }

    [field: SerializeField] public GAME_STATUS GameStatus { private set; get; }

    [Header("Canvas / UI")]
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject pnlPause;
    [SerializeField] Text txtTime;
    //[SerializeField] Text txtPresentLeft;
    [SerializeField] Text txtPause;
    [SerializeField] GameObject pnlStart;
    [SerializeField] Text txtStart;
    [SerializeField] GameObject pnlEnd;
    [SerializeField] Text txtEndTitle;
    //[SerializeField] Text txtEndScore;
    [SerializeField] Button btnEndMainMenu;
    [SerializeField] Button btnEndRestart;


    [Header("Game Settings")]
    [SerializeField] float gameTimeMax; //1����Q�[���̎���
    [SerializeField] float gameTime;    //���̎���

    [Header("Player References")]
    PlayerMain playerMain;
    Vector3 playerFirstPos;

    PresentBoxManager presentBoxManager;

    // Start is called before the first frame update
    void Start()
    {
        //  �|�[�Y�p�l����false�ɂ���
        pnlPause.SetActive(false);
        //  
        pnlStart.SetActive(false);
        pnlEnd.SetActive(false);
        txtEndTitle.gameObject.SetActive(false);
        //txtEndScore.gameObject.SetActive(false);
        btnEndMainMenu.gameObject.SetActive(false);
        btnEndRestart.gameObject.SetActive(false);
        //  �Q�[�����Ԑݒ�
        gameTime = gameTimeMax;
        //  �v���C���[�Q��
        playerMain = FindObjectOfType<PlayerMain>();
        playerFirstPos = playerMain.transform.position;

        StartCoroutine(StartGameAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameStatus)
        {
            case GAME_STATUS.GAME_NORMAL:
                {
                    gameTime -= Time.deltaTime;
                    if (gameTime <= 0.0f)
                    {
                        //--------------------
                        //�Q�[���I��
                        //--------------------
                        gameTime = 0.0f;
                        ChangeGameStatus(GAME_STATUS.GAME_ENDED);
                        playerMain.SetPlayerInputEnable(false);
                        StartCoroutine(EndGameAnimation());
                        playerMain.transform.position = playerFirstPos;
                    }
                    FormatTimeText();
                }
                break;
        }

    }

    //  �Q�[���X�e�[�^�X��ύX����
    public void ChangeGameStatus(GAME_STATUS status)
    {
        GameStatus = status;
    }

    public void SetGameStatusPresentAppear()
    {
        GameStatus = GAME_STATUS.GAME_PRESENT_APPEAR;
        ChangeGameStatus(GameManager.GAME_STATUS.GAME_PRESENT_APPEAR);
        playerMain.SetPlayerInputEnable(false);
        SetEnableAllUI(false);
    }


    #region UI
    //�@�SActiveUI�������邩�ǂ���
    public void SetEnableAllUI(bool isEnable = true)
    {
        canvas.SetActive(isEnable);
    }
    //  gameTime�̏���
    void FormatTimeText()
    {
        // .ToString("00") -> 00 80 60
        string timeFormat = gameTime.ToString("00");
        txtTime.text = timeFormat;
    }
    //  �c��v���[���g����UI���X�V����
    public void UpdateUIPresentLeft(int presentLeft)
    {
        //txtPresentLeft.text = "�c��v���[���g���F" + presentLeft;
    }
    //  �|�[�Y�{�^�������֐�

    IEnumerator StartGameAnimation()
    {
        FormatTimeText();
        playerMain.SetPlayerInputEnable(false);
        GameStatus = GAME_STATUS.GAME_START;
        pnlStart.SetActive(true);
        txtStart.text = "3";
        yield return new WaitForSeconds(1.0f);
        txtStart.text = "2";
        yield return new WaitForSeconds(1.0f);
        txtStart.text = "1";
        yield return new WaitForSeconds(1.0f);
        txtStart.text = "GO";
        yield return new WaitForSeconds(1.0f);
        pnlStart.SetActive(false);
        playerMain.SetPlayerInputEnable();
        GameStatus = GAME_STATUS.GAME_NORMAL;
    }

    IEnumerator EndGameAnimation()
    {
        pnlEnd.SetActive(true);
        txtEndTitle.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        //txtEndScore.text = "�X�R�A�F" + playerMain.GetPlayerScore().ToString();
        //txtEndScore.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        btnEndMainMenu.gameObject.SetActive(true);
        btnEndRestart.gameObject.SetActive(true);
    }


    #region Button Function
    public void PressBtnPause()
    {
        //�|�[�Y����
        if (GameStatus == GAME_STATUS.GAME_NORMAL)
        {
            Time.timeScale = 0;
            pnlPause.SetActive(true);
            ChangeGameStatus(GAME_STATUS.GAME_PAUSED);
            txtPause.text = "Resume";
            return;
        }

        //�Q�[������
        if (GameStatus == GAME_STATUS.GAME_PAUSED)
        {
            ChangeGameStatus(GAME_STATUS.GAME_NORMAL);
            pnlPause.SetActive(false);
            Time.timeScale = 1;
            txtPause.text = "Pause";
            return;
        }
    }

    public void PressBtnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PressBtnRestart()
    {
        SceneManager.LoadScene("PlayScene");
    }
    #endregion

    #endregion

}

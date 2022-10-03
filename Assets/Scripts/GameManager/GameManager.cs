using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GAME_STATUS
    {
        GAME_START, //もしアニメーション（321がやるなら最初にGAME_START）
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
    [SerializeField] float gameTimeMax; //1っ回ゲームの時間
    [SerializeField] float gameTime;    //今の時間

    [Header("Player References")]
    PlayerMain playerMain;
    Vector3 playerFirstPos;

    PresentBoxManager presentBoxManager;

    // Start is called before the first frame update
    void Start()
    {
        //  ポーズパネルはfalseにする
        pnlPause.SetActive(false);
        //  
        pnlStart.SetActive(false);
        pnlEnd.SetActive(false);
        txtEndTitle.gameObject.SetActive(false);
        //txtEndScore.gameObject.SetActive(false);
        btnEndMainMenu.gameObject.SetActive(false);
        btnEndRestart.gameObject.SetActive(false);
        //  ゲーム時間設定
        gameTime = gameTimeMax;
        //  プレイヤー参照
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
                        //ゲーム終了
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

    //  ゲームステータスを変更する
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
    //　全ActiveUIを見せるかどうか
    public void SetEnableAllUI(bool isEnable = true)
    {
        canvas.SetActive(isEnable);
    }
    //  gameTimeの書式
    void FormatTimeText()
    {
        // .ToString("00") -> 00 80 60
        string timeFormat = gameTime.ToString("00");
        txtTime.text = timeFormat;
    }
    //  残りプレゼント数のUIを更新する
    public void UpdateUIPresentLeft(int presentLeft)
    {
        //txtPresentLeft.text = "残りプレゼント数：" + presentLeft;
    }
    //  ポーズボタン押す関数

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
        //txtEndScore.text = "スコア：" + playerMain.GetPlayerScore().ToString();
        //txtEndScore.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        btnEndMainMenu.gameObject.SetActive(true);
        btnEndRestart.gameObject.SetActive(true);
    }


    #region Button Function
    public void PressBtnPause()
    {
        //ポーズする
        if (GameStatus == GAME_STATUS.GAME_NORMAL)
        {
            Time.timeScale = 0;
            pnlPause.SetActive(true);
            ChangeGameStatus(GAME_STATUS.GAME_PAUSED);
            txtPause.text = "Resume";
            return;
        }

        //ゲーム続き
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

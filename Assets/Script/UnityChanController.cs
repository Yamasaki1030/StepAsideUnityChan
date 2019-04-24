using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour {

    private Animator myAnimator;
    private Rigidbody myRigidbody;
    private float forwardForce = 800.0f;

    // 前進・左右移動・ジャンプにかかる力
    private float turnForce = 500.0f;
    private float upForce = 500.0f;
    private float movableRange = 3.4f;

    // 動きを減速させる係数
    private float coefficient = 0.95f;
    // ゲーム終了時の判定
    private bool isEnd = false;
    // テキスト
    private GameObject stateText;
    private GameObject scoreText;
    // 得点
    private int score = 0;
    // ボタン押下の判定
    private bool isLButtonDown = false;
    private bool isRButtonDown = false;

	// Use this for initialization
	void Start () {
        // Animatorコンポーネントを取得
        this.myAnimator = GetComponent<Animator>();
        // 走るアニメーションを開始
        this.myAnimator.SetFloat("Speed", 1);

        this.myRigidbody = GetComponent<Rigidbody>();
        // シーン中のTextオブジェクトを取得
        this.stateText = GameObject.Find("GameResultText");
        this.scoreText = GameObject.Find("ScoreText");
	}
	
	// Update is called once per frame
	void Update () {
        // ゲーム終了時にUnityちゃんを減速させる
        if (this.isEnd)
        {
            this.forwardForce *= this.coefficient;
            this.turnForce *= this.coefficient;
            this.upForce *= this.coefficient;
            this.myAnimator.speed *= this.coefficient;
        }
        // 前方に移動
        this.myRigidbody.AddForce(this.transform.forward * this.forwardForce);

        // 左右に移動する
        if((Input.GetKey(KeyCode.LeftArrow) || this.isLButtonDown) && -this.movableRange  < this.transform.position.x)
        {
            this.myRigidbody.AddForce(-this.turnForce, 0, 0);
        } else if((Input.GetKey(KeyCode.RightArrow) || this.isRButtonDown) && this.transform.position.x < this.movableRange)
        {
            this.myRigidbody.AddForce(this.turnForce, 0, 0);
        }
        // Jumpステートの場合はJumpにfalseをセットする
        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }

        // ジャンプする
        if(Input.GetKeyDown(KeyCode.Space) && this.transform.position.y < 0.5f)
        {
            this.myAnimator.SetBool("Jump", true);
            this.myRigidbody.AddForce(this.transform.up * this.upForce);
        }
	}

    // 他のオブジェクトと接触したときの処理
    void OnTriggerEnter(Collider other)
    {
        // 障害物に追突
        if(other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag")
        {
            this.isEnd = true;
            this.stateText.GetComponent<Text>().text = "GAME OVER";
        }

        // ゴール地点に到達
        if(other.gameObject.tag == "GoalTag")
        {
            this.isEnd = true;
            this.stateText.GetComponent<Text>().text = "CLEAR!!";
        }

        // コイン取得
        if (other.gameObject.tag == "CoinTag")
        {
            // スコアを加算
            this.score += 10;
            this.scoreText.GetComponent<Text>().text = "Score " + this.score + "pt";
            // パーティクルを再生
            GetComponent<ParticleSystem>().Play();

            Destroy(other.gameObject);
        }
    }

    // ジャンプボタンを押した場合の処理
    public void GetMyJumpButtonDown()
    {
        if(this.transform.position.y < 0.5f)
        {
            this.myAnimator.SetBool("Jump", true);
            this.myRigidbody.AddForce(this.transform.up * this.upForce);
        }
    }
    // 右ボタン
    public void GetMyLeftButtonDown()
    {
        this.isLButtonDown = true;
    }
    public void GetMyLeftButtonUp()
    {
        this.isLButtonDown = false;
    }
    // 左ボタン
    public void GetMyRightButtonDown()
    {
        this.isRButtonDown = true;
    }
    public void GetMyRightButtonUp()
    {
        this.isRButtonDown = false;
    }
}

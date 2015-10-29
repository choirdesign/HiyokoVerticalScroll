//1.カメラがプレイヤー(y座標だけ)を追跡する
//2.スコアのテキストをいじる
//3.プレイヤーの高さ3ごとにスコアに10点たす
//4.ゲームオーバー処理
//5.ハイスコアを表示する
//6.ScoreUpPosを超えたらゲーム画面の上方向に足場を２つ追加する
//7.壁を自動生成する

using UnityEngine;
using System.Collections;

//2UIをいじくるのに必要な名前空間
using UnityEngine.UI;

public class CameraScript : MonoBehaviour {

	//1プレイヤー(追跡対象のオブジェクトと位置)
	//1インスペクタ上からplayerにプレイヤーオブジェクトを設定しよう
	public GameObject player;
	private Transform playerTrans;

	//2Text用の変数と スコア表示用の変数
	//2public変数TextにはヒエラルキーのScoreをいれよう
	public Text scoreText;
	private int score = -10;

	//3座標と比較する数字
	private int scoreUpPos = 0;

	//4スクリプト呼ぶ用
	public GameOver gameOver;

	//5表示テキストオブジェクト,計算用変数,保存先のキー
	public Text highScoreText;
	private int highScore;
	private string key = "HIGH SCORE";

	//6生成する足場
	public GameObject step;
	public float minWidth = -6.0f;
	public float maxWidth = 6.0f;
	public float minHeight = 4.0f;
	public float maxHeight = 6.0f;

	//7
	public GameObject wall;
	private int createWallPos = 10;

	// public GroundScript groundScript;

	//1Playerオブジェクトの座標を取得する
	void Start (){
		//1プレイヤーの位置情報をキャッシュ
		playerTrans = player.GetComponent<Transform>();

		//2初期スコアを代入して画面に表示
		scoreText.text = "Score:0";

		//5保存されていたハイスコアをキーで呼び出し取得 保存されていなければ0が戻る
		highScore = PlayerPrefs.GetInt (key, 0);
		//5ハイスコアを表示
		highScoreText.text = "HighScore:" + highScore.ToString ();

	}

	void Update (){
		//nullチェック
		if (player != null) {
			//1プレイヤーの高さ（y軸）を取得
			float playerHeight = playerTrans.position.y;
			//1現在のカメラの高さ（y軸）を取得
			float currentCameraHeight = transform.position.y;
			//1Lerp(from,to,割合)最小値と最大値の間の値を取る。
			//1第３引数割合に0.0〜1.0の値を入れ、値を決定する。0.5なら調度真ん中の値を取得する。
			float newHeight = Mathf.Lerp (currentCameraHeight, playerHeight, 0.5f);
			//1現在のカメラの高さよりプレイヤーの高さのほうが高くなったら、カメラの高さをnewHeightにする。
			//1x軸とz軸は変更しない。
			if (playerHeight > currentCameraHeight) {
				transform.position = new Vector3 (transform.position.x, newHeight, transform.position.z);
			}

			//3Playerのy座標がscoreUpPosの数字を超えたら
			if (playerTrans.position.y >= scoreUpPos) {
				//3加点判定の数値(y座標)とスコアを増やす
				scoreUpPos += 6;
				score += 10;
				//3スコアを更新して表示
				scoreText.text = "Score:" + score.ToString ();
				//5ハイスコアよりスコアが高い場合ハイスコアを更新、保存
				if (score > highScore) {
					highScore = score;
					PlayerPrefs.SetInt (key, highScore);
					highScoreText.text = "HighScore:" + highScore.ToString ();
				}

				//6
				CreateStep ();

			}

			//4Playerの高さがカメラの高さ-6より低くなったらLoseメソッド呼ぶ
			if (playerTrans.position.y <= currentCameraHeight - 19) {
				gameOver.Lose ();
				Destroy (player);
			}

			//7
			if (playerTrans.position.y >= createWallPos) {
				CreateWall ();
				createWallPos += 10;
			}

		}
	}

	//6ランダム足場生成
	void CreateStep(){
		//6左右に１つずつランダムな位置に足場を生成する
		GameObject obj1 = Instantiate(step, new Vector2 (Random.Range (minWidth, 0f), scoreUpPos + Random.Range (minHeight, maxHeight)), step.transform.rotation) as GameObject;
		GameObject obj2 = Instantiate( step, new Vector2( Random.Range( 0f, maxWidth ), scoreUpPos + Random.Range( minHeight, maxHeight ) ), step.transform.rotation ) as GameObject;

		GroundScript ground1 = obj1.GetComponent<GroundScript>();
		if( ground1 ) {
			ground1.StepScaleShorten();
		}

		GroundScript ground2 = obj2.GetComponent<GroundScript>();
		if( ground2 ) {
			ground2.StepScaleShorten();
		}
	}

	//7壁を伸ばす
	void CreateWall() {
		Instantiate (wall, new Vector2 (-4.8f, createWallPos + 10), wall.transform.rotation);
		Instantiate (wall, new Vector2 (12.5f, createWallPos + 10), wall.transform.rotation);
	}

}
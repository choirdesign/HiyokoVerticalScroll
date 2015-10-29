//ボタン長押しでバーとジャンプ力がのびる処理

using UnityEngine;
using System.Collections;

//ひつよう
using UnityEngine.UI;

public class BarScript : MonoBehaviour {

	public Player player;
	RectTransform rt;

	//**バーの大きさが変わったらインスペクタ上でいじりたいよ
	public int maxBarWidth = 300;
	public float barHeight = 50.0f;

	//**バーの長さに掛ける数も、毎フレームゲージがどのくらい伸びるかもインスペクタ上でいじりたいよ
	public int jfMultiple = 3;
	public float frameAdd = 5.0f;

	//BarTopのRectTransformコンポーネントをキャッシュ
	void Start () {
		rt = GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	void Update () {
		//クリック中かつ PlayerScriptのisGroundedがtrueの時ゲージを増やす
		//if (Input.GetMouseButton (0) && player.isGrounded) {
		if (Input.GetMouseButton (0)) {
			//sizeDeltaでゲージの大きさを制御
			//左端から1フレームごとにframeAdd分ずつ増やす
			rt.sizeDelta = new Vector2 (rt.sizeDelta.x + frameAdd, barHeight);

			//ゲージの横幅が最大を超えたら幅0に戻す
			if (rt.sizeDelta.x >= maxBarWidth) {
				rt.sizeDelta = new Vector2 (maxBarWidth, barHeight);
			}
		}

		if (Input.GetMouseButtonUp (0)) {
			//PlayerのJumpメソッドにゲージの横幅jfMultiple倍分のjumpForceを送る
			//ジャンプしたらゲージを0に戻す
			player.SendMessage ("Jump", rt.sizeDelta.x * jfMultiple);
			rt.sizeDelta = new Vector2 (0.0f, barHeight);
		}

		//地面から落ちたらゲージを0に
		//if (player.isGrounded == false) {
		//	rt.sizeDelta = new Vector2 (0.0f, barHeight);
		//}
	
	}
}

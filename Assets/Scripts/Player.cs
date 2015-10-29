//プレイヤー動かしたりする
//**アニメーションをつなげ、切り替える
//@@BarScriptからの情報を受け取ってジャンプ力に反映

using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	Rigidbody2D rb; //キャッシュ用
	public int moveSpeed = 2;
	public LayerMask groundLayer; //地面のレイヤー

	//@@削除
	//public float jumpForce = 1700; //ジャンプ力

	public bool isGrounded; //着地判定 @@BarScriptが参照したいのでpublicに

	Animator anim;	//**キャッシュ用

	// Use this for initialization
	void Start () {
		//GetComponentの処理をキャッシュするゾ 処理が軽くなるんだっけ
		rb = GetComponent<Rigidbody2D>();

		//**Animatorをキャッシュ
		anim = GetComponent<Animator> ();
	}

	//クリック判定をひろう処理だから、ジャンプ処理はUpdateにかこうね
	void Update () {
		rb.velocity = new Vector2 (transform.localScale.x * moveSpeed,
			rb.velocity.y);

		//player中央から足元にかけて設置判定用のラインを引く
		//Linecast = これは、第１引数第２引数の２点間にラインを引いて、
		//そのラインに第３引数に指定したLayerを持つオブジェクトがあった場合、trueを返します
		isGrounded = Physics2D.Linecast (
			transform.position + transform.up * 1,     //第一引数
			transform.position - transform.up * 0.1f,  //第二引数
			groundLayer); //Linecastが判定するレイヤー    //第三引数

		//@@削除
		//if (isGrounded && Input.GetButtonDown ("Jump")) {
		//	Jump  ();
		//}

		Anim ();
	}



	//UpdateとFixedUpdateの違い
	//FixedUpdateは固定のインターバルごとに呼ばれる(デフォで0.02秒50fps)
	//Edit→ProjectSettings→TimeのTimeManagerのFixedTimestepで設定できます。
	//FixedUpdateは端末性能に依存しないfps
	//Updateは1frameごとに呼ばれる

	//つかいわけ
	//ボタンなど一瞬の処理はUpdate
	//移動処理など端末によって差を出したくない定量的な処理はFixedUpdateを使うようにする

	void FixedUpdate () {
		//velocity = 速度
		//x方向へmoveSpeed分移動させる
		//velocity.y = 今の速度そのまま = 重力の影響をそのままオブジェクトに反映する
		//localScale.xには1か-1が入る
		rb.velocity = new Vector2 (transform.localScale.x * moveSpeed, rb.velocity.y);
	}
		
	void OnCollisionEnter2D (Collision2D col) { //2Dの衝突判定
		if (col.gameObject.tag == "Wall") { //壁に当たる

			//temp変数にPlayerのlocalScaleを格納
			Vector2 temp = gameObject.transform.localScale;
			//localScale.xに-1をかける
			temp.x *= -1;
			gameObject.transform.localScale = temp;
		}
	}


	//@@publicに変更 引数を追加
	public void Jump (int jumpForce){
		if (isGrounded == true) {
			//**ジャンプアニメーションはじめる
			//**トリガーはboolと似ていて、1フレームだけtrue処理を実行したら即かってにfalseになる
			anim.SetTrigger ("Jump");

			//上向きの力
			rb.AddForce (Vector2.up * jumpForce);
			isGrounded = false;
		}
	}

	//アニメーション関連の処理 よくわからん
	void Anim(){
		//**velY:y方向へかかる速度単位、上へいくとプラス、下へ行くとマイナス
		float velY = rb.velocity.y;
		//**Animatorへパラメータを送る
		anim.SetFloat ("velY",velY);
		anim.SetBool ("isGrounded", isGrounded);
	}

}

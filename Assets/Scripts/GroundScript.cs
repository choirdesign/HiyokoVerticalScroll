//下から進入すると足場をすり抜ける
//xx画面外の足場をDestroyする
//ttPlayerが進むほど足場が狭まっていきたい

using UnityEngine;
using System.Collections;

public class GroundScript : MonoBehaviour {

	bool setOff;
	BoxCollider2D colliderOfGround;
	private Transform stepScale;

	//xx
	GameObject mainCam;

	//tt
	void Awake()
	{
		stepScale = GetComponent<Transform> ();
	}

	// Use this for initialization
	void Start () {
		//キャッシュ？ このオブジェクト
		colliderOfGround = GetComponent<BoxCollider2D> ();

		//xx 他のオブジェクトだからFindWithTag?
		mainCam = GameObject.FindWithTag ("MainCamera");


		//stepScale.localScale = new Vector2 (stepScale.localScale.x - 0.01f, stepScale.localScale.y);
	}

	// Update is called once per frame
	void Update () {
		if (setOff) {
			colliderOfGround.enabled = false;
		}
		if (!setOff) {
			colliderOfGround.enabled = true;
		}


		//xxMainCameraの高さから6を引いた位置より下に来たら足場をDestroy
		if (transform.position.y <= mainCam.transform.position.y - 20) {
			Destroy (gameObject);
		}

	}

	void OnTriggerStay2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			setOff = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			setOff = false;
		}
	}

	//ttCameraScriptでした足場の幅を設定
	public void StepScaleShorten (){
		//足場の生成された高さ/50
		float s = (stepScale.position.y/50);

		//最小の値 最も大きい足場
		if( s < 1.0f ) {
			s = 1.0f;
		}

		//最大の値 最も小さい足場
		else if( s > 50.0f ) {
			s = 50.0f;
		}

		//足場の幅/s  sが大きいほど足場が狭くなる
		stepScale.localScale = new Vector3( stepScale.localScale.x / s, stepScale.localScale.y, stepScale.localScale.z );
	}


}

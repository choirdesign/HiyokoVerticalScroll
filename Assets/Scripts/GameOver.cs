//GameOverの表示＆クリックでタイトルに戻る

//Textを扱うのでいれよう
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
	//フラグ
	bool gameOverFlg = false;

	void Start () {
		//GameOverの文字を非表示
		gameObject.GetComponent<Text> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		//フラグがtrueかつ画面クリックでタイトルにもどる
		if (gameOverFlg == true && Input.GetMouseButton (0)) {
			Application.LoadLevel ("Title");
		}
	}

	//CameraScriptから呼ばれる
	public void Lose() {
		//GameOverの文字を表示する
		gameObject.GetComponent<Text> ().enabled = true;
		//フラグをtrueに
		gameOverFlg = true;
	}
}

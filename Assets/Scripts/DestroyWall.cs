//MainCameraから外れたら壁オブジェクトを削除する

using UnityEngine;
using System.Collections;

public class DestroyWall : MonoBehaviour {

	GameObject mainCam;

	// Use this for initialization
	void Start () {
		mainCam = GameObject.FindWithTag ("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y <= mainCam.transform.position.y - 25) {
			Destroy (gameObject);
		}
	}
}

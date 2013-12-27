using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	
	public Camera GameCamera;
	
	public GameObject BubbleRef;
	public GameObject BubbleContRef;
	
	public float BubbleStartDelay;
	
	// Use this for initialization
	void Start () {
		Init();
	}
	
	void Init() {
		Application.targetFrameRate = 60;
		
		BubbleStartDelay = 0;
	}
	
	public void SetMyXScreenPos (GameObject _target, float _x) {
		Vector3 WorldPos = GameCamera.ScreenToWorldPoint (new Vector3 (_x, 0, 0));
		_target.transform.localPosition = new Vector3 (WorldPos.x, _target.transform.localPosition.y, _target.transform.localPosition.z);
	}
	
	public float OutputMyXScreenPos (GameObject _target, float _x) {
		Vector3 WorldPos = GameCamera.ScreenToWorldPoint (new Vector3 (_x, 0, 0));
		return WorldPos.x;
	}
	
	public void SetMyYScreenPos (GameObject _target, float _y) {
		Vector3 WorldPos = GameCamera.ScreenToWorldPoint (new Vector3 (0, _y, 0));
		_target.transform.localPosition = new Vector3 (_target.transform.localPosition.x, WorldPos.y, _target.transform.localPosition.z);
	}
	
	public float OutputMyYScreenPos (GameObject _target, float _y) {
		Vector3 WorldPos = GameCamera.ScreenToWorldPoint (new Vector3 (0, _y, 0));
		return WorldPos.y;
	}
	
	public void SpawnBubbles() {
		
		
		for (int i = 0; i < 25; i++) {
			BubbleStartDelay += .05f;
			GameObject _tempBubble = Instantiate(BubbleRef) as GameObject;
			
			_tempBubble.transform.name += "_"+ Random.Range(0, 999999);
			_tempBubble.transform.parent = BubbleContRef.transform;
			
			_tempBubble.GetComponent<GenericBubble>().MyDelay = BubbleStartDelay;
			StartCoroutine(_tempBubble.GetComponent<GenericBubble>().SetMeUp());
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

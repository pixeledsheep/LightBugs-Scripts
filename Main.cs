using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	
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
	
	public void SpawnBubbles() {
		
		
		for (int i = 0; i < 10; i++) {
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

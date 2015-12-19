using UnityEngine;
using System.Collections;

public class TestCamControl : MonoBehaviour {

    Vector3 lastPos;
    public float speed = 10.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 mPos = Input.mousePosition;
        
        if (Input.GetMouseButton(2))
        {
            float r = Time.deltaTime * speed * (mPos.x - lastPos.x);
            transform.Rotate(0, r, 0);
        }

        lastPos = mPos;
    }
}

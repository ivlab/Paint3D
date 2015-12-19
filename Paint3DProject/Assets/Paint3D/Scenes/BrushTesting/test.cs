using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {
    LineRenderer lr;
	// Use this for initialization
	void Start () {
        lr = gameObject.AddComponent<LineRenderer>();

	}
	
	// Update is called once per frame
	void Update () {
        lr.SetPosition(0, new Vector3(1, 2, 3));
        lr.SetPosition(1, new Vector3(2, 1, 3));
        lr.SetVertexCount(3);
        lr.SetPosition(2, new Vector3(3, 2, 1));
	}
}

using UnityEngine;
using System.Collections;

public class PaintTester : MonoBehaviour {
    Painting painting;
    Camera mainCam;

    Vector3 lastPos;

    public string currentBrush { get { return painting.CurrentBrush; } set { painting.CurrentBrush = value; } }

	// Use this for initialization
	void Start () {
        painting = gameObject.AddComponent<Painting>();
        mainCam = GameObject.FindObjectOfType<Camera>();

        //painting.CurrentBrush = "LineBrush";
        painting.CurrentBrush = "BubbleBrush";
        //painting.CurrentBrush = "TubeBrush";

        //painting.StartNewStroke();
        //
        //Vertex v = new Vertex();
        //v.position = new Vector4(0, 0, 0);
        //v.orientation = Quaternion.LookRotation(Vector3.forward);
        //painting.AddVertex(v);
        //
        //v = new Vertex();
        //v.position = new Vector4(0, 0, 3);
        //Quaternion q = Quaternion.LookRotation(Vector3.forward);
        //q = q * Quaternion.Euler(new Vector3(-45, 0, 0));
        //v.orientation = q;
        //painting.AddVertex(v);
        //
        //v = new Vertex();
        //v.position = new Vector4(0, 3, 3);
        //v.orientation = Quaternion.LookRotation(Vector3.up);
        //painting.AddVertex(v);
        //
        //painting.EndStroke();
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            painting.StartNewStroke();
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            painting.EndStroke();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            painting.EndStroke();
        }
        Vector3 mpos = Input.mousePosition;
        mpos.z = 10f;
        Vector3 pos = mainCam.ScreenToWorldPoint(mpos);

        if (painting.CurrentStroke != null)
        {
            Vector3 direction = pos - lastPos;
            direction.Normalize();

            Vertex v = new Vertex();
            v.position = pos;
            v.orientation = Quaternion.LookRotation(direction, Vector3.up);
            v.pressure = 1.0f;
            painting.AddVertex(v);
            //painting.CurrentStroke.enabled = true;
        }

        lastPos = pos;
    }
}

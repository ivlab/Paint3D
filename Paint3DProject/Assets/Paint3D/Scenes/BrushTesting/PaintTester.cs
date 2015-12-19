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
        painting.CurrentBrush = "TubeBrush";

        painting.StartNewStroke();

        Vertex v = new Vertex();
        v.position = new Vector4(0, 0, 0);
        v.orientation = Quaternion.LookRotation(v.position);
        painting.AddVertex(v);

        v = new Vertex();
        v.position = new Vector4(0, 0, 3);
        v.orientation = Quaternion.LookRotation(v.position);
        painting.AddVertex(v);

        v = new Vertex();
        v.position = new Vector4(0, 3, 3);
        v.orientation = Quaternion.LookRotation(v.position - new Vector4(2,0,0));
        painting.AddVertex(v);

        painting.EndStroke();
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
        Vector3 mpos = Input.mousePosition;
        Ray ray = mainCam.ScreenPointToRay(mpos);
        Vector3 pos = ray.GetPoint(5);

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

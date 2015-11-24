using UnityEngine;
using System.Collections;


namespace MinVR {

	public class VRCameraPairHeadTracking : MonoBehaviour {

		public string headTrackingEvent = "Head_Move";
		public string matrix4x4DataField = "Transform";


		// Use this for initialization
		void Start () {
			VRMain.VREventHandler += OnVREvent;
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		void OnVREvent(VREvent e) {
			if (e.Name == headTrackingEvent) {		
				Matrix4x4 m = VRConvert.ToMatrix4x4(e.DataIndex.GetValueAsDoubleArray(matrix4x4DataField));
				transform.position = m.GetTranslation();
				transform.rotation = m.GetRotation();
				//Debug.Log("Head Position = " + m.GetTranslation().ToString());
				//Debug.Log("Head Rotation = " + m.GetRotation().ToString());
			}
		}
	}

} // namespace MinVR
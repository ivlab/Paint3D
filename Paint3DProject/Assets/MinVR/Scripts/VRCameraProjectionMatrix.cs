using UnityEngine;
using System.Collections;

/*
 Set an off-center projection, where perspective's vanishing point is not necessarily in the center of the screen.
 Attach this script to BOTH of the cameras in a VRCameraPair.

 left/right/top/bottom define near plane size, i.e. how offset are corners of camera's near plane.
 Tweak the values and you can see camera's frustum change.

	Values that work for iPhone4:

	Left Eye Camera:
	Width = 0.45
	Height = 0.66
	Scale = 0.65
	Horiz Offset = -0.02

	Right Eye Camera:
	Width = 0.45
	Height = 0.66
	Scale = 0.65
	Horiz Offset = -0.02

 TODO: Talk with David Schroeder (original author) about the scale factor used here.  Was that just to make it easier
 to experimentally determine reasonable parameters for the iPhone?  Why not just measure the physical screen size and
 physical distance to the center of the eye as we would often do in VR?  Maybe this is too hard with the iPhone?
*/

namespace MinVR {

	[ExecuteInEditMode]
	public class VRCameraProjectionMatrix : MonoBehaviour {

		public float viewportWidth = 0.45f;
		public float viewportHeight = 1.0f;
		public float scaleFactor = 1.0f;
		public float eyeHorizOffset = 0.0f;


		void LateUpdate () {
			Camera cam = gameObject.GetComponent<Camera>();
			float left   = scaleFactor * -viewportWidth / 2.0f + eyeHorizOffset;
			float right  = scaleFactor *  viewportWidth / 2.0f + eyeHorizOffset;
			float top    = scaleFactor *  viewportHeight / 2.0f;
			float bottom = scaleFactor * -viewportHeight / 2.0f;
			float near = cam.nearClipPlane;
			float far = cam.farClipPlane;


			float x =  (2.0f * near) / (right - left);
			float y =  (2.0f * near) / (top - bottom);
			float a =  (right + left) / (right - left);
			float b =  (top + bottom) / (top - bottom);
			float c = -(far + near) / (far - near);
			float d = -(2.0f * far * near) / (far - near);
			float e = -1.0f;

			Matrix4x4 m = new Matrix4x4();
			m[0,0] = x;   m[0,1] = 0f;  m[0,2] = a;  m[0,3] = 0f;
			m[1,0] = 0f;  m[1,1] = y;   m[1,2] = b;  m[1,3] = 0f;
			m[2,0] = 0f;  m[2,1] = 0f;  m[2,2] = c;  m[2,3] = d;
			m[3,0] = 0f;  m[3,1] = 0f;  m[3,2] = e;  m[3,3] = 0f;

			cam.projectionMatrix = m;
		}

	}
} // namespace MinVR
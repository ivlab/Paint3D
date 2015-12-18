using UnityEngine;
using System.Collections;


namespace MinVR
{

	public class VRCameraPairHeadTracking : MonoBehaviour
	{

		public string user1HeadTrackingEvent = "Head1_Move";
	    public string user2HeadTrackingEvent = "Head2_Move";
		public string matrix4x4DataField = "Transform";
	    public GameObject user1Avatar;
	    public GameObject user2Avatar;

	    public int user_id = 1;


		// Use this for initialization
		void Start ()
		{
			VRMain.VREventHandler += OnVREvent;
		}
		
		// Update is called once per frame
		void Update ()
		{
		
		}

		void OnVREvent (VREvent e)
		{
			if (e.Name == user1HeadTrackingEvent ) {		
				Matrix4x4 m = VRConvert.ToMatrix4x4 (e.DataIndex.GetValueAsDoubleArray (matrix4x4DataField));

			    if (user_id == 1)
			    {
			        transform.position = m.GetTranslation();
			        transform.rotation = m.GetRotation();
			    }
			    //Debug.Log("Head Position = " + m.GetTranslation().ToString());
                //Debug.Log("Head Rotation = " + m.GetRotation().ToString());

                //------------------ Avatar 1 movement ------------------------
                // position
                Vector3 pos = m.GetTranslation();
                user1Avatar.transform.position = new Vector3(pos.x, user1Avatar.transform.position.y, pos.z);

                // rotation
                Quaternion rot = m.GetRotation();
                // 3rd column of a rotation matrix is the z-axis
                Vector3 z = new Vector3(m.GetColumn(2).x, m.GetColumn(2).y, m.GetColumn(2).z);
                // z-axis of the tracker projected onto a horizontal plane
                Vector3 zHorizontal = (z - Vector3.Dot(z, Vector3.up) * Vector3.up).normalized;

                // align the z axis of the avatar with zHorizontal 
                Quaternion newRot = Quaternion.FromToRotation(new Vector3(0, 0, 1), zHorizontal);
                user1Avatar.transform.rotation = newRot;

            } else if (e.Name == user2HeadTrackingEvent)
			{
                Matrix4x4 m = VRConvert.ToMatrix4x4(e.DataIndex.GetValueAsDoubleArray(matrix4x4DataField));

                if (user_id == 2)
                {
                    transform.position = m.GetTranslation();
                    transform.rotation = m.GetRotation();
                }
                //Debug.Log("Head Position = " + m.GetTranslation().ToString());
                //Debug.Log("Head Rotation = " + m.GetRotation().ToString());

                //------------------ Avatar 2 movement ------------------------
                // position
                Vector3 pos = m.GetTranslation();
                user2Avatar.transform.position = new Vector3(pos.x, user2Avatar.transform.position.y, pos.z);

                // rotation
                Quaternion rot = m.GetRotation();
                // 3rd column of a rotation matrix is the z-axis
                Vector3 z = new Vector3(m.GetColumn(2).x, m.GetColumn(2).y, m.GetColumn(2).z);
                // z-axis of the tracker projected onto a horizontal plane
                Vector3 zHorizontal = (z - Vector3.Dot(z, Vector3.up) * Vector3.up).normalized;

                // align the z axis of the avatar with zHorizontal 
                Quaternion newRot = Quaternion.FromToRotation(new Vector3(0, 0, 1), zHorizontal);
                user2Avatar.transform.rotation = newRot;
            }
		}
	}

} // namespace MinVR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager
{
    Dictionary<Defines.CameraType, Transform> CameraList = new Dictionary<Defines.CameraType, Transform>();
    Transform m_trackingTarget;
    Vector3 m_cameraHeight = new Vector3(0, 0, -10);

    public void BindCamera(Defines.CameraType type)
    {
        if (CameraList.ContainsKey(type))
            return;

        Transform camera = GameObject.Find($"{System.Enum.GetName(typeof(Defines.CameraType), type)} Camera").transform;

        if (camera == null)
        {
            Debug.Log($"Failed To Find Camera Object : ({type})");
        }

        CameraList.Add(type, camera);
    }

    public void UpdateCameraPos(Defines.CameraType type)
    {
        if (m_trackingTarget == null)
            return;

        if (CameraList.ContainsKey(type) == false)
            return;

        CameraList[type].position = m_trackingTarget.position + m_cameraHeight;
    }

    public void SetTrackingTarget(Transform target)
    {
        m_trackingTarget = target;
    }

    public void ClearCameraList()
    {
        CameraList = new Dictionary<Defines.CameraType, Transform>();
    }
}

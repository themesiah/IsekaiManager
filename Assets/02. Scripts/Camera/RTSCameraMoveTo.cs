using UnityEngine;

namespace Isekai.Camera
{
    public class RTSCameraMoveTo : MonoBehaviour
    {
        public void MoveTo(object referencePoint)
        {
            MoveTo((Vector3)referencePoint);
        }

        // This method does not move the camera to that point. This method moves the camera to a point above the referencePoint and looking at it.
        public void MoveTo(Vector3 referencePoint)
        {
            float currentY = transform.position.y;
            Vector3 newPosition = referencePoint;
            newPosition.y = currentY;
            transform.position = newPosition;
            Vector3 currentEuler = transform.eulerAngles;
            Vector3 newEuler = transform.eulerAngles;
            newEuler.x = 0f;
            transform.eulerAngles = newEuler;
            Vector3 backTranslation = Vector3.back * Mathf.Cos(Mathf.Deg2Rad * currentEuler.x) * (Mathf.Max(currentY-referencePoint.y, 0f) / Mathf.Sin(Mathf.Deg2Rad * currentEuler.x));
            transform.Translate(backTranslation);
            transform.eulerAngles = currentEuler;
        }
    }
}
#if ENABLE_INPUT_SYSTEM && ENABLE_INPUT_SYSTEM_PACKAGE
#define USE_INPUT_SYSTEM
    using UnityEngine.InputSystem;
    using UnityEngine.InputSystem.Controls;
#endif

using UnityEngine;
using UnityEngine.PlayerLoop;

namespace UnityTemplateProjects
{
    public class ZoomCamera : MonoBehaviour
    {
        public int minimumZoom, maximumZoom;

        public float zoomAmount = 1.0f;

        [SerializeField]
        private GameObject floor;

        private void Update()
        {
            float scrollAmount = Input.GetAxis("Mouse ScrollWheel");

            // Negative strength when zooming out
            float zoomStrength = scrollAmount * zoomAmount;

            Vector3 newPosition = transform.forward * zoomStrength;
            
            float floorDistance = transform.position.y - floor.transform.position.y;

            bool positive = zoomStrength > 0;

            // Zooming in
            if (positive && floorDistance - zoomStrength > minimumZoom)
            { 
                transform.position += newPosition;
            }
            if (!positive && floorDistance + zoomStrength < maximumZoom)
            {
                transform.position += newPosition;
            }
        }
    }

}
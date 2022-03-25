using UnityEngine;

namespace Rings
{
    public class AdaptivePanel : MonoBehaviour
    {
        #region VAR   
        [SerializeField] private RectTransform _panel;
        private Rect _lastSafeArea;
        #endregion
        
        #region MONO    
        private void Awake()
        {
            var safeArea = Screen.safeArea;

            if (safeArea != _lastSafeArea)
            {
                ApplySafeArea(safeArea);
            }
        }
        #endregion

        #region FUNC   
        private void ApplySafeArea(Rect area)
        {
            var anchorMin = area.position;
            var anchorMax = area.position + area.size;
            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;
            _panel.anchorMin = anchorMin;
            _panel.anchorMax = anchorMax;

            _lastSafeArea = area;
        }
        #endregion   
    }
}

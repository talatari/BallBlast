using UnityEngine;

namespace BallBlastClone
{
    public class LevelBoundary : MonoBehaviour
    {
        [SerializeField] private Vector2 _screenResolution;

        public static LevelBoundary Instance;

        public void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (Application.isEditor == false && Application.isPlaying == true)
            {
                _screenResolution.x = Screen.width;
                _screenResolution.y = Screen.height;
            }
        }

        public float LeftBorder
        {
            get
            {
                return Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
            }
        }

        public float RightBorder
        {
            get
            {
                return Camera.main.ScreenToWorldPoint(new Vector3(_screenResolution.x, 0, 0)).x;
            }

        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(new Vector3(LeftBorder, -10, 0), new Vector3(LeftBorder, 10, 0));
            Gizmos.DrawLine(new Vector3(RightBorder, -10, 0), new Vector3(RightBorder, 10, 0));
        }
#endif


    }
}
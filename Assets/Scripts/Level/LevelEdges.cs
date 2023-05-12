using UnityEngine;

namespace BallBlastClone
{
    public enum EdgeType
    {
        Left,
        Right,
        Bottom
    }

    public class LevelEdges : MonoBehaviour
    {
        [SerializeField] private EdgeType _edgeType;

        public EdgeType EdgeType => _edgeType;


    }
}
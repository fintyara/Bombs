using UnityEngine;


namespace NucGames.Bombs
{
    public class Grid : MonoBehaviour
    {
        public Vector2 CellSize => _cellSize;
        public Vector2 CellGap => _cellGap;
        public Vector2 SizeGrid => _sizeGrid;
        public float OffsetLine => _offsetLine;
        [SerializeField] private Vector2 _cellSize = new Vector2(1, 1);
        [SerializeField] private Vector2 _cellGap;
        [SerializeField] private Vector2 _sizeGrid;
        [Space(10)] [SerializeField] private float _offsetLine;
    }
}

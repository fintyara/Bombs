using UnityEngine;


namespace NucGames.Bombs
{
    public class Cells : MonoBehaviour
    {
        [SerializeField] private GameObject _cellPref;
        [SerializeField] private GameObject _linePref;
        [SerializeField] private Grid _grid;
        
        
        public void FillCells()
        {
            DestroyTopLevelChildren();
            
            Vector3 pos = Vector3.zero;
            float offset = 0;
            int count = 0;
                
            for (int i = 0; i < _grid.SizeGrid.y; i++)
            {
                GameObject line = Instantiate(_linePref, transform);
                line.transform.Translate(0, pos.y, 0);
                
                for (int j = 0; j < _grid.SizeGrid.x; j++)
                {
                    GameObject cell = Instantiate(_cellPref, transform.position + pos, transform.rotation);
                    count++;
                    cell.name = count.ToString();
                    cell.transform.SetParent(line.transform);

                    pos.x += _grid.CellSize.x + _grid.CellGap.x;
                }
                
                offset += _grid.OffsetLine;
                pos.x = offset;
                pos.y += _grid.CellSize.y + _grid.CellGap.y;
            }
        }
        
        private void DestroyTopLevelChildren()
        {
            Transform[] children = new Transform[transform.childCount];
            for (int ID = 0; ID < transform.childCount; ID++)
            {
                children[ID] = transform.GetChild(ID);
            }
            
            for (int i = 0; i < children.Length; i++)
            {
                DestroyImmediate(children[i].gameObject);
            }
        }
    }
}

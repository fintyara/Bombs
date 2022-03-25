using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace NucGames.Bombs
{
#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif
    public class MapEditor : MonoBehaviour
    {
        public int SelectedCat => _selectedCat;
        [SerializeField] private bool _active;
        [Space(10)] public List<string> _nameFolders;
        [SerializeField]  public LayerMask LayerMask;
        [Space(10)]
        [SerializeField] private bool _needDelete;
        public List<List<GameObject>> AllPref;
        private int _selectedCat;
        private int _curIndex;
        
        
        public void SetIndex(int index)
        {
            _curIndex = index;
            _needDelete = false;
        }
        public void ClickedOnCell(GameObject cellGO)
        {
            if (!_active)
                return;
            
            if (cellGO.transform.childCount > 0)
                DestroyImmediate(cellGO.transform.GetChild(0).gameObject);

            if (!_needDelete)
                SpawnInCell(cellGO);
        }
        private void SpawnInCell(GameObject cellGO)
        {
#if UNITY_EDITOR
         GameObject spawned  = PrefabUtility.InstantiatePrefab(
                    Resources.Load(("Prefabs/" + _nameFolders[_selectedCat] + "/" +
                                    AllPref[_selectedCat][_curIndex].name))) as
                GameObject;

            
            spawned.transform.SetParent(cellGO.transform);
            spawned.transform.localPosition = Vector3.zero;
#endif
        }
        public bool CheckNames()
        {
            for (int i = 0; i < _nameFolders.Count; i++)
                if (_nameFolders[i] == "")
                    return false;

            return true;
        }
        public void LoadPrefabs()
        {
            if (!_active || _nameFolders.Count == 0)
                return;

            AllPref = new List<List<GameObject>>();
            GameObject[] prefabs;

            for (int i = 0; i < _nameFolders.Count; i++)
            {
                List<GameObject> newList = new List<GameObject>();

                prefabs = Resources.LoadAll<GameObject>("Prefabs/" + _nameFolders[i] + "/");

                if (prefabs.Length > 0)
                {
                    for (int j = 0; j < prefabs.Length; j++)
                        newList.Add(prefabs[j]);
                }

                AllPref.Add(newList);
            }
        }
        public void ChangeCategory(int _index)
        {
            if (!_active)
                return;
            
            _selectedCat = _index;
            LoadPrefabs();
        }
    }
}

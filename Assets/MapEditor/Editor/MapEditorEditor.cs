using UnityEngine;
using UnityEditor;


namespace NucGames.Bombs
{
    [CustomEditor(typeof(MapEditor))]
    public class MapEditorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            MapEditor myTarget = (MapEditor) target;

            GUILayout.Space(20);
            if (GUILayout.Button("Load prefabs", GUILayout.Height(20)))
                myTarget.LoadPrefabs();
            GUILayout.Space(20);
           
            DrawDefaultInspector();
            
            if (!myTarget.CheckNames())
                return;

            if (myTarget.AllPref == null)
                myTarget.LoadPrefabs();

            if (myTarget.AllPref == null)
                return;

            //slots
            GUILayout.BeginHorizontal();
            for (int i = 0; i < 3; i++)
            {
                GUILayout.BeginVertical();
                for (int j = 0; j < 10; j++)
                {
                    if (i * 10 + j < myTarget.AllPref[myTarget.SelectedCat].Count)
                    {
                        if (GUILayout.Button(myTarget.AllPref[myTarget.SelectedCat][i * 10 + j].name,
                            GUILayout.Width(130), GUILayout.Height(20)))
                            myTarget.SetIndex(i * 10 + j);
                    }
                    else if (GUILayout.Button("-", GUILayout.Width(130), GUILayout.Height(20)))
                        Debug.Log("Empty!");

                    GUILayout.Space(2);
                }

                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();

            //categories
            GUILayout.BeginVertical();
            for (int i = 0; i < myTarget.AllPref.Count; i++)
            {
                if (GUILayout.Button(myTarget._nameFolders[i], GUILayout.Height(20)))
                    myTarget.ChangeCategory(i);

                GUILayout.Space(5);
            }
            GUILayout.EndVertical();
        }
        private void OnSceneGUI()
        {
            MapEditor myTarget = (MapEditor) target;

            if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
            {
                Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (hit.collider != null && hit.collider.GetComponent<EntityBase>() != null)
                {
                    myTarget.ClickedOnCell(hit.transform.parent.gameObject);
                    return;
                }

                hit = Physics2D.Raycast(ray.origin, ray.direction, myTarget.LayerMask);
                if (hit.collider != null)
                    myTarget.ClickedOnCell(hit.transform.gameObject);
            }
        }
    }
}

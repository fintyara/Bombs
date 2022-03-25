using UnityEngine;
using UnityEditor;


namespace NucGames.Bombs
{
    [CustomEditor(typeof(Cells))]
    public class CellsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            Cells myTarget = (Cells) target;

            GUILayout.Space(20);
            if (GUILayout.Button("Fill cells", GUILayout.Height(20)))
                myTarget.FillCells();
            
            GUILayout.Space(30);
            
            DrawDefaultInspector();
        }
    }
}

using UnityEditor;
using UnityEngine;

namespace ScriptsData.Utils.Navigation
{
    [CustomEditor(typeof(PathCreator))]
    public class EditorPathCreator : Editor
    {
        public override void OnInspectorGUI() // We need to draw button which allow us to generate path for all enemies
        {
            DrawDefaultInspector();

            var pathCreator = (PathCreator)target;
            if (!GUILayout.Button("(re)generate Path"))
                return;
            PathCreator.GeneratePath();
            for (var i = 0; i < pathCreator.Nodes.Count; i++) // Connect each node to our path
            {
                if(i >= pathCreator.Nodes.Count-1)
                    return;
                Debug.Log($"Line from {i} to {i+1} has been drawn");
                Debug.DrawLine(pathCreator.Nodes[i].transform.position, pathCreator.Nodes[i+1].transform.position, Color.green, 12312);
            }

        }
    }
}
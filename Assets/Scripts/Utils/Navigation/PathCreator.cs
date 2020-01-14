using System.Collections.Generic;
using UnityEngine;
namespace Utils.Navigation
{
    public class PathCreator : MonoBehaviour
    {
        public List<Node> Nodes;

        public static void GeneratePath() //This static method should (re)generate path for all enemies currently alive
        {
            Debug.Log("Generated. Path has been created.");
        }

        public void RemoveNode() //Remove simple point of the path
        {

        }

        private void RefreshIndex() //Refresh all data in List (We need to call it after each "Remove" call)
        {
            //Nodes.
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    int[,] neighbourOffsets = new int[,] {{-1,0},{1,0},{0,-1},{0,1}};

    int[,] maze = {
        {0,0,0,0,0},
        {0,0,0,0,0},
        {0,0,1,0,0},
        {0,0,1,1,0},
        {0,0,0,0,0},
        {0,0,0,0,0},
    };

    List<Node> FindPath(int[,] map, Node start, Node end) {
        List<Node> search = new List<Node>() {start};
        List<Node> processed = new List<Node>();

        while (search.Count != 0) {
            Node current = search[0];
            foreach (Node n in search) {
                if (n.f < current.f || (n.f == current.f && n.h < current.h) ) {
                    current = n;
                }
            }

            search.Remove(current);
            processed.Add(current);

            for (int i = 0; i < neighbourOffsets.GetLength(0); i++) {
                int[] nPos = {current.pos[0] + neighbourOffsets[i,0], current.pos[1] + neighbourOffsets[i,1]};
                if (nPos[0] < 0 || nPos[0] >= maze.GetLength(0)) continue;
                if (nPos[1] < 0 || nPos[1] >= maze.GetLength(1)) continue;
                if (map[nPos[0],nPos[1]] == 0) {
                    Debug.Log(nPos[0] + " " + nPos[1]);
                }
            }
            break;

        }

        return null;
    }
    
    void Start() {
        Node start = new Node(0,0);
        Node end = new Node(4,4);
        // Debug.Log(start.GetDistance(end));
        FindPath(maze, start, end);
    }
}

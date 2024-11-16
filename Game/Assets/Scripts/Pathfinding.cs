using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    int[,] neighbourOffsets = new int[,] {{-1,0},{1,0},{0,-1},{0,1}};

    int[,] maze = {
        {0,0,0,0,0},
        {1,1,1,0,0},
        {0,0,0,0,0},
        {0,1,1,1,1},
        {0,0,1,0,0},
        {0,0,0,0,0},
    };

    Node FindNode(List<Node> list, int[] pos) { // search for a node in a node list with a given position
        foreach (Node node in list) {
            if (node.pos[0] == pos[0] && node.pos[1] == pos[1]) return node;
        }
        return null;
    }

    Node tempCurrent;

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

            if (current.pos[0] == end.pos[0] && current.pos[1] == end.pos[1]) {
                List<Node> path = new List<Node>();
                Node traceback = current;
                while (traceback.pos[0] != start.pos[0] || traceback.pos[1] != start.pos[1]) {
                    path.Add(traceback);
                    traceback = traceback.parent;
                }
                return path;
            }

            for (int i = 0; i < neighbourOffsets.GetLength(0); i++) {

                int[] nPos = {current.pos[0] + neighbourOffsets[i,0], current.pos[1] + neighbourOffsets[i,1]};
                if (nPos[0] < 0 || nPos[0] >= maze.GetLength(0)) continue;
                if (nPos[1] < 0 || nPos[1] >= maze.GetLength(1)) continue;
                if (map[nPos[0],nPos[1]] == 1) continue;
                if (FindNode(processed,nPos) != null) continue;

                Node neighbour = FindNode(search, nPos);
                bool inSearch = neighbour != null;
                if (neighbour == null) neighbour = new Node(nPos[0],nPos[1]);
                int G = current.g + 1;

                if (!inSearch || G < neighbour.g) {
                    neighbour.SetG(G);
                    neighbour.SetParent(current);

                    if (!inSearch) {
                        neighbour.SetH(neighbour.GetDistance(end));
                        search.Add(neighbour);
                    }
                }
            }

            // Debug.Log("Iteration " + temp);
            // Debug.Log("Current: " + current.pos[0] + " " + current.pos[1] + " | F: " + current.f + " | G: " + current.g + " | H: " + current.h);
            // Debug.Log("Search:");
            // foreach (Node node in search) Debug.Log("Node: " + node.pos[0] + " " + node.pos[1] + " | F: " + node.f + " | G: " + node.g + " | H: " + node.h);
            // Debug.Log("Processed:");
            // foreach (Node node in processed) Debug.Log("Node: " + node.pos[0] + " " + node.pos[1] + " | F: " + node.f + " | G: " + node.g + " | H: " + node.h);

        }

        return null;
    }
    
    void Start() {
        Node start = new Node(0,0);
        Node end = new Node(4,4);
        
        // FindPath(maze, start, end);
        List<Node> path = FindPath(maze, start, end);
        path.Reverse();
        foreach (Node node in path) {
            Debug.Log(node.pos[0] + " " + node.pos[1]);
        }
    }
}

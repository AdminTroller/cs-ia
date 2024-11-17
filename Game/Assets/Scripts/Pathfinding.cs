using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinding : MonoBehaviour
{

    [SerializeField] Tilemap walls;
    int[,] maze;

    int[,] neighbourOffsets = new int[,] {{-1,0},{1,0},{0,-1},{0,1}};

    Node FindNode(List<Node> list, int[] pos) { // search for a node in a node list with a given position
        foreach (Node node in list) {
            if (node.pos[0] == pos[0] && node.pos[1] == pos[1]) return node;
        }
        return null;
    }

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
                if (nPos[0] < 0 || nPos[0] >= map.GetLength(0)) continue;
                if (nPos[1] < 0 || nPos[1] >= map.GetLength(1)) continue;
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
        // int[,] maze = {
        //     {0,0,0,0,0},
        //     {1,1,1,0,0},
        //     {0,0,0,0,0},
        //     {0,1,1,1,1},
        //     {0,0,1,0,0},
        //     {0,0,0,0,0},
        // };
        // Node start = new Node(0,0);
        // Node end = new Node(4,4);
        
        // List<Node> path = FindPath(maze, start, end);
        // path.Reverse();
        // foreach (Node node in path) {
        //     Debug.Log(node.pos[0] + " " + node.pos[1]);
        // }

        walls.CompressBounds();
        BoundsInt bounds = walls.cellBounds;
        Debug.Log(bounds);

        Debug.Log(bounds.xMin);
        Debug.Log(bounds.xMax);
        Debug.Log(bounds.yMin);
        Debug.Log(bounds.yMax);

        maze = new int[bounds.yMax - bounds.yMin, bounds.xMax - bounds.xMin];
        for (int j=bounds.yMax-1; j>=bounds.yMin; j--) {
            for (int i=bounds.xMin; i<=bounds.xMax-1; i++) {
                if (walls.HasTile(new Vector3Int(i,j,0))) maze[j-bounds.yMin, i-bounds.xMin] = 1;
                else maze[j-bounds.yMin, i-bounds.xMin] = 0;
            }
        }

        // for (int i=maze.GetLength(0)-1; i>=0; i--) {
        //     String temp = "";
        //     for (int j=0; j<maze.GetLength(1); j++) {
        //         temp += maze[i,j];
        //     }
        //     Debug.Log(temp);
        // }
    }

    void Update() {
        walls.CompressBounds();
        BoundsInt bounds = walls.cellBounds;
        float xOffset = bounds.xMin + 0.5f;
        float yOffset = bounds.yMin + 0.5f;
        for (int i=0; i<maze.GetLength(0); i++) {
            for (int j=0; j<maze.GetLength(1); j++) {
                if (maze[i,j] == 1) {
                    Debug.DrawLine(new Vector2(j-0.4f,i-0.4f)+new Vector2(xOffset,yOffset), new Vector2(j+0.4f,i+0.4f)+new Vector2(xOffset,yOffset));
                    Debug.DrawLine(new Vector2(j-0.4f,i+0.4f)+new Vector2(xOffset,yOffset), new Vector2(j+0.4f,i-0.4f)+new Vector2(xOffset,yOffset));
                }
            }
        }
    }
}

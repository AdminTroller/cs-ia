using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    Vector2 dir;

    [SerializeField] Transform player;
    [SerializeField] BoxCollider2D playerCol;
    [SerializeField] LayerMask tileMask;
    Vector2Int playerPosRound;
    Vector2Int posRound;
    float trackCooldown = 0;

    int state = 2; // 0 = idle, 1 = wandering, 2 = pursuit
    float speed = 5f;

    [SerializeField] Tilemap walls;
    BoundsInt bounds;
    int[,] maze;
    List<Node> path;
    int currentNode = -1;

    int xOffset;
    int yOffset;

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
                path.Add(traceback);
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

        walls.CompressBounds();
        bounds = walls.cellBounds;

        maze = new int[bounds.yMax - bounds.yMin, bounds.xMax - bounds.xMin];
        for (int j=bounds.yMax-1; j>=bounds.yMin; j--) {
            for (int i=bounds.xMin; i<=bounds.xMax-1; i++) {
                if (walls.HasTile(new Vector3Int(i,j,0))) maze[j-bounds.yMin, i-bounds.xMin] = 1;
                else maze[j-bounds.yMin, i-bounds.xMin] = 0;
            }
        }

        walls.CompressBounds();
        xOffset = bounds.xMin;
        yOffset = bounds.yMin;
    }

    void Update() {
        RaycastHit2D playerRay = Physics2D.Linecast(transform.position, player.transform.position, tileMask);
        Debug.DrawLine(transform.position, player.transform.position, Color.green);
        if (playerRay.collider == null) state = 1;
        else state = 2;

        if (state == 1) {
            ChasePlayer();
        }
        else if (state == 2) {
            trackCooldown += Time.deltaTime;
            TrackPlayer();
            PathfindPlayer();
            if (trackCooldown >= 1) trackCooldown = 0;
        } 
    }

    void ChasePlayer() {
        dir = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
    }

    void TrackPlayer() {
        if (trackCooldown >= 1) { // update pathfind every second
            playerPosRound = new Vector2Int(Mathf.FloorToInt(player.transform.position.x), Mathf.FloorToInt(player.transform.position.y));
            posRound = new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y));

            Node start = new Node(posRound.y - yOffset, posRound.x - xOffset);
            Node end = new Node(playerPosRound.y - yOffset, playerPosRound.x - xOffset);
            
            path = FindPath(maze, start, end);
            if (path != null) path.Reverse();
        }
    }

    void PathfindPlayer() {
        if (path != null) {
            for (int i=0; i<path.Count-1; i++) {
                Debug.DrawLine(new Vector2(path[i].pos[1], path[i].pos[0])+new Vector2(xOffset+0.5f,yOffset+0.5f), new Vector2(path[i+1].pos[1], path[i+1].pos[0])+new Vector2(xOffset+0.5f,yOffset+0.5f),Color.red);
            }

            for (int i=0; i<path.Count; i++) {
                if (Mathf.Abs(transform.position.x - 0.5f - xOffset - path[i].pos[1]) <= 0.8f && Mathf.Abs(transform.position.y - 0.5f - yOffset - path[i].pos[0]) <= 0.8f) {
                    currentNode = i+1;
                    if (currentNode >= path.Count) currentNode -= 1;
                    break;
                }
            }

            if (currentNode != -1) {
                // Debug.DrawLine(new Vector2(path[currentNode].pos[1] + xOffset + 0.5f + 0.5f, path[currentNode].pos[0] + yOffset + 0.5f + 0.5f), new Vector2(path[currentNode].pos[1] + xOffset - 0.5f + 0.5f, path[currentNode].pos[0] + yOffset - 0.5f + 0.5f), Color.green);
                // Debug.DrawLine(new Vector2(path[currentNode].pos[1] + xOffset - 0.5f + 0.5f, path[currentNode].pos[0] + yOffset + 0.5f + 0.5f), new Vector2(path[currentNode].pos[1] + xOffset + 0.5f + 0.5f, path[currentNode].pos[0] + yOffset - 0.5f + 0.5f), Color.green);
                // transform.position = Vector2.MoveTowards(transform.position, new Vector2(path[currentNode].pos[1] + xOffset + 0.5f, path[currentNode].pos[0] + yOffset + 0.5f), speed * Time.deltaTime);

                dir = new Vector2(path[currentNode].pos[1] + xOffset + 0.5f - transform.position.x, path[currentNode].pos[0] + yOffset + 0.5f - transform.position.y);
            }
            else dir = Vector2.zero;
            
        }
    }

    void FixedUpdate() {
        rb.velocity = dir.normalized * speed;
    }
}

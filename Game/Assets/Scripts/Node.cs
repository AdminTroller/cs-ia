using UnityEngine;

public class Node {
    // variables for nodes used in A* pathfinding
    public int g;
    public int h;
    public int f => g + h;
    public int[] pos;
    public Node parent;

    // node constructor
    public Node(int x, int y) {
        pos = new int[]{x,y};
    }

    // mutator methods
    public void SetParent(Node parent) {this.parent = parent;}
    public void SetG(int g) {this.g = g;}
    public void SetH(int h) {this.h = h;}
    public string Print() {return "G: " + g + ", H: " + h + ", F: " + f;}

    // returns grid distance from this node to a specified target node
    public int GetDistance(Node target) {
        return Mathf.Abs(pos[0] - target.pos[0]) + Mathf.Abs(pos[1] - target.pos[1]);
    }
}
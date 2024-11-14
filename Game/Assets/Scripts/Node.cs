using UnityEngine;

public class Node {
    public int g;
    public int h;
    public int f => g + h;
    public int[] pos;
    public Node parent;

    public Node(int x, int y) {
        pos = new int[]{x,y};
    }
    public void SetParent(Node parent) {this.parent = parent;}
    public void SetG(int g) {this.g = g;}
    public void SetH(int h) {this.h = h;}
    public string Print() {return "G: " + g + ", H: " + h + ", F: " + f;}

    public int GetDistance(Node target) {
        return Mathf.Abs(pos[0] - target.pos[0]) + Mathf.Abs(pos[1] - target.pos[1]);
    }
}
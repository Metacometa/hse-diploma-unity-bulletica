using UnityEngine;

public class Chamber : MonoBehaviour
{
    private Transform leftWall;
    private Transform rightWall;
    private Transform upWall;
    private Transform downWall;

    public bool left;
    public bool right;
    public bool up;
    public bool down;


    void Start()
    {
        GetFillerTransforms();       
    }

    void Update()
    {
        ToogleWalls();
    }

    void ToogleWalls()
    {
        leftWall.gameObject.SetActive(left);
        rightWall.gameObject.SetActive(right);

        upWall.gameObject.SetActive(up);
        downWall.gameObject.SetActive(down);
    }

    void GetFillerTransforms()
    {
        Transform grid = transform.Find("Grid");
        if (grid != null)
        {
            leftWall = grid.Find("WallLeft");
            rightWall = grid.Find("WallRight");

            upWall = grid.Find("WallUp");
            downWall = grid.Find("WallDown");
        }
    }
}

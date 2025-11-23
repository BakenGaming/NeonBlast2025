using UnityEngine;

public class TestingScript : MonoBehaviour
{
    [SerializeField] private bool showGrid;
    [SerializeField] private int x,y;
    [SerializeField] private float cs;
    private Grid testGrid;
    void Start()
    {
        testGrid = new Grid(x,y,cs, new Vector3(20,0),showGrid);
    }

    void Update()
    {
        
    }

}

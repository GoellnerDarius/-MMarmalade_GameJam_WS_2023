using UnityEngine;

public class MoveTileMap : MonoBehaviour
{
    public GameObject[] tileMaps = new GameObject[2];

    public float speed = 2;
    private Vector3 move = new(0, -1, 0);

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < tileMaps.Length; i++)
        {
            tileMaps[i].transform.Translate(move * (speed * Time.deltaTime));

            if (tileMaps[i].transform.position.y < -24f)
            {
                tileMaps[i].transform.Translate(0, 48, 0);
            }
        }
    }
}

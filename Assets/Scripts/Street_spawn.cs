using UnityEngine;

public class Street_spawn : MonoBehaviour
{
    public float Counter = 0;
    public GameObject Street;


    // Update is called once per frame
    void Update()
    {
        Counter += 1 * Time.deltaTime;
        if (Counter >= 5 && Counter <= 6)
        {
            Instantiate(Street, new Vector3(0f, -40f, -10f), new Quaternion());
            Counter = 0;
        }
    }
}

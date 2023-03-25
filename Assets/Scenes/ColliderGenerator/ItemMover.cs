using UnityEngine;

public class ItemMover : MonoBehaviour
{
    [SerializeField] private GameObject _colliders;
    [SerializeField] private float _speed;

    private void Update()
    {
        foreach (Transform child in _colliders.transform)
        {
            child.position += new Vector3() { y = -_speed * Time.deltaTime };
        }
    }
}

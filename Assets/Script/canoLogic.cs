using UnityEngine;

public class canoLogic : MonoBehaviour
{
    public float canoSpeed;
    void Start()
    {
        
    }

    void Update()
    {
        CanoMove();
    }

    void CanoMove()
    {
        transform.position += Vector3.left * canoSpeed * Time.deltaTime;
    }
}

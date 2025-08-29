using UnityEngine;

public class CanoSpawn : MonoBehaviour
{
    public float maxTime;
    private float time;
    public GameObject cano;
    GameObject canoClone;
    public float dist;
    void Start()
    {
        CanoSpawner();
    }
    void Update()
    {
        if(time > maxTime)
        {
            CanoSpawner();
            time = 0;
        }

        time += Time.deltaTime;
    }

    void CanoSpawner()
    {
        canoClone = Instantiate(cano);
        canoClone.transform.position = transform.position + new Vector3(0, Random.Range(dist, -dist), 0);
    }
}

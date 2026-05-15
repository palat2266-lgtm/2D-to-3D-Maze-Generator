using UnityEngine;

public class CreateTrees : MonoBehaviour
{

    public GameObject Tree;

    void Start()
    {
        int treeCount = 250;

        for (int i = 0; i < treeCount; i++)
        {
            CreateRandomTree();
        }
    }

    void CreateRandomTree()
    {
        float x = 0;
        float z = 0;

        // випадково вибираємо сторону
        int side = Random.Range(0, 4);

        switch (side)
        {
            // низ
            case 0:
                x = Random.Range(-15f, 30f);
                z = Random.Range(-15f, -1f);
                break;

            // верх
            case 1:
                x = Random.Range(-15f, 30f);
                z = Random.Range(20f, 30f);
                break;

            // ліва
            case 2:
                x = Random.Range(-15f, -1f);
                z = Random.Range(-15f, 30f);
                break;

            // права
            case 3:
                x = Random.Range(20f, 30f);
                z = Random.Range(-15f, 30f);
                break;
        }

        Vector3 pos = new Vector3(x, 0, z);

        // рандомний поворот
        float rotY = Random.Range(0f, 360f);

        // легкий нахил
        float rotX = Random.Range(-4f, 4f);
        float rotZ = Random.Range(-4f, 4f);

        Quaternion rot = Quaternion.Euler(rotX, rotY, rotZ);

        GameObject tree = Instantiate(Tree, pos, rot);

        // рандомний розмір
        float scale = Random.Range(0.16f, 0.24f);

        tree.transform.localScale = new Vector3(scale, scale, scale);
    }
}

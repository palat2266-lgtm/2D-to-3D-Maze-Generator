using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class General : MonoBehaviour
{

    int i, j, k;
    bool play=false, f1;
    public GameObject CubeTexture, CubeEmpty;
    RaycastHit hit;

    bool[,] m = new bool[20,20];

    BinaryReader br;
    BinaryWriter bw;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        BinaryReader bw = new BinaryReader(File.Open("map", FileMode.Open));

        for (i = 0; i < 20; i++)
        {
            for (j = 0; j < 20; j++)
            {
                m[i, j] = bw.ReadBoolean();

                Instantiate(
                    m[i, j] ? CubeTexture : CubeEmpty,
                    new Vector3(i, 0.5f, j),
                    Quaternion.Euler(0, 0, 0)
                );
            }
        }

        bw.Close();




        /*for(i = 0; i < 20; i++)
        {
            for (j = 0; j < 20; j++) m[i,j] = true;
        }
        for (i = 1;i < 19; i++)
        {
            m[i,1] = m[i,18] = m[1,i] = m[18,i] = false;
        }

        for (i = 0; i < 20; i++)
        {
            for (j = 0; j < 20; j++)
            {
                Instantiate(m[i, j] ? CubeTexture : CubeEmpty, new Vector3(i, 0.5f, j), Quaternion.Euler(0, 0, 0));
            }
        }*/

    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (play == false) //editer
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit))
                {
                    if (hit.collider.tag == "Untagged") return;
                        Instantiate(hit.collider.tag == "EmptyObj" ? CubeTexture : CubeEmpty, hit.collider.GetComponent<Transform>().position, 
                            Quaternion.Euler(0, 0, 0));
                    m[(int)hit.collider.transform.position.x, (int)hit.collider.transform.position.z] = hit.collider.tag.Equals("EmptyObj") ? true : false;
                    Destroy(hit.collider.gameObject);
                }
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            float speed = Input.GetKey(KeyCode.LeftShift) ? 4f : 2.5f;

            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            Vector3 forward = transform.forward;
            Vector3 right = transform.right;

            // прибираємо рух вверх/вниз
            forward.y = 0;
            right.y = 0;

            // нормалізуємо
            forward.Normalize();
            right.Normalize();

            Vector3 move = right * x + forward * z;

            transform.position += move * speed * Time.deltaTime;

            float mouseSensitivity = 200f;

            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            transform.Rotate(0, mouseX, 0);
        }
    }

    public void BClick()
    {
        bw = new BinaryWriter(File.Open("map",FileMode.OpenOrCreate));
        for (i = 0; i < 20; i++)
        {
            for (j = 0; j < 20; j++) bw.Write(m[i, j]);
        }
        bw.Close();

        GameObject[] tmp = GameObject.FindGameObjectsWithTag("EmptyObj");
        for(i=0; i < tmp.Length;i++)
        {
            Destroy(tmp[i].gameObject);
        }
        GameObject[] tmp1 = GameObject.FindGameObjectsWithTag("TextureObj");
        Vector3 v = new Vector3(0, 1f, 0);
        for (i = 0; i < tmp1.Length; i++)
        {
            Instantiate(CubeTexture, tmp1[i].GetComponent<Transform>().position + v,
                            Quaternion.Euler(0, 0, 0));
        }

        transform.position = new Vector3(1, 0.5f, 18);
        transform.rotation = Quaternion.Euler(0, 0, 0);

        play = true;
        //Destroy(GameObject.FindGameObjectWithTag("Buttons"));
        GameObject.FindGameObjectWithTag("Buttons").SetActive(false);
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerIO : MonoBehaviour
{

    public static PlayerIO currentPlayerIO;
    public float maxInteractionRange = 8;

    public byte selectedInventory = 0;
    public List<byte> inventoryBlocks = new List<byte>();
    public List<byte> inventoryBlocksCount = new List<byte>();

    // Use this for initialization
    void Start()
    {
        currentPlayerIO = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedInventory = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedInventory = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedInventory = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedInventory = 4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            selectedInventory = 5;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, maxInteractionRange))
            {
                Chunk chunk = hit.transform.GetComponent<Chunk>();
                if (chunk == null)
                {
                    return;
                }

                Vector3 p = hit.point;
                p.y /= World.currentWorld.brickHeight;

                p -= hit.normal / 4;
                chunk.SetBrick(0, p);
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, maxInteractionRange))
            {
                Chunk chunk = hit.transform.GetComponent<Chunk>();
                if (chunk == null)
                {
                    Debug.Log("Clicked on " + hit.transform.name + " and isn't a chunk.");
                    return;
                }

                Vector3 p = hit.point;
                p.y /= World.currentWorld.brickHeight;

                //if (inventoryBlocks.Count == 0)
                //{
                //    p -= hit.normal / 4;
                //    inventoryBlocks.Add(chunk.GetByte(p));
                //    chunk.SetBrick(0, p);
                //    Debug.Log(inventoryBlocks[0]);
                //}
                //else
                //{
                //    for (int a = 0; a < inventoryBlocks.Count; a++)
                //    {
                //        if (inventoryBlocks.Count <= 9)
                //        {
                //            if (chunk.GetByte(p) != 0)
                //            {
                //                p -= hit.normal / 4;
                //                inventoryBlocks.Add(chunk.GetByte(p));
                //                chunk.SetBrick(0, p);
                //                Debug.Log(inventoryBlocks[a]);
                //            }
                //        }
                //    }
                //}

                //if (selectedInventory == 0)
                //{
                //    p -= hit.normal / 4;
                //    selectedInventory = chunk.GetByte(p);
                //    chunk.SetBrick(0, p);
                //}
                //else
                //{
                //    p += hit.normal / 4;
                //    chunk.SetBrick(selectedInventory, p);
                //    selectedInventory = 0;
                //}

                p += hit.normal / 4;
                chunk.SetBrick(selectedInventory, p);
            }
            else
            {
                Debug.Log("Clicked, but has nothing.");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baston : Singleton<Baston>
{
    protected override bool persistent => false;

    public GameObject obj;
    [SerializeField] Transform aim;


    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            LeaveBox();
        }
    }

    public void GetBox(GameObject _obj)
    {
        obj = _obj;
        obj.transform.position = aim.position;
        obj.transform.rotation = aim.rotation;
        obj.transform.parent = this.transform;
        
    }

    public void LeaveBox()
    {
        if(obj != null)
        {
            obj.GetComponent<Caja>().LeaveObj();
            obj.transform.parent = null;
            obj = null;
        }
    }
}

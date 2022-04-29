using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    ItemModel itemModel;

    void Start(){
        itemModel = gameObject.GetComponent<ItemModel>();
    }

    public void Consume(){
        ref string item = ref itemModel.Item();
        switch(item){
            case "Meat":
                MeatController meatController = gameObject.GetComponent<MeatController>();
                meatController.ApplyStatEffect();
                break;
        }
    }
}

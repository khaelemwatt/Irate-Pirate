using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemController : MonoBehaviour
{
    ItemModel itemModel;

    void Start(){
        itemModel = gameObject.GetComponent<ItemModel>();

        ref GameObject itemPickupImage = ref itemModel.ItemPickupImage();
        ref GameObject itemPickupText = ref itemModel.ItemPickupText();
        ref GameObject itemPickupDesc = ref itemModel.ItemPickupDesc();

        itemPickupImage = GameObject.FindWithTag("ItemImage");
        itemPickupText = GameObject.FindWithTag("ItemText");
        itemPickupDesc = GameObject.FindWithTag("ItemDesc");

        itemPickupImage.GetComponent<Image>().sprite = null;
        itemPickupImage.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        itemPickupText.GetComponent<Text>().text = "";
        itemPickupDesc.GetComponent<Text>().text = "";
    }

    public void Consume(){
        ref string item = ref itemModel.Item();
        switch(item){
            case "Meat":
                MeatController meatController = gameObject.GetComponent<MeatController>();
                meatController.ApplyStatEffect();
                break;
            case "Egg":
                EggController eggController = gameObject.GetComponent<EggController>();
                eggController.ApplyStatEffect();
                break;
            case "Mushroom":
                MushroomController mushroomController = gameObject.GetComponent<MushroomController>();
                mushroomController.ApplyStatEffect();
                break;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        ref GameObject itemPickupImage = ref itemModel.ItemPickupImage();
        ref GameObject itemPickupText = ref itemModel.ItemPickupText();
        ref GameObject itemPickupDesc = ref itemModel.ItemPickupDesc();

        GameObject player = collision.collider.gameObject;
        if(player.CompareTag("Player"))
        {
            itemPickupImage.GetComponent<Image>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            itemPickupImage.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            itemPickupText.GetComponent<Text>().text = gameObject.GetComponent<ItemModel>().Item();
            itemPickupDesc.GetComponent<Text>().text = itemModel.ItemDesc();

            StartCoroutine(PickupTimer());
            Consume();
        }
    }   

    IEnumerator PickupTimer(){

        Destroy(gameObject.GetComponent<SpriteRenderer>());
        Destroy(gameObject.GetComponent<BoxCollider2D>());

        yield return new WaitForSeconds(4f);
        itemModel.itemPickupImage.GetComponent<Image>().sprite = null;
        itemModel.itemPickupImage.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        itemModel.itemPickupText.GetComponent<Text>().text = "";
        itemModel.itemPickupDesc.GetComponent<Text>().text = "";
        Destroy(gameObject);
    } 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class itemPickup : MonoBehaviour

{
// public int bombAmount = 1;
// public int bombsRemaining=1;
// [SerializeField] private Text bombsRemainingText;
    public enum ItemType{
        ExtraBomb,
        BlastRadius,
        SpeedIncrease,
    }
    public ItemType type;
    private void OnItemPick(GameObject player){
        switch(type){
            case ItemType.ExtraBomb:
            player.GetComponent<BombController>().AddBomb();
            // UpdateBombsRemainingText();
            break;
            case ItemType.BlastRadius:
player.GetComponent<BombController>().explosionReduis++;
            break;
            case ItemType.SpeedIncrease:
    player.GetComponent<MovmentController>().speed++;
break;
        }
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other){
    if(other.CompareTag("Player")){
        OnItemPick(other.gameObject);
    }
    }
//     private void UpdateBombsRemainingText()
// {
//     bombsRemainingText.text = "Bombs: " + bombsRemaining + "/" +  bombAmount;
// }
    
}
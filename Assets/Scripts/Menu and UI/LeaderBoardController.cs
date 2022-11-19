using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using LootLocker.Requests;
using UnityEngine;
using TMPro;

public class LeaderBoardController : MonoBehaviour
{
    public int ID;
    public TextMeshProUGUI userID;
    public TextMeshProUGUI userScore;
    private bool getData;

    private void Awake(){

    }
    private void Start(){
        getData = false;
        LootLockerSDKManager.StartSession("virus1", (response)=>
        {
            if(response.success)
            {   
                Debug.Log("Success");
            } else
            {
                Debug.Log("Failed");
            }
        });
    }


    private void Update(){
        if (!getData){
            LootLockerSDKManager.GetScoreListMain(ID, 5, 0, (response) =>
            {
                if(response.success){
                    string playerNames = "";
                    string playerScores = "Scores\n";

                    LootLockerLeaderboardMember [] members = response.items;

                    for (int i = 0; i<members.Length; i++){
                        if (members[i].player.name != ""){
                            playerNames += members[i].player.name;
                        }
                    }
                    userID.text = playerNames;  
                }else{
                    Debug.Log("Failed get data");
                    userID.text = "12334";
                }
                getData = true;
            });
        }
    }

    public void SubmitScore(){
        LootLockerSDKManager.SubmitScore(PlayerPrefs.GetString("ID"), HighestScore.currentHighest, ID, (response)=>
        {
            if(response.success)
            {   
                Debug.Log("Success");
            } else
            {
                Debug.Log("Failed");
            }
        });
    }
}


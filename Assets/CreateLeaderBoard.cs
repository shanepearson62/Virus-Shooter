using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class CreateLeaderBoard : MonoBehaviour
{
    public TextMeshProUGUI userID;
    public TextMeshProUGUI userScore;
    public int ID;
    // Start is called before the first frame update
    void Start()
    {
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
                userID.text = "12334";
            }else{
                Debug.Log("Failed get data");
                userID.text = "12334";
            }
            
        });
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

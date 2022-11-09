using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using LootLocker.Requests;
using UnityEngine;

public class LeaderBoardController : MonoBehaviour
{
    public InputField MemberID, PlayerScore;
    public int ID;

    private void Start(){
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
    public void SubmitScore(){
        LootLockerSDKManager.SubmitScore(MemberID.text, int.Parse(PlayerScore.text), ID, (response)=>
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


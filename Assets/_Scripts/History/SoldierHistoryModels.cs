using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierHistoryModels : MonoBehaviour {

    [SerializeField] protected TextMeshTeamLeadObjectMarker textTeamLead;
    [SerializeField] protected TextMeshTeamAssistObjectMarker textTeamAssist;
    [SerializeField] protected TeamWinObjectMarker teamWinLogo;
	// TODO: only enable either team win or target win logo

    [SerializeField] protected TextMeshTargetLeadObjectMarker textTargetLead;
    [SerializeField] protected TextMeshTargetAssistObjectMarker textTargetAssist;
    [SerializeField] protected TargetWinObjectMarker targetWinLogo;


    [SerializeField] protected HighRuleObjectMarker ruleHighFront;
    [SerializeField] protected HighRuleObjectMarker ruleHighBack;
    [SerializeField] protected LowRuleObjectMarker ruleLowFront;
    [SerializeField] protected LowRuleObjectMarker ruleLowBack;
	
	// TODO: Add Getters

}

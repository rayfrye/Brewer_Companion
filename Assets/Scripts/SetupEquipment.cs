using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Equipment
{
	public string equipmentName;
	public float mashVol;
	public float tunMass;
	public int boilRateFlag;
	public float tunSpecificHeat;
	public float tunDeadspace;
	public int tunAdjDeadspace;
	public int calcBoil;
	public float boilVol;
	public float boilTime;
	public float oldEvapRate;
	public int equip39;
	public float boilOff;
	public float trubLoss;
	public float coolPct;
	public float topUpKettle;
	public float batchVol;
	public float fermenterLoss;
	public float topUp;
	public float efficiency;
	public float hopUtil;
	public string notes;
}

public class SetupEquipment : MonoBehaviour 
{
	public List<Equipment> equipments;

}

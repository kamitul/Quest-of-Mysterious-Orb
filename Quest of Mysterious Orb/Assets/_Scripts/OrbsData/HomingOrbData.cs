using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HomingOrbData", menuName = "Quest of Mysterious Orb/ScriptableData/OrbData/HomingOrb", order = 0)]
public class HomingOrbData : OrbData {
   #region MovementData
      [Range(0,5f)] [SerializeField] private float movingSpeed;
   #endregion

   public override void RandomizeParameters()
   {
      movingSpeed = UnityEngine.Random.Range(0, 5f);
   }

   public override List<dynamic> GetData() {
      List<dynamic> dataToReturn = new List<dynamic>();
      dataToReturn.Add("Moving Speed");
      dataToReturn.Add(movingSpeed);
      return dataToReturn;
   }

    public override void SetData(List<dynamic> dataTab)
    {
       movingSpeed = dataTab[1];
    }
}
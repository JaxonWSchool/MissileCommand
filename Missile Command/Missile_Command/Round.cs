using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Missile_Command
{
    class Round
    {

        public Round()
        {
            
        }

        //if enemyMissileList is empty, round ends
        public bool roundEnd(List<Missile> eml) 
        {
            return eml.Count == 0;
        }

        //if silos are destroyed, game ends
        public bool gameOver(Silo one, Silo two, Silo three)
        {
            if (one == null && two == null && three == null)
                return true;
            return false;
        }

        //if at least 1 city is not hit, game is not over
        public bool gameOver(List<City> cityList)
        {
            for (int x = 0; x < cityList.Count; x++)
            {
                if (cityList[x].isHit == false)  
                    return false;
            }
            return true;
        }
    }
}

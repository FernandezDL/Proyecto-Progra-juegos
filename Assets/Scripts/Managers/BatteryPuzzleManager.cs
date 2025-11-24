using System.Collections.Generic;
using System.Linq;
using Objects;
using UnityEngine;

namespace Managers
{
    public class BatteryPuzzleManager : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private List<Battery> batteries;
        [SerializeField] private GameObject cristal;

        private int currentBatteryId = -1;
        
        private void Start()
        {
            foreach (var battery in batteries)
            {
                battery.OnBatteryTurnedOn += EvaluateBatteryId;
            }
        }

        private void EvaluateBatteryId(int id)
        {
            if (currentBatteryId == -1)
            {
                currentBatteryId = id;
                return;
            }

            if (currentBatteryId != id)
            {
                foreach (var battery in batteries.Where(battery =>
                             battery.BatteryID == currentBatteryId || battery.BatteryID == id))
                {
                    if(!battery.IsComplete) battery.DeactivateBattery();
                }
                currentBatteryId = -1;
                return;
            }

            foreach (var battery in batteries.Where(battery => battery.BatteryID == currentBatteryId))
            {
                battery.CompleteBattery();
            }
            EvaluateCompleteCondition();
        }

        private void EvaluateCompleteCondition()
        {
            var completed = true;

            foreach (var _ in batteries.Where(battery => !battery.IsComplete))
            {
                completed = false;
            }

            if (!completed) return;
            cristal.SetActive(false);
        }
    }
}

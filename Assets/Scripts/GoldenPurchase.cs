using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenPurchase : MonoBehaviour
{
    public void PurchaseGoldenArch()
    {
        if (GameManager.Instance != null)
        {
            GameManager.PurchaseGoldenArch(GameManager.Instance.currentBalance);
        }
    }
}

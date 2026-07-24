using System.Threading.Tasks;
using UnityEngine;

public class TapsellAd : MonoBehaviour
{
    private void OnEnable()
    {
        bool userConsent = true;
        Tapsell.Mediation.Tapsell.SetUserConsent(userConsent);

        EventsManager.OnShowAd_Event += RequestAd;
    }

    private void OnDisable()
    {
        EventsManager.OnShowAd_Event += RequestAd;
    }

    //tnmrcibtnseiethrbokrsllljeojogftmrjpeenhlimghrdegkqemnrqopehehjmhgmalk
    //6a25565e62158d268e9562c5
    private const string ZoneID = "6a63535c4faf920e00708728";
    private static string _adId = "";

    public void RequestAd()
    {
        Tapsell.Mediation.Tapsell.RequestRewardedAd(ZoneID,
            adId =>
            {
                Debug.Log("onRewardedAd requestSuccess");
                _adId = adId;
                ShowAd();
            },
            (error) =>
            {
                _adId = "";
                Debug.Log("onRewardedAd requestFailed: " + error);
                WinLoseWindow_UI winLoseWindow_UI = new WinLoseWindow_UI(new GameObject(), false);
            }
        );
    }

    public async void ShowAd()
    {
        while (_adId == "")
        {
            Debug.Log("Null");
            await Awaitable.EndOfFrameAsync();
        }

        if (_adId != "")
        {
            Tapsell.Mediation.Tapsell.ShowRewardedAd(_adId,
                () => { Debug.Log("onRewardedAd impression"); },
                () => { Debug.Log("onRewardedAd click"); },
                completionState => { Debug.Log("onRewardedAd close: " + completionState); },
                message => { Debug.Log("onRewardedAd showFailed: " + message); },
                () => { Debug.Log("onRewardedAd rewarded"); }
            );
        }
    }



    /*
    private const string ZoneID = "6a25565e62158d268e9562c5";
    private static string _adId;

    public void Request()
    {
        Tapsell.Mediation.Tapsell.RequestRewardedAd(ZoneID,
            adId =>
            {
                Debug.Log("onRewardedAd requestSuccess");
                _adId = adId;
            },
            error =>
            {
                Debug.Log("onRewardedAd requestFailed");
            }
        );
    }

    public void Show()
    {
        if (_adId != "")
        {
            Tapsell.Mediation.Tapsell.ShowRewardedAd(_adId,
                () => { Debug.Log("onRewardedAd impression"); },
                () => { Debug.Log("onRewardedAd click"); },
                completionState => { Debug.Log("onRewardedAd close: " + completionState); },
                message => { Debug.Log("onRewardedAd showFailed: " + message); },
                () => { Debug.Log("onRewardedAd rewarded"); }
            );
        }
    }
    */
}

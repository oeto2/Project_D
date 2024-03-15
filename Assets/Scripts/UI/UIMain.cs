using UnityEngine;
using UnityEngine.UI;

public class UIMain : UIBase
{
    [SerializeField] private Button btnInventory;
    [SerializeField] private Button btnRanking;
    [SerializeField] private Button btnMission;

    void Start()
    {
        //btninventory.onclick.addlistener(() =>
        //{
        //    var ui = uimanager.instance.openui<uipopup>();
        //    ui.setpopup("open inventory ?? ", () => { uimanager.instance.openui<uiinventory>(); });
        //});

        //btnranking.onclick.addlistener(() =>
        //{
        //    var ui = uimanager.instance.openui<uipopup>();
        //    ui.setpopup("open ranking ?? ", () => { uimanager.instance.openui<uiranking>(); });
        //});

        //btnmission.onclick.addlistener(() =>
        //{
        //    var ui = uimanager.instance.openui<uipopup>();
        //    ui.setpopup("open mission ?? ", () => { uimanager.instance.openui<uimission>(); });
        //});
    }
}

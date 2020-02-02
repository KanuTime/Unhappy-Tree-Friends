using _Scripts.Powers;
using _Scripts.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Scripts
{
    public class TutorialView : View
    {
        [SerializeField] private Text _windCost;
        [SerializeField] private Text _earthCost;
        [SerializeField] private Text _waterCost;
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("_Scenes/Game");
            }
        }

        protected override void Install()
        {
            Container.BindInterfacesTo<ManaModel>().AsSingle();
            
            Container.BindInterfacesTo<PowerCostPresenter>().AsCached().WithArguments(PowerType.Wind, _windCost);
            Container.BindInterfacesTo<PowerCostPresenter>().AsCached().WithArguments(PowerType.Earth, _earthCost);
            Container.BindInterfacesTo<PowerCostPresenter>().AsCached().WithArguments(PowerType.Water, _waterCost);
        }
    }
}
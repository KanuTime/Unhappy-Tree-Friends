using DefaultNamespace;

namespace _Scripts.Tiles.Types
{
    public class SwampView : TileView
    {
        protected override void Install()
        {
            base.Install();
            
            Container.BindInterfacesTo<FloodSystem>().AsSingle();
        }
    }
}
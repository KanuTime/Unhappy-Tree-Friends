using DefaultNamespace;

namespace _Scripts.Tiles.Types
{
    public class DesertView : TileView
    {
        protected override void Install()
        {
            base.Install();
            
            Container.BindInterfacesTo<TornadoSystem>().AsSingle();
        }
    }
}
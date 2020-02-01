using DefaultNamespace;

namespace _Scripts.Tiles.Types
{
    public class SeaView : TileView
    {
        protected override void Install()
        {
            base.Install();
            
            Container.BindInterfacesTo<TornadoSystem>().AsSingle();
        }
    }
}
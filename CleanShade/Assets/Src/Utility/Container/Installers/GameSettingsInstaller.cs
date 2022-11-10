using Src.CameraHandling;
using Src.Characters;
using Zenject;

namespace Src.Utility.Container.Installers
{
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        public CharactersSettings Characters;
        public CameraSettings Camera;
 
        public override void InstallBindings()
        {
            Container.BindInstance(Characters.PlayerSettings).IfNotBound();
            Container.BindInstance(Characters.EnemySettings).IfNotBound();
            Container.BindInstance(Camera).IfNotBound();
        }
    }
}
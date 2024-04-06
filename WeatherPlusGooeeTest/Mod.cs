using Colossal.Logging;
using Game;
using Game.Modding;
using Game.SceneFlow;
//using DependencyPack;
using System.Reflection;
using System.Linq;
using Gooee.Plugins.Attributes;
using Gooee.Plugins;
using Gooee.Example.UI;
using Game.UI;
using HarmonyLib;
using Unity.Entities;
using Gooee.Example.Models;
using UnityEngine;



namespace WeatherPlusGooeeTest
{
    public class Mod : IMod
    {
        public static ILog log = LogManager.GetLogger($"{nameof(WeatherPlusGooeeTest)}.{nameof(Mod)}").SetShowsErrorsInUI(false);

        private Harmony _harmony;
        private World _world;
        private ExamplePlugin _myPlugin;


        public void OnLoad(UpdateSystem updateSystem)
        {

            _world = updateSystem.World;

            updateSystem.UpdateAt<ExampleController>(SystemUpdatePhase.MainLoop);
            updateSystem.UpdateAt<ExampleController>(SystemUpdatePhase.Rendering);

            /*_harmony = new Harmony("WeatherPlus");
            _harmony.PatchAll();*/
            // ... other code ...

            // Assuming you have a field or property to store your 'ExamplePlugin' instance
            _myPlugin = new ExamplePlugin();

            // Log the resolved path for your UI script
            var scriptPath = _myPlugin.ScriptResource;
            log.Info($"Attempting to load script from: {scriptPath}");
        }


        private void SafelyRemove<T>()
            where T : GameSystemBase
        {
            var system = _world?.GetExistingSystemManaged<T>();

            if (system != null)
                _world.DestroySystemManaged(system);
        }

        public void OnDispose()
        {
            log.Info(nameof(OnDispose));

            SafelyRemove<ExampleController>();


            /*_harmony.UnpatchAll("WeatherPlus");*/
        }
    }

   

    [ControllerTypes(typeof(ExampleController))]
    public class ExamplePlugin : IGooeePluginWithControllers
    {
        public string Name => "WeatherPlusGooeeTest";
        public string Version => "1.1.2";
        public string ScriptResource => "WeatherPlusGooeeTest.Resources.ui.js";

        //public string StyleResource => null;

        public IController[] Controllers
        {
            get;
            set;
        }
    }
}

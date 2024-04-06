using Game.Tools;
using Gooee.Plugins.Attributes;
using Gooee.Plugins;
using Unity.Entities;
using System;
using Gooee.Example.Models;
using Colossal.UI.Binding;
using Game.Prefabs;

namespace Gooee.Example.UI
{
    public partial class ExampleController : Controller<ExampleModel>
    {
        public override ExampleModel Configure( )
        {
            var toolSystem = World.GetOrCreateSystemManaged<ToolSystem>( );

            toolSystem.EventToolChanged += ( tool =>
            {
                if ( Model.IsVisible )
                {
                    Model.IsVisible = false;
                    TriggerUpdate( );
                }
            } );

            return new ExampleModel( );
        }

        [OnTrigger]
        private void OnTestClick( )
        {
            Model.Message = "An amended message! " + DateTime.Now;
            TriggerUpdate( );
        }

      
    }
}

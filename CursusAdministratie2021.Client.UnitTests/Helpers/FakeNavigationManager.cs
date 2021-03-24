using Bunit.Rendering;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Client.UnitTests.Helpers {
    //https://github.com/egil/blazor-workshop/blob/master/save-points/09-progressive-web-app/BlazingPizza.Tests/TestDoubles/FakeNavigationManager.cs
    public class FakeNavigationManager : NavigationManager {
        private readonly ITestRenderer renderer;

        public FakeNavigationManager(ITestRenderer renderer) {
            this.renderer = renderer;
            Initialize("http://localhost/", "http://localhost/");
        }

        protected override void NavigateToCore(string uri, bool forceLoad) {
            Uri = ToAbsoluteUri(uri).ToString();

            renderer.Dispatcher.InvokeAsync(
                () => NotifyLocationChanged(isInterceptedLink: false));
        }
    }
}

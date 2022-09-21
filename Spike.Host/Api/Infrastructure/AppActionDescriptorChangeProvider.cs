using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spike.Base.Host.Api.Infrastructure
{
    /// <summary>
    /// An app specific implementation of the
    /// <see cref="IActionDescriptorChangeProvider"/>
    /// contract.
    /// <para>
    /// Usage:
    /// <![CDATA[
    /// public void ConfigureServices(IServiceCollection services)
    /// {
    ///     services.AddMvc();
    ///     services.AddSingleton<IActionDescriptorChangeProvider>(AppActionDescriptorChangeProvider.Instance);
    ///     services.AddSingleton(AppActionDescriptorChangeProvider.Instance);
    /// }
    /// ]]>
    /// </para>
    /// </summary>
    /// <remarks>
    /// Refer to:
    /// https://stackoverflow.com/questions/46156649/asp-net-core-register-controller-at-runtime
    /// </remarks>
    public class AppActionDescriptorChangeProvider : IActionDescriptorChangeProvider
    {

        /// <summary>
        /// Public flag as to whether things have changed.
        /// <para>
        /// TODO: Improve documentation.
        /// </para>
        /// </summary>
        public bool HasChanged { get; set; }

        /// <summary>
        /// Singleton static instance of this
        /// <see cref="AppActionDescriptorChangeProvider"/>.
        /// </summary>
        public static AppActionDescriptorChangeProvider Instance { get; } =
            new AppActionDescriptorChangeProvider();


        /// <summary>
        /// Constructor
        /// </summary>
        public AppActionDescriptorChangeProvider()
        {
            HasChanged = false;
        }

        /// <summary>
        /// The <see cref="CancellationTokenSource"/>
        /// developed within 
        /// <see cref="GetChangeToken"/>
        /// </summary>
        public CancellationTokenSource? TokenSource { get; private set; }


        /// <inheritdoc/>
        public IChangeToken GetChangeToken()
        {
            // Create a new CancellationTokenSource
            TokenSource = new CancellationTokenSource();

            // Reset flag:
            HasChanged = false;

            // return new token:
            return new CancellationChangeToken(TokenSource.Token);
        }
        /// <summary>
        /// Reset the 
        /// <see cref="TokenSource"/>
        /// </summary>
        public void Reset()
        {
            if (TokenSource == null)
            {
                return;
            }

            HasChanged = true;
            TokenSource.Cancel();
        }
    }
}

// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Threading.Tasks;

namespace AngularJSAuthentication.Glass.Provider
{
    public interface IGlassAuthenticationProvider
    {
        Task Authenticated(GlassAuthenticatedContext context);
        Task ReturnEndpoint(GlassReturnEndpointContext context);
    }
}

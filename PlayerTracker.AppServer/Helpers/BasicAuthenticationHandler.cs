using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PlayerTracker.AppServer.Model;
using PlayerTracker.AppServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace PlayerTracker.AppServer.Helpers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private MongoDbContext dbcontext;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            MongoDbContext dbcontext)
            : base(options,
                  logger,
                  //new DummyLogger(),
                  encoder, clock)
        {
            this.dbcontext = dbcontext;
        }

        public void dodo(int a, int b) {}

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {

            // skip authentication if endpoint has [AllowAnonymous] attribute
            var endpoint = Context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
                return AuthenticateResult.NoResult();

            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");
            
            // retrieve credentials
            string tokenDecoded =
                Request.Headers["Authorization"]
                .Chain(auth => auth.Last().Split().Last())
                .Chain(Convert.FromBase64String)
                .Chain(Encoding.UTF8.GetString);

            string username = null, password = null;
            tokenDecoded.Split(':').Chain(val => { username = val[0]; password = val[1]; });

            var userColl = dbcontext.PluginDb.GetCollection<AuthorizationInformationModel>("authorization");
            var left = Builders<AuthorizationInformationModel>.Filter.Eq("name", username);
            var right = Builders<AuthorizationInformationModel>.Filter.Eq("token", password);
            var filter = Builders<AuthorizationInformationModel>.Filter.And(left, right);

            AuthorizationInformationModel authEntity = userColl.Find(user => user.Name == username && user.Token == password).FirstOrDefault();
            if (authEntity is null)
                return AuthenticateResult.Fail("Invalid token");

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, authEntity._id.ToString()),
                new Claim(ClaimTypes.Name, authEntity.Name),
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}

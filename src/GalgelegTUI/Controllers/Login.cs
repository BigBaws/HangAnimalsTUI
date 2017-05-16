using GalgelegTUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GalgelegTUI
{
    public class Login
    {
        string Url;

        public Login()
        {
            Url = "login";
        }

        public async Task<User> UserLogin(string userID, string pass)
        {

            Dictionary<string, string> pairs = new Dictionary<string, string>()
            {
                {"username", userID },
                {"password", pass }
            };

            RESTConnector rc = new RESTConnector();
            var userLoggedIn = await rc.RestPOST<User>(Url, pairs);

            var user = new User()
            {
                Name = userLoggedIn.Name,
                Token = userLoggedIn.Token,
                Userid = userLoggedIn.Userid,
                Currency = userLoggedIn.Currency,
                Study = userLoggedIn.Study,
                Animal = userLoggedIn.Animal,
                AnimalColor = userLoggedIn.AnimalColor,
                Singleplayer = userLoggedIn.Singleplayer,
                Multiplayer = userLoggedIn.Multiplayer

            };
                
            return user;
        }
    }
}

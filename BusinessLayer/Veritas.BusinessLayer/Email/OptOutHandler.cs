using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veritas.DataLayer;
using Veritas.DataLayer.Models;
using Veritas.BusinessLayer.Caching;
using Veritas.BusinessLayer.Logging;

namespace Veritas.BusinessLayer.Email
{
    public class OptOutHandler
    {
        public static void ProcessOptOut(string emailAddress)
        {
            var repo = VeritasRepository.GetInstance();

            Blacklist blacklistedAddress = repo.GetBlacklistByEmailAddress(CacheHandler.BlogConfigId, emailAddress);
            if (blacklistedAddress != null)
            {
                LoggingHandler.Log("Opt Out Issue",
                    "Address " + emailAddress + " tried to optout but was already opted out.", 
                    "Info", "OptOutHandler");
            }
            else
            {
                blacklistedAddress = new Blacklist();
                blacklistedAddress.EmailAddress = emailAddress;
                blacklistedAddress.BlogConfigId = CacheHandler.BlogConfigId;
                blacklistedAddress.CreateDate = DateTime.Now;
                repo.Add(blacklistedAddress);
                repo.Save();
            }
        }
    }
}

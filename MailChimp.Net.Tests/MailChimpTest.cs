﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MailChimpTest.cs" company="Brandon Seydel">
//   N/A
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MailChimp.Net.Core;
using MailChimp.Net.Interfaces;
using MailChimp.Net.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailChimp.Net.Tests
{
    /// <summary>
    /// The mail chimp test.
    /// </summary>
    public abstract class MailChimpTest
    {
        /// <summary>
        /// The _mail chimp manager.
        /// </summary>
        protected IMailChimpManager _mailChimpManager;

        internal List MailChimpList => new List
        {
            Name = "TestList",
            PermissionReminder = "none",
            Contact = new Contact
            {
                Address1 = "TEST",
                City = "Bettendorf",
                Country = "USA",
                State = "IA",
                Zip = "61250",
                Company = "TEST"
            },
            CampaignDefaults = new CampaignDefaults
            {
                FromEmail = "test@test.com",
                FromName = "test",
                Language = "EN",
                Subject = "Yo"
            }
        };


        internal async Task ClearMailChimpAsync()
        {
            var lists = await this._mailChimpManager.Lists.GetAllAsync();
            await Task.WhenAll(lists.Select(x => _mailChimpManager.Lists.DeleteAsync(x.Id)));

            var campaings = await this._mailChimpManager.Campaigns.GetAllAsync();
            await Task.WhenAll(campaings.Select(x => _mailChimpManager.Campaigns.DeleteAsync(x.Id)));

            



        }

        /// <summary>
        /// The initialize.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this._mailChimpManager = new MailChimpManager();
        }

        /// <summary>
        /// The hash.
        /// </summary>
        /// <param name="emailAddress">
        /// The email address.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        internal string Hash(string emailAddress)
        {
            using (var md5 = MD5.Create()) return md5.GetHash(emailAddress);
        }
    }
}
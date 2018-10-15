﻿// <copyright file="FakeGeocoderHttpClient.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>

namespace Neocoder.UnitTests.Fakes
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Neocoder.Interfaces;
    using Neocoder.UnitTests.ExtensionMethods;

    /// <summary>
    /// Fake for testing the <see cref="GoogleGeocoder"/> class without causing any network traffic to Google.
    /// </summary>
    public class FakeGeocoderHttpClient : IGeocoderHttpClient
    {
        public async Task<HttpResponseMessage> MakeApiRequestAsync(string url)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent("Invalid response"),
            };

            if (url.Contains("NULL"))
            {
                response.Content = new StringContent(string.Empty);
            }
            else if (url.Contains("Header") && url.Contains("SlowDown"))
            {
                response.Headers.Add("X-MS-BM-WS-INFO", "1");
            }
            else if (url.Contains("File"))
            {
                response.Content = new StringContent(ExtractContentFromFile(url));
            }

            return await Task.FromResult(response);
        }

        private static string ExtractContentFromFile(string url)
        {
            var content = string.Empty;
            var urlParams = url.Substring(url.IndexOf("?") + 1);
            var keyvalues = urlParams.Split("&");

            foreach (var keyvalueItem in keyvalues)
            {
                var pair = keyvalueItem.Split("=");

                if (pair.Length == 2 && pair[1].StartsWith("File"))
                {
                    var cleaned = WebUtility.UrlDecode(pair[1]);
                    var fileSplit = cleaned.Split("=");

                    content = ResourceReader.ReadEmbeddedFile($"Neocoder.UnitTests/Data/{fileSplit[1]}");
                    break;
                }
            }

            return content;
        }
    }
}

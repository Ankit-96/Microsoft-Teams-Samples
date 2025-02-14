﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace CallingBotSample.Options
{
    public class BotOptions
    {
        /// <summary>
        /// Gets the application id.
        /// </summary>
        public string? AppId { get; set; }

        /// <summary>
        /// Gets the application secret.
        /// </summary>
        public string? AppSecret { get; set; }

        /// <summary>
        /// Gets the calls uri of the application.
        /// </summary>
        public Uri? BotBaseUrl { get; set; }

        /// <summary>
        /// Gets the comms platform endpoint uri.
        /// </summary>
        public Uri? PlaceCallEndpointUrl { get; set; }

        /// <summary>
        /// Gets the graph resource url.
        /// </summary>
        public string? GraphApiResourceUrl { get; set; }

        /// <summary>
        ///  Gets The Microsoft login url
        /// </summary>
        public string? MicrosoftLoginUrl { get; set; }
    }
}

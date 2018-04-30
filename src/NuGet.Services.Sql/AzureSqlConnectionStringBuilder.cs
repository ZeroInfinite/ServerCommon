﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;

namespace NuGet.Services.Sql
{
    /// <summary>
    /// Builder for SQL server connections which support AAD token-based authentication with
    /// <see cref="AzureSqlConnectionFactory"/>.
    /// </summary>
    public class AzureSqlConnectionStringBuilder : DbConnectionStringBuilder
    {
        private const string AadAuthorityTemplate = "https://login.microsoftonline.com/{0}/v2.0";

        public string AadTenant { get; }

        public string AadClientId { get; }

        public string AadCertificate { get; }

        [DefaultValue(true)]
        public bool AadSendX5c { get; }

        public string AadAuthority { get; }

        public AzureSqlConnectionStringBuilder(string connectionString)
        {
            ConnectionString = connectionString;

            AadTenant = Ingest<string>(nameof(AadTenant));
            AadClientId = Ingest<string>(nameof(AadClientId));
            AadCertificate = Ingest<string>(nameof(AadCertificate));
            AadSendX5c = Ingest(nameof(AadSendX5c), defaultValue: true);

            if (!string.IsNullOrEmpty(AadTenant))
            {
                AadAuthority = string.Format(CultureInfo.InvariantCulture, AadAuthorityTemplate, AadTenant);
            }
        }

        private T Ingest<T>(string propertyName, T defaultValue = default(T))
        {
            T result = defaultValue;
            if (ContainsKey(propertyName))
            {
                var value = this[propertyName] as string;
                result = (T)Convert.ChangeType(value, typeof(T));
                Remove(propertyName);
            }
            return result;
        }
    }
}

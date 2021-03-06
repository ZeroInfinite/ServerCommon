﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace NuGet.Services.Validation
{
    /// <summary>
    /// The result of an asynchronous validation.
    /// </summary>
    public interface IValidationResult
    {
        /// <summary>
        /// The status of the validation.
        /// </summary>
        ValidationStatus Status { get; }

        /// <summary>
        /// The validation issues that were encountered.
        /// </summary>
        IReadOnlyList<IValidationIssue> Issues { get; }

        /// <summary>
        /// The URL to the modified package content (.nupkg). This URL should be accessible without special
        /// authentication headers. However, authentication information could be included in the URL (e.g. Azure Blob
        /// Storage SAS URL). This URL need not have a single value for a specific <see cref="ValidationId"/>.
        /// </summary>
        string NupkgUrl { get; }
    }
}
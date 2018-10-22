﻿// <copyright file="DateRangeScope.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>

namespace GeneGenie.Geocoder.Models
{
    public enum DateRangeScope
    {
        NotSet = 0,

        /// <summary>Not implemented yet, for future usage, depending on data source.</summary>
        ExactDateAndTime = 1,

        ExactDateWithTimeRange = 2,

        DateRangeWithTimeRange = 3,
    }
}

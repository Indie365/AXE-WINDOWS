// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Axe.Windows.Extensions.Interfaces.ReferenceLinks;
using System;

namespace Axe.Windows.RuleSelection
{
    class ReferenceLink : IReferenceLink
    {
        public string ShortDescription { get; }
        public Uri Uri { get; }

        public ReferenceLink(string shortDescription, string url)
        {
            ShortDescription = shortDescription;
            Uri = new Uri(url);
        }

        public ReferenceLink(string shortDescription)
        {
            ShortDescription = shortDescription;
        }
    } // class
} // namespace

// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Axe.Windows.Core.Bases;
using Axe.Windows.Core.Types;
using System;

namespace Axe.Windows.Rules.PropertyConditions
{
    class PlatformProperties
    {
        public static Condition SimpleStyle = Condition.Create(IsSimpleStyle);

        private static bool IsSimpleStyle(IA11yElement e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));

            var style = e.GetPlatformPropertyValue<uint>(PlatformPropertyType.Platform_WindowsStylePropertyId);

            const int CBS_SIMPLE = 1;

            return (style & CBS_SIMPLE) != 0;
        }
    } // class
} // namespace

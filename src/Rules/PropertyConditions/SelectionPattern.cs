// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Axe.Windows.Core.Bases;
using Axe.Windows.Core.Types;
using System;

namespace Axe.Windows.Rules.PropertyConditions
{
    /// <summary>
    /// Contains commonly used conditions for testing against the Selection pattern of an IA11yElement.
    /// </summary>
    static class SelectionPattern
    {
        // pattern property values
        public const string CanSelectMultipleProperty = "CanSelectMultiple";

        public static Condition Null = Condition.Create(IsNull);
        public static Condition NotNull = ~Null;
        public static Condition CanSelectMultiple = Condition.Create(GetCanSelectMultiple);

        private static bool IsNull(IA11yElement e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));

            var selectionPattern = e.GetPattern(PatternType.UIA_SelectionPatternId);
            return selectionPattern == null;
        }

        private static bool GetCanSelectMultiple(IA11yElement e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));

            var selectionPattern = e.GetPattern(PatternType.UIA_SelectionPatternId);
            if (selectionPattern == null) return false;

            return selectionPattern.GetValue<bool>(CanSelectMultipleProperty);
        }
    } // class
} // namespace

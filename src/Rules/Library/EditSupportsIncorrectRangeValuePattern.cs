// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Axe.Windows.Core.Bases;
using Axe.Windows.Core.Enums;
using Axe.Windows.Core.Types;
using Axe.Windows.Rules.PropertyConditions;
using Axe.Windows.Rules.Resources;
using System;
using static Axe.Windows.Rules.PropertyConditions.ControlType;

namespace Axe.Windows.Rules.Library
{
    [RuleInfo(ID = RuleId.EditSupportsIncorrectRangeValuePattern)]
    class EditSupportsIncorrectRangeValuePattern : Rule
    {
        public EditSupportsIncorrectRangeValuePattern()
        {
            Info.Description = Descriptions.EditSupportsIncorrectRangeValuePattern;
            Info.HowToFix = HowToFix.EditSupportsIncorrectRangeValuePattern;
            Info.Standard = A11yCriteriaId.AvailableActions;
            Info.ErrorCode = EvaluationCode.Error;
        }

        public override bool PassesTest(IA11yElement e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));

            return IsLargeChangeValueNull(e);
        }

        private static bool IsLargeChangeValueNull(IA11yElement e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));

            var rangeValue = e.GetPattern(PatternType.UIA_RangeValuePatternId);
            if (rangeValue == null) return true;

            var largeChange = rangeValue.GetValue<double>("LargeChange");

            return largeChange == default(double);
        }

        protected override Condition CreateCondition()
        {
            return Edit & Patterns.RangeValue;
        }
    } // class
} // namespace

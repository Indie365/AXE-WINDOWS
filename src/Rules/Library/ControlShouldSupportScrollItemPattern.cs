// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Axe.Windows.Core.Bases;
using Axe.Windows.Core.Enums;
using Axe.Windows.Rules.PropertyConditions;
using Axe.Windows.Rules.Resources;
using System;
using static Axe.Windows.Rules.PropertyConditions.ControlType;
using static Axe.Windows.Rules.PropertyConditions.Relationships;

namespace Axe.Windows.Rules.Library
{
    [RuleInfo(ID = RuleId.ControlShouldSupportScrollItemPattern)]
    class ControlShouldSupportScrollItemPattern : Rule
    {
        public ControlShouldSupportScrollItemPattern()
        {
            Info.Description = Descriptions.ControlShouldSupportScrollItemPattern;
            Info.HowToFix = HowToFix.ControlShouldSupportScrollItemPattern;
            Info.Standard = A11yCriteriaId.AvailableActions;
            Info.ErrorCode = EvaluationCode.Error;
        }

        public override bool PassesTest(IA11yElement e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));

            var condition = Patterns.ScrollItem | AnyChild(Patterns.ScrollItem);
            return condition.Matches(e);
        }

        protected override Condition CreateCondition()
        {
            return Parent(Patterns.Scroll & (ScrollPattern.HorizontallyScrollable | ScrollPattern.VerticallyScrollable))
                & (DataItem | ListItem | TreeItem);
        }
    } // class
} // namespace

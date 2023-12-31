// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Axe.Windows.Core.Bases;
using Axe.Windows.Core.Enums;
using Axe.Windows.Rules.PropertyConditions;
using Axe.Windows.Rules.Resources;
using static Axe.Windows.Rules.PropertyConditions.ControlType;

namespace Axe.Windows.Rules.Library
{
    [RuleInfo(ID = RuleId.ButtonInvokeAndTogglePatterns)]
    class ButtonInvokeAndTogglePatterns : Rule
    {
        public ButtonInvokeAndTogglePatterns()
        {
            Info.Description = Descriptions.ButtonInvokeAndTogglePatterns;
            Info.HowToFix = HowToFix.ButtonInvokeAndTogglePatterns;
            Info.Standard = A11yCriteriaId.NameRoleValue;
            Info.ErrorCode = EvaluationCode.Error;
        }

        public override bool PassesTest(IA11yElement e)
        {
            var rule = ~(Relationships.All(Patterns.Invoke, Patterns.Toggle));

            return rule.Matches(e);
        }

        protected override Condition CreateCondition()
        {
            return Button;
        }
    } // class
} // namespace

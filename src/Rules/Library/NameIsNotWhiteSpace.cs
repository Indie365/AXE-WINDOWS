// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Axe.Windows.Core.Bases;
using Axe.Windows.Core.Enums;
using Axe.Windows.Core.Types;
using Axe.Windows.Rules.Resources;
using static Axe.Windows.Rules.PropertyConditions.StringProperties;

namespace Axe.Windows.Rules.Library
{
    [RuleInfo(ID = RuleId.NameNotWhiteSpace)]
    class NameIsNotWhiteSpace : Rule
    {
        public NameIsNotWhiteSpace()
        {
            Info.Description = Descriptions.NameNotWhiteSpace;
            Info.HowToFix = HowToFix.NameNotWhiteSpace;
            Info.Standard = A11yCriteriaId.ObjectInformation;
            Info.PropertyID = PropertyType.UIA_NamePropertyId;
            Info.ErrorCode = EvaluationCode.Error;
        }

        public override bool PassesTest(IA11yElement e)
        {
            return e.Name.Trim().Length > 0;
        }

        protected override Condition CreateCondition()
        {
            return Name.NotNullOrEmpty;
        }
    } // class
} // namespace

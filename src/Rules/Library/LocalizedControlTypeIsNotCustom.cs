// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Axe.Windows.Core.Bases;
using Axe.Windows.Core.Enums;
using Axe.Windows.Core.Types;
using Axe.Windows.Rules.PropertyConditions;
using Axe.Windows.Rules.Resources;
using System;
using static Axe.Windows.Rules.PropertyConditions.BoolProperties;
using static Axe.Windows.Rules.PropertyConditions.ControlType;
using static Axe.Windows.Rules.PropertyConditions.Relationships;
using static Axe.Windows.Rules.PropertyConditions.StringProperties;

namespace Axe.Windows.Rules.Library
{
    [RuleInfo(ID = RuleId.LocalizedControlTypeNotCustom)]
    class LocalizedControlTypeIsNotCustom : Rule
    {
        public LocalizedControlTypeIsNotCustom()
        {
            Info.Description = Descriptions.LocalizedControlTypeNotCustom;
            Info.HowToFix = HowToFix.LocalizedControlTypeNotCustom;
            Info.Standard = A11yCriteriaId.ObjectInformation;
            Info.PropertyID = PropertyType.UIA_LocalizedControlTypePropertyId;
            Info.ErrorCode = EvaluationCode.Error;
        }

        public override bool PassesTest(IA11yElement e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));

            return e.LocalizedControlType != LocalizedControlTypeNames.Custom;
        }

        protected override Condition CreateCondition()
        {
            var dataGridDetailsPresenter = ClassName.Is("DataGridDetailsPresenter") & Parent(DataItem);

            return Custom
                & IsKeyboardFocusable
                & LocalizedControlType.NotNullOrEmpty
                & ~dataGridDetailsPresenter
                & ~ElementGroups.WPFDataGridCell;
        }
    } // class
} // namespace

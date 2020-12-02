﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

namespace Microsoft.Data.Entity.Design.Model.Designer
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Xml.Linq;

    internal class DesignerProperty : EFNameableItem
    {
        internal static readonly string ElementName = "DesignerProperty";
        internal static readonly string AttributeValue = "Value";
        private DefaultableValue<string> _valueAttr;

        internal DesignerProperty(DesignerInfoPropertySet parent, XElement element)
            : base(parent, element)
        {
        }

        internal DefaultableValue<string> ValueAttr
        {
            get
            {
                if (_valueAttr == null)
                {
                    _valueAttr = new AttributeValueDefaultableValue(this);
                }
                return _valueAttr;
            }
        }

        private class AttributeValueDefaultableValue : DefaultableValue<string>
        {
            internal AttributeValueDefaultableValue(EFElement parent)
                : base(parent, AttributeValue)
            {
            }

            internal override string AttributeName
            {
                get { return AttributeValue; }
            }

            public override string DefaultValue
            {
                get { return String.Empty; }
            }
        }

#if DEBUG
        internal override ICollection<string> MyAttributeNames()
        {
            var s = base.MyAttributeNames();
            s.Add(AttributeValue);
            return s;
        }
#endif

        protected override void PreParse()
        {
            Debug.Assert(State != EFElementState.Parsed, "this object should not already be in the parsed state");

            ClearEFObject(_valueAttr);
            _valueAttr = null;

            base.PreParse();
        }

        // we unfortunately get a warning from the compiler when we use the "base" keyword in "iterator" types generated by using the
        // "yield return" keyword.  By adding this method, I was able to get around this.  Unfortunately, I wasn't able to figure out
        // a way to implement this once and have derived classes share the implementation (since the "base" keyword is resolved at 
        // compile-time and not at runtime.
        private IEnumerable<EFObject> BaseChildren
        {
            get { return base.Children; }
        }

        internal override IEnumerable<EFObject> Children
        {
            get
            {
                foreach (var efobj in BaseChildren)
                {
                    yield return efobj;
                }
                yield return ValueAttr;
            }
        }

        internal override bool ParseSingleElement(ICollection<XName> unprocessedElements, XElement element)
        {
            if (element.Name.LocalName == AttributeValue)
            {
                _valueAttr.Value = element.Value;
            }
            else
            {
                return base.ParseSingleElement(unprocessedElements, element);
            }
            return true;
        }

        internal override string GetRefNameForBinding(ItemBinding binding)
        {
            return LocalName.Value;
        }
    }
}

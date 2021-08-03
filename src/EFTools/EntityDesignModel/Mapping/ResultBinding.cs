﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the MIT license.  See License.txt in the project root for license information.

namespace Microsoft.Data.Entity.Design.Model.Mapping
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Xml.Linq;
    using Microsoft.Data.Entity.Design.Model.Entity;

    internal class ResultBinding : EFElement
    {
        internal static readonly string ElementName = "ResultBinding";

        internal static readonly string AttributeName = "Name";
        internal static readonly string AttributeColumnName = "ColumnName";

        private SingleItemBinding<Property> _property;
        private DefaultableValue<string> _columnNameAttr;

        internal ResultBinding(EFElement parent, XElement element)
            : base(parent, element)
        {
            Debug.Assert(parent is ModificationFunction, "parent should be a ModificationFunction");
        }

        internal ModificationFunction ModificationFunction
        {
            get
            {
                var parent = Parent as ModificationFunction;
                Debug.Assert(parent != null, "this.Parent should be a ModificationFunction");
                return parent;
            }
        }

        /// <summary>
        ///     Manages the content of the Name attribute
        /// </summary>
        internal SingleItemBinding<Property> Name
        {
            get
            {
                if (_property == null)
                {
                    _property = new SingleItemBinding<Property>(
                        this,
                        AttributeName,
                        ProperyMappingNameNormalizer.NameNormalizer
                        );
                }
                return _property;
            }
        }

        /// <summary>
        ///     Manages the content of the ColumnName attribute; unlike ScalarProperty or Condition,
        ///     the ColumnName attribute of a ResultBinding is a free form string that maps to the
        ///     result set returned from the sproc.  There is nothing to "bind" to.
        /// </summary>
        internal DefaultableValue<string> ColumnName
        {
            get
            {
                if (_columnNameAttr == null)
                {
                    _columnNameAttr = new ColumnNameDefaultableValue(this);
                }
                return _columnNameAttr;
            }
        }

        private class ColumnNameDefaultableValue : DefaultableValue<string>
        {
            internal ColumnNameDefaultableValue(EFElement parent)
                : base(parent, AttributeColumnName)
            {
            }

            internal override string AttributeName
            {
                get { return AttributeColumnName; }
            }

            public override string DefaultValue
            {
                get { return null; }
            }
        }

#if DEBUG
        internal override ICollection<string> MyAttributeNames()
        {
            var s = base.MyAttributeNames();
            s.Add(AttributeName);
            s.Add(AttributeColumnName);
            return s;
        }
#endif

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
                yield return Name;
                yield return ColumnName;
            }
        }

        protected override void PreParse()
        {
            Debug.Assert(State != EFElementState.Parsed, "this object should not already be in the parsed state");

            ClearEFObject(_property);
            _property = null;

            ClearEFObject(_columnNameAttr);
            _columnNameAttr = null;

            base.PreParse();
        }

        internal override string DisplayName
        {
            get
            {
                return string.Format(
                    CultureInfo.CurrentCulture, "{0} <==> {1}",
                    Name.RefName,
                    ColumnName.Value);
            }
        }

        protected override void DoResolve(EFArtifactSet artifactSet)
        {
            Name.Rebind();
            if (Name.Status == BindingStatus.Known)
            {
                State = EFElementState.Resolved;
            }
        }
    }
}

// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the MIT license.  See License.txt in the project root for license information.

namespace Microsoft.Data.Entity.Design.Model.Mapping
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Xml.Linq;
    using Microsoft.Data.Entity.Design.Model.Entity;

    internal class FunctionImportScalarProperty : EFElement
    {
        internal static readonly string ElementName = "ScalarProperty";
        internal static readonly string AttributeName = "Name";
        internal static readonly string AttributeColumnName = "ColumnName";

        private SingleItemBinding<Property> _property;
        private DefaultableValue<string> _column;

        internal FunctionImportScalarProperty(FunctionImportTypeMapping parent, XElement element)
            : base(parent, element)
        {
        }

        internal override string EFTypeName
        {
            get { return ElementName; }
        }

        internal FunctionImportTypeMapping TypeMapping
        {
            get { return Parent as FunctionImportTypeMapping; }
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
                        FunctionImportProperyMappingNameNormalizer.NameNormalizer
                        );
                }
                return _property;
            }
        }

        /// <summary>
        ///     Manages the content of the ColumnName attribute
        /// </summary>
        internal DefaultableValue<string> ColumnName
        {
            get
            {
                if (_column == null)
                {
                    _column = new ColumnNameDefaultableValue(this);
                }
                return _column;
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

        protected override void PreParse()
        {
            Debug.Assert(State != EFElementState.Parsed, "this object should not already be in the parsed state");

            ClearEFObject(_property);
            _property = null;

            ClearEFObject(_column);
            _column = null;

            base.PreParse();
        }

        internal override bool ParseSingleElement(ICollection<XName> unprocessedElements, XElement elem)
        {
            return false;
        }

        protected override void DoResolve(EFArtifactSet artifactSet)
        {
            Name.Rebind();
            if (Name.Status == BindingStatus.Known)
            {
                State = EFElementState.Resolved;
            }
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
                yield return Name;
                yield return ColumnName;
            }
        }
    }
}

﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the MIT license.  See License.txt in the project root for license information.

namespace Microsoft.Data.Entity.Design.Model.Entity
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Xml.Linq;
    using Microsoft.Data.Entity.Design.Model.Mapping;
    using Microsoft.Data.Entity.Design.VersioningFacade;

    internal class ConceptualEntitySet : EntitySet
    {
        internal static readonly string AttributeGetterAccess = "GetterAccess";
        private DefaultableValue<string> _getterAccessAttr;

        internal ConceptualEntitySet(EFElement parent, XElement element)
            : base(parent, element)
        {
        }

        internal EntitySetMapping EntitySetMapping
        {
            get
            {
                foreach (var esm in GetAntiDependenciesOfType<EntitySetMapping>())
                {
                    return esm;
                }
                return null;
            }
        }

        internal DefaultableValue<string> GetterAccess
        {
            get
            {
                if (_getterAccessAttr == null)
                {
                    _getterAccessAttr = new GetterAccessDefaultableValue(this);
                }
                return _getterAccessAttr;
            }
        }

        private class GetterAccessDefaultableValue : DefaultableValue<string>
        {
            internal GetterAccessDefaultableValue(EFElement parent)
                : base(parent, AttributeGetterAccess, SchemaManager.GetCodeGenerationNamespaceName())
            {
            }

            internal override string AttributeName
            {
                get { return AttributeGetterAccess; }
            }

            public override string DefaultValue
            {
                get { return ModelConstants.CodeGenerationAccessPublic; }
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

                yield return GetterAccess;
            }
        }

#if DEBUG
        internal override ICollection<string> MyAttributeNames()
        {
            var s = base.MyAttributeNames();
            s.Add(AttributeGetterAccess);
            return s;
        }
#endif

        protected override void PreParse()
        {
            Debug.Assert(State != EFElementState.Parsed, "this object should not already be in the parsed state");

            ClearEFObject(_getterAccessAttr);

            base.PreParse();
        }
    }
}

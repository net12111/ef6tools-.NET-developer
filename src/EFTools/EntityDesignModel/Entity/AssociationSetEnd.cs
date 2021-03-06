// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the MIT license.  See License.txt in the project root for license information.

namespace Microsoft.Data.Entity.Design.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Xml.Linq;
    using Microsoft.Data.Entity.Design.Model.Mapping;

    internal class AssociationSetEnd : EFNormalizableItem
    {
        internal static readonly string ElementName = "End";
        internal static readonly string AttributeRole = "Role";
        internal static readonly string AttributeEntitySet = "EntitySet";

        // TODO: override Children() method and return _roleBinding and _entitySetBinding;
        // also do this for all the other classes that have DefaultTableValues or ItemBindings.
        // An interesting test would be if a traversal of the XLinq tree from the root, produced the 
        // same output as a traversal of the EDM tree from the root (ie, do they both generate the same 
        // xml document).  This would be a great unit test we�ll have to add.  

        private SingleItemBinding<AssociationEnd> _roleBinding;
        private SingleItemBinding<EntitySet> _entitySetBinding;

        internal AssociationSetEnd(EFElement parent, XElement element)
            : base(parent, element)
        {
        }

        internal override string EFTypeName
        {
            get { return ElementName; }
        }

        /// <summary>
        ///     A bindable reference to the AssociationEnd for this end of the association set
        /// </summary>
        internal SingleItemBinding<AssociationEnd> Role
        {
            get
            {
                if (_roleBinding == null)
                {
                    _roleBinding = new SingleItemBinding<AssociationEnd>(
                        this,
                        AttributeRole,
                        AssociationSetEndRoleNormalizer.NameNormalizer);
                }

                return _roleBinding;
            }
        }

        /// <summary>
        ///     A bindable reference to the EntitySet for this end of the association set
        /// </summary>
        internal SingleItemBinding<EntitySet> EntitySet
        {
            get
            {
                if (_entitySetBinding == null)
                {
                    _entitySetBinding = new SingleItemBinding<EntitySet>(
                        this,
                        AttributeEntitySet,
                        AssociationSetEndEntitySetNormalizer.NameNormalizer);
                }
                return _entitySetBinding;
            }
        }

#if DEBUG
        internal override ICollection<string> MyAttributeNames()
        {
            var s = base.MyAttributeNames();
            s.Add(AttributeRole);
            s.Add(AttributeEntitySet);
            return s;
        }
#endif

        protected override void PreParse()
        {
            Debug.Assert(State != EFElementState.Parsed, "this object should not already be in the parsed state");

            ClearEFObject(_roleBinding);
            _roleBinding = null;
            ClearEFObject(_entitySetBinding);
            _entitySetBinding = null;

            base.PreParse();
        }

        internal override bool ParseSingleElement(ICollection<XName> unprocessedElements, XElement elem)
        {
            return false;
        }

        protected override void DoNormalize()
        {
            var normalizedName = AssociationSetEndNameNormalizer.NameNormalizer(this, Role.RefName);
            Debug.Assert(null != normalizedName, "Null NormalizedName for refName " + Role.RefName);
            NormalizedName = (normalizedName != null ? normalizedName.Symbol : Symbol.EmptySymbol);
            base.DoNormalize();
        }

        protected override void DoResolve(EFArtifactSet artifactSet)
        {
            Role.Rebind();
            EntitySet.Rebind();

            if (Role.Status == BindingStatus.Known
                && EntitySet.Status == BindingStatus.Known)
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
                yield return EntitySet;
                yield return Role;
            }
        }

        internal ICollection<EndProperty> EndProperties
        {
            get
            {
                var antiDeps = Artifact.ArtifactSet.GetAntiDependencies(this);

                var ends = new List<EndProperty>();
                foreach (var antiDep in antiDeps)
                {
                    var end = antiDep as EndProperty;
                    if (end == null
                        && antiDep.Parent != null)
                    {
                        end = antiDep.Parent as EndProperty;
                    }

                    if (end != null)
                    {
                        ends.Add(end);
                    }
                }

                return ends.AsReadOnly();
            }
        }

        internal EndProperty EndProperty
        {
            get
            {
                foreach (var end in EndProperties)
                {
                    // just return the first one
                    return end;
                }

                return null;
            }
        }

        /// <summary>
        ///     An AssociationSetEnd is always referred to by its bare Name.  This is because an AssociationSetEnd is always
        ///     referred to in the context of an AssociationSet.
        /// </summary>
        internal override string GetRefNameForBinding(ItemBinding binding)
        {
            return Role.RefName;
        }

        internal override void GetXLinqInsertPosition(EFElement child, out XNode insertAt, out bool insertBefore)
        {
            if (child is EndProperty)
            {
                insertAt = FirstChildXElementOrNull();
                insertBefore = true;
            }
            else
            {
                base.GetXLinqInsertPosition(child, out insertAt, out insertBefore);
            }
        }

        public override IValueProperty<string> Name
        {
            get { throw new NotImplementedException("What do we do here...."); }
        }
    }
}
